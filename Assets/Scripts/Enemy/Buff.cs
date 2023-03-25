using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum BuffName 
{
    灼烧 = 0,
    击退 = 1,
    穿刺 = 2,
    减速 = 3,
    中毒 = 4,
    混乱 = 5,
}

public class Buff
{
    public BuffName buffName;
    EnemyBase owner;
    float time = 0f;
    int hit = 0;    // 命中次数
    float duration = 0f;    // 持续时间
    public bool Stoped { get; set; }

    public Buff(BuffName buffName, EnemyBase owner)
    {
        this.buffName = buffName;
        this.owner = owner;
        switch (buffName)
        {
            case BuffName.穿刺:
                duration = 2f;
                break;
            case BuffName.减速:
                duration = 3f;
                break;
            case BuffName.混乱:
                duration = 3f;
                break;
        }

        this.OnAdd();
    }
    private void OnAdd()
    {

    }
    private void OnRemove()
    {
        Stoped = true;
    }

    public void Update()
    {
        if (Stoped) return;

        this.time += Time.deltaTime;

        switch (buffName)
        {
            case BuffName.灼烧:
                float interval = 1f;    // 间隔时间
                if (this.time > interval * (this.hit + 1))
                {
                    this.hit++;
                    this.DoBuffDamage(4 - hit);
                }
                if (hit == 3)
                {
                    this.OnRemove();
                }
                break;
            case BuffName.击退:
                this.owner.Repulsed(80f);
                break;
            case BuffName.穿刺:
                break;
            case BuffName.减速:
                if (this.time < this.duration)
                {
                    owner.Decelerate(0.5f);
                }
                else
                {
                    owner.NormalSpeed();
                }
                break;
            case BuffName.中毒:
                owner.Decelerate(0.3f);
                owner.AtkDown(0.8f);
                break;
            case BuffName.混乱:
                if (this.time < this.duration)
                {
                    owner.isChaos = true;
                }
                else
                {
                    owner.isChaos = false;
                }
                break;
            default:
                break;
        }


    }

    void DoBuffDamage(float damage)
    {
        this.owner.Hurt(damage);
    }


}
