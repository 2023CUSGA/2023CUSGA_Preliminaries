using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    Rigidbody2D rb;

    public List<Vector3> pathPoints = new List<Vector3>();
    int pathListIndex;
    Vector3 direction;

    Seeker seeker;
    List<Vector3> aimPoint;

    /// <summary>
    /// 初始化
    /// </summary>
    public override void Init()
    {
        rb = GetComponent<Rigidbody2D>();
        curr_hp = maxHp;
        curr_atk = defaultAtk;
        moveSpeed = defaultSpeed;
        buffManager = gameObject.GetComponent<BuffManager>();
        //animator = GetComponent<Animator>();

        seeker = GetComponent<Seeker>();
        seeker.pathCallback = OnPathComplete;

        EnvironmentManager.instance.enemysList.Add(this);
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
        #region 弃用的移动方法
        ////获取目标的当前位置
        //Vector3 endPos = targetPosition;

        ////计算每帧的时间增量
        //float moveTime = Time.deltaTime;

        ////计算移动百分比
        //float percent = moveTime * moveSpeed / Vector3.Distance(this.transform.position, endPos);

        ////使用Lerp函数插值计算位置
        //transform.position = Vector3.Lerp(this.transform.position, endPos, percent);

        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, moveSpeed); 

        //if (pathPoints.Count > 0)
        //{
        //    // 开始向下个路径点位移
        //    transform.position += direction.normalized * moveSpeed * Time.deltaTime;
        //    // 如果到达下一个路径点
        //    if (Vector3.Distance(new Vector3(pathPoints[pathListIndex].x, pathPoints[pathListIndex].y), gameObject.transform.position) <= 0.1f)
        //    {
        //        pathListIndex++;
        //        if (pathPoints.Count <= pathListIndex)      // 如果没有下个路径点了
        //        {
        //            pathPoints.Clear();
        //            pathListIndex = 0;
        //        }
        //        else
        //        {
        //            //计算下个路径点行走方向
        //            direction = new Vector3(pathPoints[pathListIndex].x, pathPoints[pathListIndex].y) - transform.position;
        //        }
        //    }
        //}
        #endregion

        if (aimPoint != null && aimPoint.Count != 0)
        {
            Vector3 dir = (aimPoint[0] - transform.position).normalized;
            transform.position += dir * moveSpeed * Time.fixedDeltaTime;    // 移动

            if ((transform.position - aimPoint[0]).sqrMagnitude <= 0.1f)    // 是否到达下一个目标点
            {
                aimPoint.RemoveAt(0);
            }
        }

    }

    /// <summary>
    /// 重新计算路径
    /// </summary>
    public void RePathFinding(Transform targetTrain)
    {
        //pathPoints.Clear();
        //pathListIndex = 0;
        //// 获取路径
        //pathPoints = PathFinding(PathFindingManager.instance.IAStar(this, transform.position, targetTrain.transform.position));
        //if (pathPoints.Count > 0)   // 计算初始行走方向
        //{
        //    direction = new Vector3(pathPoints[0].x, pathPoints[0].y) - transform.position;
        //}

        seeker.StartPath(transform.position, targetTrain.position);
        Vector3 dir = (targetTrain.position - transform.position);
        transform.localScale = dir.x >= 0 ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);  // 转向

    }

    void OnPathComplete(Path path)
    {
        aimPoint = new List<Vector3>(path.vectorPath);
    }

    /// <summary>
    /// 将路径转成列表
    /// </summary>
    List<Vector3> PathFinding(Stack<Vector3Int> trace)
    {
        List<Vector3> pathPoints = new List<Vector3>();
        if (trace == null) return pathPoints;

        while (trace.Count > 0)
        {
            pathPoints.Add(trace.Pop());
        }
        return pathPoints;
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
            EnvironmentManager.instance.enemysList.Remove(this);

            // TODO:掉金币
        }
    }

    public void HurtByRatio(float ratio)
    {
        this.curr_hp -= this.curr_hp * ratio;
        if (curr_hp <= 0)       // 敌人死亡，返回对象池
        {
            this.gameObject.SetActive(false);
            EnvironmentManager.instance.enemysList.Remove(this);

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
    public void Repulsed(float power)
    {
        isChaos = true;
        Vector2 dir = transform.position - GetComponentInChildren<TrackTrigger>().targetTrain.transform.position;
        rb.AddForce(dir * power, ForceMode2D.Impulse);
        StartCoroutine(WaitForRepulsed());
        //this.transform.Translate();
    }

    IEnumerator WaitForRepulsed()
    {
        yield return new WaitForSeconds(1.5f);
        isChaos = false;

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

