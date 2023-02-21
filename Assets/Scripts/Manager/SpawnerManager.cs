using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
	public EnemySpawner[] enemySpawners;

	public float spawnerChangeTime = 10f;
	public float spawnerChangeDeltaTime = 10f;
	public float timerOfChangeTime;
	public float timerOfChangeDeltaTime;
	

    void Awake()
	{
		Shuffle();
		SetOneSpawnerActive();
    }

	private void Update()
	{
        if (timerOfChangeDeltaTime >= spawnerChangeDeltaTime)	// 刷怪点出现间隔的变化值
        {
            timerOfChangeDeltaTime = 0;
            spawnerChangeTime += spawnerChangeDeltaTime;
        }
        timerOfChangeDeltaTime += Time.deltaTime;

        if (timerOfChangeTime >= spawnerChangeTime)		// 刷怪点的出现间隔
		{
            timerOfChangeTime = 0;
            SetOneSpawnerActive();
        }
        timerOfChangeTime += Time.deltaTime;


    }

	/// <summary>
	/// 初始化刷怪点顺序
	/// </summary>
	public void Shuffle()
	{
		for (int i = 0; i < enemySpawners.Length; i++)
		{
			EnemySpawner temp = enemySpawners[i];
            int index = Random.Range(0, enemySpawners.Length);
			enemySpawners[i] = enemySpawners[index];
			enemySpawners[index] = temp;
        }
    }

	/// <summary>
	/// 激活一个刷怪点
	/// </summary>
	public void SetOneSpawnerActive()
	{
		for (int i = 0; i < enemySpawners.Length; i++)
		{
			if (!enemySpawners[i].gameObject.activeInHierarchy)
			{
				enemySpawners[i].gameObject.SetActive(true);
				return;
            }
		}
	}







}
