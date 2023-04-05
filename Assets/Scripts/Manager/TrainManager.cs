using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    private static TrainManager instance;  //火车管理器，单例模式
    public GameObject trainhead;  //火车头预制体
    public GameObject trainbody;  //火车身预制体
    private int energy = 0;  //火车头能量
    [SerializeField]private float timeCount_CrashEnermy = 0;
    [SerializeField]private int trainLength;  //火车长度
    private bool isPerversion = false;  //火车操控方向是否颠倒
    private bool isUpdated = false;  //火车头是否经过升级
    [SerializeField]private int trainHeadType = 0;  //火车头类型 0.普通 1.重装 2.弹力 3.迅捷
    private Coroutine energyLeakCoro;  //能量流失协程函数

    private void Awake()
    {
        instance = this;  //单例模式
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
        if(energy == 0)
        {
            timeCount_CrashEnermy = 0;
            energyLeakCoro =  StartCoroutine(EnergyLeak());
        }
        if(energy<100) energy+=i;
        if(energy>=80 && !isUpdated)
        {
            isUpdated = true;
            UpdateTrainHead();
        }
    }

    public void ReduceEnergy(int i=1)  //减少能量
    {
        if(i>energy) energy = 0;
        else energy -= i;
        if(energy == 0)
        {
            StopCoroutine(energyLeakCoro);
            timeCount_CrashEnermy = 0;
        }
        if(energy<80 && isUpdated)
        {
            isUpdated = false;
            DownGradeTrainHead();
        }
    }

    private void UpdateTrainHead()  //火车头升级
    {
        Debug.Log("火车头升级");
    }  

    private void DownGradeTrainHead()  //火车头降级
    {
        Debug.Log("火车头降级");
        trainHeadType = 0;
    }

    public void SetTrainHeadType(int i)  //trainHeadType Set方法
    {
        if (0 <= i && i <= 3)
            this.trainHeadType = i;
        else
            Debug.Log("trainHeadType Error: 0.普通 1.重装 2.弹力 3.迅捷");
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
}
