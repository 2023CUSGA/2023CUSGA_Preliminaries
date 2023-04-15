using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

//车厢数据
public class TrainBody : MonoBehaviour
{
    //车厢序号
    private int id;
    public int GetId() => id;
    public void SetId(int id) { this.id = id; }

    //车厢HP
    private float health = 100;
    private bool dead;
    public float GetHealth() => health;
    /// <summary>
    /// 设定车厢血量(初始化用，造成伤害用DecreaseHealth)
    /// </summary>
    /// <param name="health"></param>
    public void SetHealth(float health) {  this.health = health; }
    /// <summary>
    /// 减少车厢血量(造成伤害用)
    /// </summary>
    /// <param name="damage"></param>
    public void DecreaseHealth(float damage)
    { 
        if (!dead)
        {
            health -= damage;
            if(health <= 0)
            {
                health = 0;
                DisableAllParts();
                this.gameObject.GetComponent<TrainBodyMov>().SetBrokeImage();
                TrainManager.GetInstance().DecreaseTrainBodyNum();
                dead = true;
            }
        }
    }

    //车厢配件
    private List<PartBase> trainParts;
    /// <summary>
    /// 从TrainPartDataContainer中读取并实例化配件
    /// </summary>
    private void LoadParts()
    {
        trainParts = new List<PartBase>();
        List<PartNames> parts = TrainPartDataContainer.GetTrainPart(id);
        foreach (PartNames name in parts)
        {
            switch (name)
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
                default: break;
            }
        }
    }
    /// <summary>
    /// 使所有车厢配件无效化
    /// </summary>
    public void DisableAllParts()
    {
        foreach (PartBase trainPart in trainParts)
        {
            trainPart.enabled = false;
        }
    }

    //资源采集速度
    private float gatheringSpeed;
    public float GetGatheringSpeed() => gatheringSpeed;
    public void SetGatheringSpeed(float speed) { gatheringSpeed = speed; }

    private void Start()
    {

        //TODO:初始化车厢血量和资源采集速度

        LoadParts();
    }
}
