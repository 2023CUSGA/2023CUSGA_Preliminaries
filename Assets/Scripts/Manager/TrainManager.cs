using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainManager : MonoBehaviour
{
    private static TrainManager instance;  //火车管理器，单例模式
    public GameObject trainhead;  //火车头预制体
    public GameObject trainbody;  //火车身预制体
    public List<Sprite> trainhead_images = new List<Sprite>(4);  //火车头贴图list 0.普通 1.毁灭 2.弹力 3.迅捷
    private int energy = 0;  //火车头能量
    [SerializeField]private float timeCount_CrashEnermy = 0;
    [SerializeField]private int trainLength;  //火车长度
    private bool isPerversion = false;  //火车操控方向是否颠倒
    private bool isUpdated = false;  //火车头是否经过升级
    [SerializeField]private int trainHeadType = 0;  //火车头类型 0.普通 1.毁灭 2.弹力 3.迅捷
    private Coroutine energyLeakCoro;  //能量流失协程函数
    private GameObject upgradeCard;  //升级火车头的卡片UI
    private bool isInMaxPower = false; //是否为满能量状态

    private void Awake()
    {
        instance = this;  //单例模式
        upgradeCard = GameObject.FindGameObjectWithTag("UI_UpgradeCards");
        upgradeCard.SetActive(false);
        initTrain();
    }

    public static TrainManager GetInstance()  //单例模式
    {
        return instance;
    }

    public int GetTrainLength()  //trainLength Get方法
    {
        return this.trainLength;
    }

    public void SetTrainLength(int length)  //trainLength Set方法
    {
        this.trainLength = length;
    }

    public void SetIsPerversion(bool isPerversion)  //isPerversion Set方法
    {
        this.isPerversion = isPerversion;
    }

    public bool GetIsPerversion()  //isPerversion Get方法
    {
        return this.isPerversion;
    }

    public bool GetIsInMaxPower()  //IsInMaxPower Get方法
    {
        return isInMaxPower;
    }

    void initTrain()  //初始化火车
    {
        float nodeDistance = 0;
        Instantiate(trainhead, new Vector3(0,0,0),Quaternion.identity).name = "train_head";  //初始化火车头

        for(int i=0;i<trainLength-1;i++)  //初始化火车身
        {
            if (i == 0)
                nodeDistance -= 0.55f;
            else
                nodeDistance -= 0.57f;
            GameObject temp = Instantiate(trainbody, new Vector3(0, nodeDistance, 0), Quaternion.identity);
            temp.name = "train_body" + i.ToString();
            temp.GetComponent<TrainBody>().SetId(i);
        }
    }

    public void OnCrashEnermy()  //火车头撞击敌人
    {
        AddEnergy(1);
        timeCount_CrashEnermy = 0;
    }

    public int GetEnergy()  //energy Get方法
    {
        return this.energy;
    }

    public void SetEnergy(int i)  //energy Set方法
    {
        this.energy = i;
    }

    public void AddEnergy(int i=1)  //增加能量
    {
        if (!isInMaxPower)
        {
            if (energy == 0)  //能量从0开始获得：启动能量衰减计时器
            {
                timeCount_CrashEnermy = 0;
                energyLeakCoro = StartCoroutine(EnergyLeak());
            }
            if (energy + i <= 100) energy += i;
            else energy = 100;
        }

        if(energy>=80 && !isUpdated)  //能量超过80且未升级：升级
        {
            isUpdated = true;
            UpgradeTrainHead();
        }
        else if(energy == 100 && !isInMaxPower)  //满能量再次升级
        {
            isInMaxPower = true;
            StopCoroutine(energyLeakCoro);
            StartCoroutine(MaxPowerCounter());
        }
    }

    public void ReduceEnergy(int i=1)  //减少能量
    {
        if(i>energy) energy = 0;
        else energy -= i;

        if(energy == 0)  //能量衰减至0：停止能量衰减计时器
        {
            StopCoroutine(energyLeakCoro);
            timeCount_CrashEnermy = 0;
        }
        if(energy<80 && isUpdated)  //能量低于80：退化
        {
            isUpdated = false;
            DownGradeTrainHead();
        }
    }

    private void UpgradeTrainHead()  //火车头升级
    {
        upgradeCard.SetActive(true);
        Time.timeScale = 0;
    }  

    private void DownGradeTrainHead()  //火车头降级
    {
        SetTrainHeadType(0);
    }

    public void SetTrainHeadType(int i)  //trainHeadType Set方法
    {

        if (0 <= i && i <= 3)
        {
            this.trainHeadType = i;
            if(i==0)
            {
                GameObject.Find("train_head").GetComponent<SpriteRenderer>().sprite = trainhead_images[0];
                GameObject.Find("train_head").GetComponent<TrainheadMov>().SetMoveSpeed(3.0f);
            }
            if(i==1)  //设置毁灭车头的配置
            {
                GameObject.Find("train_head").GetComponent<SpriteRenderer>().sprite = trainhead_images[1];
                GameObject.Find("train_head").GetComponent<TrainheadMov>().SetMoveSpeed(2.0f);
            }
            if(i==2)  //设置弹力车头的配置
            {
                GameObject.Find("train_head").GetComponent<SpriteRenderer>().sprite = trainhead_images[2];
                GameObject.Find("train_head").GetComponent<TrainheadMov>().SetMoveSpeed(2.5f);
            }
            if(i==3)  //设置迅捷车头的配置
            {
                GameObject.Find("train_head").GetComponent<SpriteRenderer>().sprite = trainhead_images[3];
                GameObject.Find("train_head").GetComponent<TrainheadMov>().SetMoveSpeed(4.0f);
            }
        }
        else
            Debug.Log("trainHeadType Error: 0.普通 1.毁灭 2.弹力 3.迅捷");
        upgradeCard.SetActive(false);
        Time.timeScale = 1;
    }

    public int GetTrainHeadType()  //trainHeadType Get方法
    {
        return this.trainHeadType;
    }

    IEnumerator EnergyLeak()  //每秒流失能量函数
    {
        while(true)
        {
            timeCount_CrashEnermy += 1;
            if (timeCount_CrashEnermy > 3 && energy >= 0)
            {
                ReduceEnergy(1);
            }
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator MaxPowerCounter()  //能量满格计时器
    {
        yield return new WaitForSeconds(3.0f);
        energy = 80;
        timeCount_CrashEnermy = 0;
        energyLeakCoro = StartCoroutine(EnergyLeak());
        isInMaxPower = false;
    }
}
