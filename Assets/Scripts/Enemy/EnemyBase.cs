using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyBase : EntityBase
{
    public float maxHp;
    public float defaultAtk;
    public float defaultSpeed;

    private float curr_hp;
    private float curr_atk;

    private Animator animator;

    public float checkStatusTime = 1f;
    [NonSerialized] public float timer;

    public float smoothTime = 0.5f;
    Vector3 velocity = Vector3.zero;

    BuffManager buffManager;

    /// <summary>
    /// 初始化
    /// </summary>
    public override void Init()
    {
        curr_hp = maxHp;
        curr_atk = defaultAtk;
        moveSpeed = defaultSpeed;

        buffManager = gameObject.GetComponent<BuffManager>();
        //animator = GetComponent<Animator>();
    }

    public override void UpdateFunc()
    {

    }

    /// <summary>
    /// 向车厢核心移动
    /// </summary>
    public void Move(Vector3 targetPosition)
    {
        if (isChaos) return;
        ////获取目标的当前位置
        //Vector3 endPos = target.transform.position;

        ////计算每帧的时间增量
        //float moveTime = Time.deltaTime;

        ////计算移动百分比
        //float percent = moveTime * moveSpeed / Vector3.Distance(this.transform.position, endPos);

        ////使用Lerp函数插值计算位置
        //transform.position = Vector3.Lerp(this.transform.position, endPos, percent);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, moveSpeed);
    }

    public override void TouchEvent(Collider2D collision)
    {
        Attack(collision);
    }

    public void Attack(Collider2D collision)
    {
        // TODO:播放动画、对火车造成伤害
        if (collision.CompareTag("TrainHead") || collision.CompareTag("TrainBody"))
        {

        }

    }

    #region 受伤与buff逻辑
    public void Hurt(float trainAtk)
    {
        this.curr_hp -= trainAtk;   // 需要在车厢攻击那边写一个受伤间隔逻辑
        if (curr_hp <= 0)       // 敌人死亡，返回对象池
        {
            this.gameObject.SetActive(false);
            // TODO:掉金币
        }
    }

    /// <summary>
    /// 由外部调用，为敌人挂上buff
    /// </summary>
    /// <param name="buffName">传入buff的名字（枚举值）</param>
    public void AddBuff(BuffName buffName)
    {
        this.buffManager.AddBuff(buffName, this);
    }

    /// <summary>
    /// 击退效果
    /// </summary>
    public void Repulsed()
    {
        //this.transform.Translate();
    }

    /// <summary>
    /// 减速buff
    /// </summary>
    public void Decelerate(float ratio)
    {
        moveSpeed = defaultSpeed * ratio;
    }
    /// <summary>
    /// 恢复速度
    /// </summary>
    public void NormalSpeed()
    {
        moveSpeed = defaultSpeed;
    }
    /// <summary>
    /// 攻击力下降
    /// </summary>
    public void AtkDown(float ratio)
    {
        curr_atk = defaultAtk * ratio;
    }
    /// <summary>
    /// 攻击力恢复
    /// </summary>
    public void AtkNormal()
    {
        curr_atk = defaultAtk;
    }
    public bool isChaos = false;    // 是否处于混乱状态
    #endregion


}

