using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBase : MonoBehaviour 
{
    [Header("生成时间间隔")] public int spawnSpace = 5;
    [Header("生成时间改变的时间间隔")] public int spawnDeltaSpace = 30;
    private float timerOfSpace;
    private float timerOfDeltaSpace;

    [Header("生成物种类")] public GameObject[] spawnObjects;
    [Header("当前的生成个数")] public int defualtSpawnCount = 5;
    [Header("每次增加的生成个数")] public int deltaSpawnCount = 2;


    public void ChangeSpawnCount()
    {
        defualtSpawnCount += deltaSpawnCount;
    }
    
    public virtual void SpawnPrefab()
    {
        for (int i = 0; i < defualtSpawnCount; i++)
        {
            int index = Random.Range(0, spawnObjects.Length);

            PoolManager.Release(spawnObjects[index], transform.position);
        }
    }



    private void Update()
    {
        UpdateFunc();

        if (timerOfDeltaSpace >= spawnDeltaSpace)   // 改变刷怪点的刷怪数量
        {
            timerOfDeltaSpace = 0;
            ChangeSpawnCount();
        }
        timerOfDeltaSpace += Time.deltaTime;

        if (timerOfSpace >= spawnSpace)     // 刷怪
        {
            timerOfSpace = 0;
            SpawnPrefab();
        }
        timerOfSpace += Time.deltaTime;
    }

    public virtual void UpdateFunc()
    {

    }



}
