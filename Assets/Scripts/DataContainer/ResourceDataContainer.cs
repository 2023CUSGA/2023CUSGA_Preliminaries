using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourseNames : int  //资源名称及其对应的编号
{
    木材 = 0,
    石材 = 1,
    铜块 = 2,
    铁块 = 3,
    合金 = 4,
    金币 = 5,
}

[Serializable]
public static class ResourceDataContainer
{
    //五种资源和金币数量的存储容器
    private static List<int> resoursesList = new List<int>() { 0, 0, 0, 0, 0, 0 };
    public static int GetResourceQuantity(ResourseNames name) => resoursesList[(int)name];
    public static void SetResourceQuantity(ResourseNames name,int number) { resoursesList[(int)name] = number; }
}
