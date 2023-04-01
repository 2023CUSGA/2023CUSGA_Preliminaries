using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBase : MonoBehaviour 
{

    [Header("生成物种类")] public GameObject[] spawnObjects;
    [Header("当前的生成个数")] public int defualtSpawnCount = 5;

    private void Start()
    {
        EnvironmentManager.instance.spawnerList.Add((EnemySpawner)this);
    }

    public virtual void SpawnPrefab(int regionLeft, int regionRight)
    {
        int spawnCount = Random.Range(regionLeft, regionRight + 1);
        for (int i = 0; i < spawnCount; i++)
        {
            int index = Random.Range(0, spawnObjects.Length);

            PoolManager.Release(spawnObjects[index], transform.position);
        }
    }


}
