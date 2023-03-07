using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    private static TrainManager instance;  //�𳵹�����������ģʽ
    [SerializeField]private int trainLength;  //�𳵳���
    private Route route;  //ת����Ϣ
    public event Action onRouteSetted;  //��ͷ����·����Ϣʱ���¼�

    private void Awake()
    {
        instance = this;  //����ģʽ
    }

    public static TrainManager GetInstance()  //����ģʽ
    {
        return instance;
    }

    public void SetRoute(Route new_route)  //route Set����
    {
        this.route = new_route;
        onRouteSetted();
    }

    public Route GetRoute()  //route Get����
    {
        return this.route;
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
