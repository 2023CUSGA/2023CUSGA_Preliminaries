using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    private static TrainManager instance;  //火车管理器，单例模式
    public GameObject trainhead;  //火车头预制体
    public GameObject trainbody;  //火车身预制体
    [SerializeField]private int trainLength;  //火车长度

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
}
