using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    public static List<int> GetAllResource() => resoursesList;
    public static int GetResourceQuantity(ResourseNames name) => resoursesList[(int)name];
    public static void SetResourceQuantity(ResourseNames name,int number) { resoursesList[(int)name] = number; }
    public static void IncreaseResourceQuantity(ResourseNames name, int number) { resoursesList[(int)name] += number; }
    public static void DecreaseResourceQuantity(ResourseNames name, int number) { resoursesList[(int)name] -= number; }

    //图纸数量存储容器
    private static Dictionary<PartNames, int> blueprints = new Dictionary<PartNames, int>()
    {
        //武器
        {PartNames.弓弩,0 },
        {PartNames.火焰喷射器,0 },
        {PartNames.石弹枪,0 },
        {PartNames.地刺,0 },
        {PartNames.炸药,0 },
        {PartNames.电击枪,0 },
        {PartNames.连发炮,0 },
        {PartNames.化学迷雾,0 },
        //采集
        {PartNames.采集架,0 },
        {PartNames.采集强化器,0 },
        {PartNames.超采集器,0 },
        //防御
        {PartNames.木制挡板,0 },
        {PartNames.铁皮挡板,0 },
        {PartNames.合金挡板,0 },
        //灯光
        {PartNames.小型探照灯,0 },
        {PartNames.中型探照灯,0 },
        {PartNames.大探照灯,0 }
    };
    public static Dictionary<PartNames, int> GetAllBlueprints() => blueprints;
    public static int GetBlueprintCount(PartNames name) => blueprints[name];
    public static void SetBlueprintCount(PartNames name,int count) { blueprints[name] = count; }
    public static void IncreaseBlueprintCount(PartNames name, int count) { blueprints[name] += count; }
    public static void DecreaseBlueprintCount(PartNames name, int count) { blueprints[name] -= count; }

    //背包配件存储容器
    private static Dictionary<PartNames, int> parts = new Dictionary<PartNames, int>()
    {
        //武器
        {PartNames.弓弩,0 },
        {PartNames.火焰喷射器,0 },
        {PartNames.石弹枪,0 },
        {PartNames.地刺,0 },
        {PartNames.炸药,0 },
        {PartNames.电击枪,0 },
        {PartNames.连发炮,0 },
        {PartNames.化学迷雾,0 },
        //采集
        {PartNames.采集架,0 },
        {PartNames.采集强化器,0 },
        {PartNames.超采集器,0 },
        //防御
        {PartNames.木制挡板,0 },
        {PartNames.铁皮挡板,0 },
        {PartNames.合金挡板,0 },
        //灯光
        {PartNames.小型探照灯,0 },
        {PartNames.中型探照灯,0 },
        {PartNames.大探照灯,0 }
    };
    public static Dictionary<PartNames, int> GetAllParts() => parts;
    public static int GetPartCount(PartNames name)=> parts[name];
    public static void SetPartCount(PartNames name,int count) { parts[name] = count; }
    public static void IncreasePartCount(PartNames name, int count) { parts[name] += count; }
    public static void DecreasePartCount(PartNames name, int count) { parts[name] -= count; }
}
