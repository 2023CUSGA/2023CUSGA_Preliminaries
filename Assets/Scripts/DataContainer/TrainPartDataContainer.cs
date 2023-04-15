using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//配件名称枚举
[Serializable]
public enum PartNames
{
    //武器
    弓弩,
    火焰喷射器,
    石弹枪,
    地刺,
    炸药,
    电击枪,
    连发炮,
    化学迷雾,
    //采集
    采集架,
    采集强化器,
    超采集器,
    //防御
    木制挡板,
    铁皮挡板,
    合金挡板,
    //灯光
    小型探照灯,
    中型探照灯,
    大探照灯,
}

//火车配件数据存储
[Serializable]
public static class TrainPartDataContainer
{
    //火车车厢数
    private static int trainLength = 0;
    public static int GetTrainLength() => trainLength;
    public static void SetTrainLength(int length) { trainLength = length; }

    //车厢配件

    //存储了所有车厢的配件列表的集合
    private static List<List<PartNames>> trainParts = new List<List<PartNames>>();

    /// <summary>
    /// 获取存储了所有车厢的配件列表的集合
    /// </summary>
    public static List<List<PartNames>> GetTrainParts() => trainParts;

    /// <summary>
    /// 获取某一车厢的配件列表
    /// </summary>
    /// <param name="index">车厢序号</param>
    /// <returns></returns>
    public static List<PartNames> GetTrainPart(int index)
    {
        if (index >= trainLength)   return null;
        else    return trainParts[index];
    }

    /// <summary>
    /// 向集合末尾插入一个新的空车厢
    /// </summary>
    public static void AddTrainPart()
    {
        trainParts.Add(new List<PartNames>());
        trainLength++;
    }

    /// <summary>
    /// 更新某一车厢的配件列表
    /// </summary>
    /// <param name="trainPart">配件列表</param>
    /// <param name="index">车厢序号</param>
    public static void ModifyTrainPart(List<PartNames> trainPart,int index) { trainParts[index] = trainPart; }
}
