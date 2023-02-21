using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBase : MonoBehaviour 
{
    [Header("����ʱ����")] public int spawnSpace = 5;
    [Header("����ʱ��ı��ʱ����")] public int spawnDeltaSpace = 30;
    private float timerOfSpace;
    private float timerOfDeltaSpace;

    [Header("����������")] public GameObject[] spawnObjects;
    [Header("��ǰ�����ɸ���")] public int defualtSpawnCount = 5;
    [Header("ÿ�����ӵ����ɸ���")] public int deltaSpawnCount = 2;


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

        if (timerOfDeltaSpace >= spawnDeltaSpace)   // �ı�ˢ�ֵ��ˢ������
        {
            timerOfDeltaSpace = 0;
            ChangeSpawnCount();
        }
        timerOfDeltaSpace += Time.deltaTime;

        if (timerOfSpace >= spawnSpace)     // ˢ��
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
