using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : AttackObjectBase
{
    [Header("炸弹飞行速度")]
    [SerializeField]
    private float speed;
    [Header("炸弹投掷半径，为0表示为炮弹")]
    [SerializeField]
    private float radius_throw;
    [Header("爆炸半径")]
    [SerializeField]
    private float radius_boom;

    private List<EnemyBase> enemies;

    private Vector3 p0;
    private Vector3 p;  //目标爆点
    private float dis0; //总距离
    private float dis;

    private void Start()
    {
        enemies = new List<EnemyBase>();
        if (radius_throw != 0.0f)
        {
            p0 = transform.position;
            p = p0 + radius_throw * new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            dis0 = (p - p0).magnitude;
            transform.rotation = Quaternion.identity;
            transform.Rotate(0, 0, Vector3.Angle(transform.right, (p - p0)));
            dis = 0.0f;
        }
        
    }

    private void FixedUpdate()
    {
        dis += speed * Time.deltaTime;
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        if (radius_throw != 0.0f)
        {
            if (dis >= dis0)
            {
                Explode();
            }
        }
    }

    private void Explode()
    {
        foreach (EnemyBase enemy in enemies)
        {
            Attack(enemy);
        }
        DestroyGameObject();
    }

    protected override void Attack(EnemyBase enemy)
    {
        enemy.Hurt(damage);
        if (buff != "" && buff != "无")
        {
            BuffManager buffManager = enemy.gameObject.GetComponent<BuffManager>();
            if (buffManager == null)
            {
                buffManager = enemy.gameObject.AddComponent<BuffManager>();
            }
            buffManager.AddBuff((BuffName)System.Enum.Parse(typeof(BuffName), buff), enemy);
        }
    }

    protected new void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemies.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemies.Remove(enemy);
        }
    }

}
