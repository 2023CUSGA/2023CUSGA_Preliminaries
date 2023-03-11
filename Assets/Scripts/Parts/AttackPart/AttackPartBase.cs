using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackPartBase : MonoBehaviour
{
    [Header("攻击间隔")]
    [SerializeField]
    protected float attack_speed;
    protected float time;

    protected void Update()
    {
        time += Time.deltaTime;
        if (time >= attack_speed)
        {
            Attack();
            time = 0.0f;
        }
    }

    protected abstract void Attack();
}
