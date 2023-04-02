using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
	public static EnvironmentManager instance;

	public float fogTime;
	public float rainningTime;
	public float enemyTideTime;
    
    public List<EnemyBase> enemysList;
    public List<EnemySpawner> spawnerList;
	public float enemySpeedRatio;
	public float enemyAtkRatio;

	void Awake()
	{
        instance = this;
        instance.enemysList = new List<EnemyBase>();
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



}
