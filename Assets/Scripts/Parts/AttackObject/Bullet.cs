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

    private void FixedUpdate()
    {
        dis += speed * Time.deltaTime;
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        if (dis >= distance)
        {
            DestroyGameObject();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            Attack(enemy);
        }
        DestroyGameObject();
    }
}
