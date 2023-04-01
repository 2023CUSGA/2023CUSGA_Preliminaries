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
    private float timeCount_CrashEnermy = 0;
    [SerializeField]private int trainLength;  //火车长度
    private bool isPerversion = false;  //火车操控方向是否颠倒
    private bool isUpdated = false;  //火车头是否经过升级

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
            Instantiate(trainbody, new Vector3(0, nodeDistance, 0), Quaternion.identity).name = "train_body"+i.ToString();
            TrainBody[] array = GameObject.Find("train_body" + i.ToString()).GetComponents<TrainBody>();
            foreach (var p in array)
            {
                p.SetId(i);
            }
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

    public void AddEnergy(int i=1)  //增加能量
    {
        if(energy == 0)
        {
            StartCoroutine(EnergyLeak());
        }
        energy+=i;
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
            StopCoroutine(EnergyLeak());
            timeCount_CrashEnermy = 0;
        }
        if(energy<80 && isUpdated)
        {
            isUpdated = false;
            DownGradeTrainHead();
        }
    }

    private void UpdateTrainHead()
    {
        Debug.Log("火车头升级");
    }  //火车头升级

    private void DownGradeTrainHead()
    {
        Debug.Log("火车头降级");
    }  //火车头降级

    IEnumerator EnergyLeak()  //每秒流失能量函数
    {
        timeCount_CrashEnermy += 1;
        if(timeCount_CrashEnermy>=3 && energy>=0)
        {
            ReduceEnergy(1);
        }
        yield return new WaitForSeconds(1);
    }
}
