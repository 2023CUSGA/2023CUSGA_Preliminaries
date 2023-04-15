using Pathfinding;
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
    public GameObject Wastland;
    public GameObject snowyPlain;
    public GameObject Mountain;

    [Header("敌人种类")] public GameObject[] spawnObjects;
    public int spawnCount;
    public AstarPath Path;
    private void Start()
    {
        grid = new Grid(25, 15, 1f, this.transform);
        //PathFindingManager.instance.gridArry = PathFindingArray(grid.gridArry);
        spawnCount = EnvironmentManager.instance.enemyBaseCount;
        ClickShuffleButton();
    }

    public void ClickShuffleButton()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        grid.ShowGridInfo(Obstacle1, Obstacle2, Obstacle3, ResPoint, Wastland, snowyPlain, Mountain);
        Path.Scan();
        SpawnEnemy();
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

    int[,] PathFindingArray(WorldObject[,] gridArry)
    {
        int[,] pathArray = new int[gridArry.GetLength(0), gridArry.GetLength(1)];

        for (int i = 0; i < gridArry.GetLength(0); i++)
        {
            for (int j = 0; j < gridArry.GetLength(1); j++)
            {
                if (gridArry[i, j] == WorldObject.None && pathArray[i, j] != 2)
                    pathArray[i, j] = 1;    // 代表可达

                if (gridArry[i, j] == WorldObject.ResourcePoint || gridArry[i, j] == WorldObject.Obstacle1)
                {
                    pathArray[i, j] = 2;    // 代表不可达
                    pathArray[i + 1, j] = 2;
                    pathArray[i, j + 1] = 2;
                    pathArray[i + 1, j + 1] = 2;
                }

                if (gridArry[i, j] == WorldObject.Obstacle2)
                {
                    for (int k = i; k < i + 3; k++)
                    {
                        for (int l = j; l < j + 3; l++)
                        {
                            pathArray[k, l] = 2;    // 代表不可达
                        }
                    }
                }

                if (gridArry[i, j] == WorldObject.Obstacle3)
                {
                    for (int k = i; k < i + 4; k++)
                    {
                        for (int l = j; l < j + 4; l++)
                        {
                            pathArray[k, l] = 2;    // 代表不可达
                        }
                    }
                }
            }
        }
        return pathArray;

    }




}
