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

    private Rigidbody2D rb;
    private float dis;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        dis += speed * Time.deltaTime;
        rb.MovePosition(transform.position + speed * Time.deltaTime * transform.right);
        if (dis >= distance)
        {
            DestroyGameObject();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBase enemy = GetEnemy(collision);
        if (enemy != null)
        {
            Attack(enemy);
            DestroyGameObject();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("AttackObject"))
        {
            DestroyGameObject();
        }
    }
}
