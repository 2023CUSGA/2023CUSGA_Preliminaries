using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : AttackObjectBase
{
    [Header("飞行速度")]
    [SerializeField]
    private float speed;
    

    private void FixedUpdate()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
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
            buffManager.AddBuff((BuffName)System.Enum.Parse(typeof(BuffName),buff), enemy);
        }
    }

}
