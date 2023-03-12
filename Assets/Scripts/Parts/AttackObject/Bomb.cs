using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : AttackObjectBase
{
    [Header("炸弹飞行速度")]
    [SerializeField]
    private float speed;
    [Header("是否为炮弹")]
    [SerializeField]
    private bool is_cannonball;
    [Header("攻击范围")]
    [SerializeField]
    private float distance;
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
        CircleCollider2D[] colliders = GetComponents<CircleCollider2D>();
        foreach (CircleCollider2D collider in colliders)
        {
            if (collider.isTrigger == true)
            {
                collider.radius = radius_boom;
                break;
            }
        }
        if (!is_cannonball)
        {
            p0 = transform.position;
            p = p0 + distance * new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            dis0 = (p - p0).magnitude;
            transform.rotation = Quaternion.identity;
            transform.Rotate(0, 0, Vector3.Angle(transform.right, (p - p0)));
        }
        dis = 0.0f;
    }

    private void FixedUpdate()
    {
        dis += speed * Time.deltaTime;
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        if (!is_cannonball)
        {
            if (dis >= dis0)
            {
                Explode();
            }
        }
        else
        {
            if (dis >= distance)
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

    private void OnCollisionEnter2D(Collision2D collision)
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
