using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridTest : MonoBehaviour
{
    Grid grid;
    public GameObject Obstacle1;
    public GameObject Obstacle2;
    public GameObject Obstacle3;
    public GameObject ResPoint;
    [Header("敌人种类")] public GameObject[] spawnObjects;
    public int spawnCount;

    private void Start()
    {
        grid = new Grid(25, 15, 1f, this.transform);
        SpawnEnemy();
    }

    public void ClickShuffleButton()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        grid.ShowGridInfo(Obstacle1, Obstacle2, Obstacle3, ResPoint);
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            int index = Random.Range(0, spawnObjects.Length);
            int x = Random.Range(0, 26);
            int y = Random.Range(0, 16);
            PoolManager.Release(spawnObjects[index], grid.GetWorldPosition(x, y));
        }
    }






}
