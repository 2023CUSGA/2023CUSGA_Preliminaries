//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class AttackObjectBase : MonoBehaviour
{
    [Header("伤害")]
    [SerializeField]
    protected float damage;
    [Header("造成的Buff，“无”或留空表示没有")]
    [SerializeField]
    protected string buff;

    public EnemyBase GetEnemy(Collision2D collision)
    {
        EnemyBase enemy = null;
        if ((enemy = collision.gameObject.GetComponent<NormalEnemy>()) != null) { }
        else if ((enemy = collision.gameObject.GetComponent<TankEnemy>()) != null) { }
        return enemy;
    }
    public EnemyBase GetEnemy(Collider2D collision)
    {
        EnemyBase enemy = null;
        if ((enemy = collision.GetComponent<NormalEnemy>()) != null) { }
        else if ((enemy = collision.GetComponent<TankEnemy>()) != null) { }
        return enemy;
    }

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

    public void DestroyGameObject()
    {
        gameObject.SetActive(false);
    }
}
