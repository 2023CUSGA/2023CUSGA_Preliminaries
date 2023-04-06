using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackPartBase : PartBase
{
    protected float attack_speed;
    public void SetAttackSpeed(float speed) { attack_speed = speed; }

    protected GameObject[] prefebs;
    public void SetPrefeb(GameObject[] prefebs) { this.prefebs = prefebs; }

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
