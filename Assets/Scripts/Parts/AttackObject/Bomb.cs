using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
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

    private Rigidbody2D rb;
    private List<EnemyBase> enemies;
    public void AddEnemy(EnemyBase enemy) { enemies.Add(enemy); }
    public void RemoveEnemy(EnemyBase enemy) { enemies.Remove(enemy); }

    private Vector3 p0;
    private Vector3 p;  //目标爆点
    private float dis0; //总距离
    private float dis;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
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
        rb.MovePosition(transform.position + new Vector3(speed * Time.deltaTime, 0, 0));
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
        if (!collision.gameObject.CompareTag("AttackObject"))
        {
            Explode();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBase enemy = GetEnemy(collision);
        if (enemy != null)
        {
            Explode();
        }
    }
}
