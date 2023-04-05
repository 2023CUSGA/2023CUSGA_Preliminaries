using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
public enum WorldObject
{
    None = 0,
    ResourcePoint = 1,
    Obstacle1 = 2,
    Obstacle2 = 3,
    Obstacle3 = 4,
}

public class Grid
{
    int width;
    int height;
    float cellSize;
    public WorldObject[,] gridArry;
    Vector3 offsest;
    Transform owner;
    List<GameObject> objList = new List<GameObject>();

    WorldObject[] worldObjects;

    public GameObject Obstacle1;
    public GameObject Obstacle2;
    public GameObject Obstacle3;
    public GameObject ResPoint;

    public Grid(int width, int height, float cellSize, Transform owner)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.owner = owner;

        this.gridArry = new WorldObject[width, height];
        this.offsest = new Vector3(cellSize, cellSize);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                gridArry[x, y] = WorldObject.None;
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 10f);   // 画线
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 10f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 10f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 10f);
    }

    public Vector3 GetWorldPosition(int x, int y) => new Vector3(x, y) * cellSize;
    
    public void ShowGridInfo(GameObject obstacle1, GameObject obstacle2, GameObject obstacle3, GameObject ResPoint)
    {
        this.Obstacle1 = obstacle1;
        this.Obstacle2 = obstacle2;
        this.Obstacle3 = obstacle3;
        this.ResPoint = ResPoint;

        foreach (var item in objList)
        {
            item.SetActive(false);
        }
        objList.Clear();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                gridArry[x, y] = WorldObject.None;

            }
        }
        worldObjects = Shuffle();
        for (int i = 0; i < worldObjects.Length; i++)
        {
            WorldObject obj = worldObjects[i];
            switch (obj)
            {
                case WorldObject.None:
                    break;
                case WorldObject.ResourcePoint:
                    CreateResPoint(i);
                    break;
                case WorldObject.Obstacle1:
                    CreateObstacle1(i);
                    break;
                case WorldObject.Obstacle2:
                    CreateObstacle2(i);
                    break;
                case WorldObject.Obstacle3:
                    CreateObstacle3(i);
                    break;
            }
            
        }
    }
    public WorldObject[] Shuffle()
    {
        WorldObject[] worldObjects = new WorldObject[375];
        for (int i = 0; i < 375; i++)
        {
            if (i < 2)
            {
                worldObjects[i] = WorldObject.Obstacle3;
            }
            else if (i < 6)
            {
                worldObjects[i] = WorldObject.Obstacle2;
            }
            else if (i < 16)
            {
                worldObjects[i] = WorldObject.Obstacle1;
            }
            else if (i < 26)
            {
                worldObjects[i] = WorldObject.ResourcePoint;
            }
            else
                worldObjects[i] = WorldObject.None;
        }

        for (int i = 0; i < worldObjects.Length; i++)
        {
            WorldObject temp = worldObjects[i];
            int index = Random.Range(0, worldObjects.Length);
            worldObjects[i] = worldObjects[index];
            worldObjects[index] = temp;
        }

        return worldObjects;    // 返回一组洗好的牌
    }


    void CreateResPoint(int i)
    {
        int y = i / width;
        int x = i - y * width;
        if (gridArry[x, y] == WorldObject.None)
        {
            gridArry[x, y] = WorldObject.ResourcePoint;
            objList.Add(PoolManager.Release(ResPoint, GetWorldPosition(x, y)));
        }
    }
    void CreateObstacle1(int i)
    {
        int y = i / width;
        int x = i - y * width;

        if (gridArry[x, y] == WorldObject.None)
        {
            gridArry[x, y] = WorldObject.Obstacle1;
            objList.Add(PoolManager.Release(Obstacle1, GetWorldPosition(x, y)));
        }
    }
    void CreateObstacle2(int i)
    {
        int y1 = i / width;
        int y2 = y1 +1;
        int x1 = i - y1 * width;

        int x2 = y1 + 1;
        if (x1 > width - 2 || y1 > height - 2)
        {
            return;
        }
        if (gridArry[x1, y1] == WorldObject.None)
        {
            gridArry[x1, y1] = WorldObject.Obstacle2;
            gridArry[x1, y2] = WorldObject.Obstacle2;
            gridArry[x2, y1] = WorldObject.Obstacle2;
            gridArry[x2, y2] = WorldObject.Obstacle2;
            objList.Add(PoolManager.Release(Obstacle2, GetWorldPosition(x1, y1)));

        }
    }
    void CreateObstacle3(int i)
    {
        int y1 = i / width;
        int y2 = y1 + 1;
        int y3 = y2 + 1;
        int x1 = i - y1 * width;

        int x2 = x1 + 1;
        int x3 = x2 + 1;
        if (x1 > width - 3 || y1 > height - 3)
        {
            return;
        }
        if (gridArry[x1, y1] == WorldObject.None)
        {
            gridArry[x1, y1] = WorldObject.Obstacle3;
            gridArry[x1, y2] = WorldObject.Obstacle3;
            gridArry[x1, y3] = WorldObject.Obstacle3;
            gridArry[x2, y1] = WorldObject.Obstacle3;
            gridArry[x2, y2] = WorldObject.Obstacle3;
            gridArry[x2, y3] = WorldObject.Obstacle3;
            gridArry[x3, y1] = WorldObject.Obstacle3;
            gridArry[x3, y2] = WorldObject.Obstacle3;
            gridArry[x3, y3] = WorldObject.Obstacle3;
            objList.Add(PoolManager.Release(Obstacle3, GetWorldPosition(x1, y1)));
        }
    }

}
