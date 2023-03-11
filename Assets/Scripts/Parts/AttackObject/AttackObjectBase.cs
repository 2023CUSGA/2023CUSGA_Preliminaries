using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackObjectBase : MonoBehaviour
{
    [Header("伤害")]
    [SerializeField]
    protected float damage;
    [Header("造成的Buff，“无”或留空表示没有")]
    [SerializeField]
    protected string buff;

    protected abstract void Attack(EnemyBase enemy);

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            Attack(enemy);
        }
        DestroyGameObject();
    }

    protected void OnBecameInvisible()
    {
        DestroyGameObject();
    }

    protected void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
