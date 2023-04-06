using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResoursesManager : MonoBehaviour
{
    private static ResoursesManager instance;  //单例模式

    //资源管理
    private List<int> resoursesList;  //资源数量list
    public Action OnResourseNumberChanged;  //资源数量改变事件

    void Awake()
    {
        instance = this;
        if (resoursesList == null)
            resoursesList = new List<int>() { 0,0,0,0,0,0};
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadFromLocal();
        OnResourseNumberChanged();
    }

    public int GetResoursesNumber(string name)  //返回资源数量
    {
        return resoursesList[(int)Enum.Parse(typeof(ResourseNames), name)];
    }

    public void AddResourses(string name,int num)  //资源数量的增减
    {
        resoursesList[(int)Enum.Parse(typeof(ResourseNames), name)] += num;
        OnResourseNumberChanged();
    }

    private void LoadFromLocal()  //从本地存档读取数据
    {
        //从本地存档读取数据
    }

    public static ResoursesManager GetInstance()  //单例模式
    {
        return instance;
    }
}
