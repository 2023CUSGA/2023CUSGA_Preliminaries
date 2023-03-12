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

    protected void Attack(EnemyBase enemy)
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

    protected void OnBecameInvisible()
    {
        DestroyGameObject();
    }

    protected void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
