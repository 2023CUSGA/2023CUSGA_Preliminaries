using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    private static TrainManager instance;  //火车管理器，单例模式
    [SerializeField]private int trainLength;  //火车长度
    private Route route;  //转向信息
    public event Action onRouteSetted;  //火车头发送路径信息时的事件

    private void Awake()
    {
        instance = this;  //单例模式
    }

    public static TrainManager GetInstance()  //单例模式
    {
        return instance;
    }

    public void SetRoute(Route new_route)  //route Set方法
    {
        this.route = new_route;
        onRouteSetted();
    }

    public Route GetRoute()  //route Get方法
    {
        return this.route;
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
