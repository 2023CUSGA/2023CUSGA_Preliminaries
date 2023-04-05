using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TrainPart : MonoBehaviour
{
    //如何获取hp和id？
    /*
    private float hp;
    public float GetHP() => hp;
    public void SetHP(float hp) { this.hp = hp; }
    public void HurtHP(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            DisableAllParts();
            this.enabled = false;
        }
    }
    */

    private int id;
    private List<PartBase> trainParts;
    public void DisableAllParts()
    {
        foreach(PartBase trainPart in trainParts)
        {
            trainPart.enabled = false;
        }
    }

    private void Start()
    {
        trainParts = new List<PartBase>();
        List<PartNames> parts = TrainPartDataContainer.GetTrainPart(id);
        foreach(PartNames name in parts)
        {
            switch(name)
            {
                case PartNames.弓弩:
                    {
                        AttackPart_Arrow part = gameObject.AddComponent<AttackPart_Arrow>();
                        GameObject[] prefeb = { (GameObject)Resources.Load("Prefab/Train/TrainParts/AttackObject/弓箭_test.prefab") };
                        part.SetPrefeb(prefeb);
                        part.SetAttackSpeed(2);
                        trainParts.Add(part);
                        break;
                    }
                case PartNames.火焰喷射器:
                    {
                        //add
                        break;
                    }
                case PartNames.石弹枪:
                    {
                        AttackPart_StoneBullet part = gameObject.AddComponent<AttackPart_StoneBullet>();
                        GameObject[] prefeb = { (GameObject)Resources.Load("Prefab/Train/TrainParts/AttackObject/石弹_test.prefab"),
                                                                (GameObject)Resources.Load("Prefab/Train/TrainParts/AttackObject/石弹_buff_test.prefab")};
                        part.SetPrefeb(prefeb);
                        part.SetAttackSpeed(1.5f);
                        trainParts.Add(part);
                        break;
                    }
                case PartNames.地刺:
                    {
                        AttackPart_Spike part = gameObject.AddComponent<AttackPart_Spike>();
                        trainParts.Add(part);
                        GameObject[] prefeb = { (GameObject)Resources.Load("Prefab/Train/TrainParts/AttackObject/地刺_test.prefab") };
                        part.SetPrefeb(prefeb);
                        part.SetAttackSpeed(2.5f);
                        break;
                    }
                case PartNames.炸药:
                    {
                        //add
                        break;
                    }
                case PartNames.电击枪:
                    {
                        //add
                        break;
                    }
                case PartNames.连发炮:
                    {
                        //add
                        break;
                    }
                case PartNames.化学迷雾:
                    {
                        //add
                        break;
                    }
                case PartNames.采集架:
                case PartNames.采集强化器:
                case PartNames.超采集器:
                    {
                        ResourcePart part = gameObject.AddComponent<ResourcePart>();
                        part.SetPartType(name);
                        trainParts.Add(part);
                        break;
                    }
                case PartNames.木制挡板:
                case PartNames.铁皮挡板:
                case PartNames.合金挡板:
                    {
                        DefendPart part = gameObject.AddComponent<DefendPart>();
                        part.SetPartType(name);
                        trainParts.Add(part);
                        break;
                    }
                case PartNames.小型探照灯:
                    {
                        //add
                        break;
                    }
                case PartNames.中型探照灯:
                    {
                        //add
                        break;
                    }
                case PartNames.大探照灯:
                    {
                        //add
                        break;
                    }
                default:break;
            }
        }
    }
}
