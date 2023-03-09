using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    private static TrainManager instance;  //�𳵹�����������ģʽ
    [SerializeField]private int trainLength;  //�𳵳���

    //��Դ����
    private int numberOfReousrses1 = 0;
    private int numberOfResourses2 = 0;

    private void Awake()
    {
        instance = this;  //����ģʽ
    }

    public static TrainManager GetInstance()  //����ģʽ
    {
        return instance;
    }

    public int GetTrainLength()  //trainLength Get����
    {
        return this.trainLength;
    }

    public void SetTrainLength(int length)  //trainLength Set����
    {
        this.trainLength = length;
    }
}
