using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    private static TrainManager instance;  //火车管理器，单例模式
    [SerializeField]private int trainLength;  //火车长度

    //资源管理
    private int numberOfReousrses1 = 0;
    private int numberOfResourses2 = 0;

    private void Awake()
    {
        instance = this;  //单例模式
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
}
