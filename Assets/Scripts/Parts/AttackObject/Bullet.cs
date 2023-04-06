using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : AttackObjectBase
{
    [Header("飞行速度")]
    [SerializeField]
    private float speed;
    [Header("攻击范围")]
    [SerializeField]
    private float distance;

    private float dis;

    private void OnEnable()
    {
        dis = 0;
    }

    private void FixedUpdate()
    {
        dis += speed * Time.deltaTime;
        transform.position += speed * Time.deltaTime * transform.right;
        if (dis >= distance)
        {
            DestroyGameObject();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyBase enemy = GetEnemy(collision);
        if (enemy != null)
        {
            Attack(enemy);
            DestroyGameObject();
        }
        if (!collision.gameObject.CompareTag("AttackObject"))
        {
            DestroyGameObject();
        }
    }
}
