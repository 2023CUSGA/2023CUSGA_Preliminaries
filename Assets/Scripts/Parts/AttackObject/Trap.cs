using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : AttackObjectBase
{
    [Header("持续时间")]
    [SerializeField]
    private float time;
    [Header("攻击范围")]
    [SerializeField]
    private float radius;
    [Header("攻击间隔")]
    [SerializeField]
    private float time_attack;

    private List<EnemyBase> enemies;
    private float t;

    private void OnEnable()
    {
        t = 0;
        if (enemies == null)
        {
            enemies = new List<EnemyBase>();
        }
        else
        {
            enemies.Clear();
        }
    }

    private void Update()
    {
        t += Time.deltaTime;
        if (t >= time)
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
            enemies.Add(enemy);
            Invoke(nameof(RemoveFromList),time_attack);
        }
    }

    private void RemoveFromList(EnemyBase enemy)
    {
        enemies.Remove(enemy);
    }
}
