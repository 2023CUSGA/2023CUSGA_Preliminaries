using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnvironmentManager : MonoBehaviour
{
	public static EnvironmentManager instance;
    int levelNum;   // 关卡数

	public float fogTime;
	public float rainningTime;
	public float enemyTideTime;
    
    public List<EnemyBase> enemysList;
    public List<EnemySpawner> spawnerList;
	public float enemySpeedRatio;
	public float enemyAtkRatio;

    public float tideTimer;
    public float deltaTideTime;
    int tideCount;
    public int enemyBaseCount;
    int enemyMinCount;
    int enemyMaxCount;

    public Action gameWin;
    public Action gameLose;
    public Action<int> addGold;

    public int enemyKillNum;
    public int EnemyKillNum
    {
        get { return enemyKillNum; }
        set
        {
            enemyKillNum = value;
            int rand = UnityEngine.Random.Range(5, 9);
            ResourceDataContainer.IncreaseResourceQuantity(ResourseNames.金币, rand);

            addGold?.Invoke(rand);
        }
    }

	void Awake()
	{
        instance = this;
        instance.enemysList = new List<EnemyBase>();
        CheckEnemyTideInfo();
    }

    private void Update()
    {
        TideController();
    }

    public void Fog()
	{
		TrainManager.GetInstance().SetIsPerversion(true);
		StartCoroutine(WaitForFog(fogTime));
	}

	IEnumerator WaitForFog(float time)
	{
		yield return new WaitForSeconds(time);
        TrainManager.GetInstance().SetIsPerversion(false);
    }

    public void Rainning()
    {
        //FindObjectOfType<>	// 找到控制武器的脚本
        StartCoroutine(WaitForRainning(fogTime));
    }

    IEnumerator WaitForRainning(float time)
    {
        yield return new WaitForSeconds(time);
        // 传入控制脚本来恢复伤害值

    }

	public void Earthquake()
	{
        //FindObjectOfType<>	// 找到所有列车脚本
		// 扣血
    }


    void CheckEnemyTideInfo()
    {
        if (!PlayerPrefs.HasKey("levelNum"))
        {
            levelNum = 1;
            PlayerPrefs.SetInt("levelNum", levelNum);
        }
        levelNum = PlayerPrefs.GetInt("levelNum");


        switch (levelNum)
        {
            case 1:
                tideCount = 2;
                enemyBaseCount = 23;
                enemyMinCount = 5;
                enemyMaxCount = 5;
                break;
            case 2:
                tideCount = 4;
                enemyBaseCount = 25;
                enemyMinCount = 5;
                enemyMaxCount = 9;
                break;
            case 3:
                tideCount = 5;
                enemyBaseCount = 30;
                enemyMinCount = 6;
                enemyMaxCount = 8;
                break;
            case 4:
                tideCount = 7;
                enemyBaseCount = 35;
                enemyMinCount = 6;
                enemyMaxCount = 10;
                break;
            case 5:
                tideCount = 8;
                enemyBaseCount = 35;
                enemyMinCount = 7;
                enemyMaxCount = 10;
                break;
            case 6:
                tideCount = 9;
                enemyBaseCount = 40;
                enemyMinCount = 10;
                enemyMaxCount = 10;
                break;
            case 7:
                tideCount = 10;
                enemyBaseCount = 50;
                enemyMinCount = 10;
                enemyMaxCount = 10;
                break;
            default:
                tideCount = 10;
                enemyBaseCount = 50;
                enemyMinCount = 10;
                enemyMaxCount = 10;
                break;
        }
    }

    public void TideController()
    {
        if (tideTimer >= deltaTideTime)
        {
            tideTimer = 0;
            tideCount--;
            if (tideCount < 0)
            {
                CheckGameOver();
                return;
            }
            else
                SpawnEnemy(enemyMinCount, enemyMaxCount);
        }
        tideTimer += Time.deltaTime;

    }

    void SpawnEnemy(int minCout, int maxCout)
    {
        int randomCount = Random.Range(minCout, maxCout + 1);
        for (int i = 0; i < randomCount; i++)
        {
            int randomSpawner = Random.Range(0, spawnerList.Count);
            spawnerList[randomSpawner].SpawnPrefab();
        }
    }

	public void EnemyTide()
	{
		foreach (var enemy in enemysList)
		{
			enemy.Decelerate(enemySpeedRatio);
			enemy.AtkDown(enemyAtkRatio);
		}

        foreach (var item in spawnerList)
        {
            item.SpawnPrefab(5,6);
        }

		StartCoroutine(WaitForTide(enemyTideTime));
    }
    IEnumerator WaitForTide(float time)
    {
        yield return new WaitForSeconds(time);

        foreach (var enemy in enemysList)
        {
            enemy.NormalSpeed();
            enemy.AtkNormal();
        }
    }

    void CheckGameOver()
    {
        if (enemysList.Count < 10)
        {
            EnemyKillNum = 0;
            gameWin?.Invoke();
        }

    }

}
