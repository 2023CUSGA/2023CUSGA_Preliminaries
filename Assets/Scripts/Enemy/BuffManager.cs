using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{

    List<Buff> buffs = new List<Buff>();


    private void OnEnable()
    {
        this.buffs.Clear();
    }

    public void AddBuff(BuffName buffName, EnemyBase owner)
    {
        Buff buff = new Buff(buffName, owner);
        this.buffs.Add(buff);
    }

    private void Update()
    {
        for (int i = 0; i < this.buffs.Count; i++)
        {
            if (!this.buffs[i].Stoped)      // 如果buff没停就继续更新
            {
                this.buffs[i].Update();
            }
        }

        this.buffs.RemoveAll((b) => b.Stoped);    // 把停止的buff从列表中清除
    }




}
