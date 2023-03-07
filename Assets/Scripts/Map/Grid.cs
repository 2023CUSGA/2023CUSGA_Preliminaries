using System.Collections;
using System.Collections.Generic;
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
    WorldObject[,] gridArry;
    Vector3 offsest;
    Transform owner;
    List<GameObject> objList = new List<GameObject>();

    WorldObject[] worldObjects;

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
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }

    Vector3 GetWorldPosition(int x, int y) => new Vector3(x, y) * cellSize;
    
    public void ShowGridInfo()
    {

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
        WorldObject[] worldObjects = new WorldObject[100];
        for (int i = 0; i < 100; i++)
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

        return worldObjects;
    }


    void CreateResPoint(int i)
    {
        int x = i / width;
        int y = i % height;
        if (gridArry[x, y] == WorldObject.None)
        {
            gridArry[x, y] = WorldObject.ResourcePoint;
            Utils.CreateWorldText("Res", owner, GetWorldPosition(x, y) + offsest * 0.5f, 30, objList,Color.yellow, TextAnchor.MiddleCenter);
        }
    }
    void CreateObstacle1(int i)
    {
        int x = i / width;
        int y = i % height;
        if (gridArry[x, y] == WorldObject.None)
        {
            gridArry[x, y] = WorldObject.Obstacle1;
            Utils.CreateWorldText("1", owner, GetWorldPosition(x, y) + offsest * 0.5f, 30, objList, Color.white, TextAnchor.MiddleCenter);
        }
    }
    void CreateObstacle2(int i)
    {
        int x1 = i / width;
        int x2 = x1 +1;
        int y1 = i % height;
        int y2 = y1 + 1;
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
            Utils.CreateWorldText("2", owner, GetWorldPosition(x1, y1) + offsest * 0.5f, 30, objList, Color.red, TextAnchor.MiddleCenter);
            Utils.CreateWorldText("2", owner, GetWorldPosition(x1, y2) + offsest * 0.5f, 30, objList, Color.red, TextAnchor.MiddleCenter);
            Utils.CreateWorldText("2", owner, GetWorldPosition(x2, y1) + offsest * 0.5f, 30, objList, Color.red, TextAnchor.MiddleCenter);
            Utils.CreateWorldText("2", owner, GetWorldPosition(x2, y2) + offsest * 0.5f, 30, objList, Color.red, TextAnchor.MiddleCenter);

        }
    }
    void CreateObstacle3(int i)
    {
        int x1 = i / width;
        int x2 = x1 + 1;
        int x3 = x2 + 1;
        int y1 = i % height;
        int y2 = y1 + 1;
        int y3 = y2 + 1;
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
            Utils.CreateWorldText("3", owner, GetWorldPosition(x1, y1) + offsest * 0.5f, 30, objList, Color.green, TextAnchor.MiddleCenter);
            Utils.CreateWorldText("3", owner, GetWorldPosition(x1, y2) + offsest * 0.5f, 30, objList, Color.green, TextAnchor.MiddleCenter);
            Utils.CreateWorldText("3", owner, GetWorldPosition(x1, y3) + offsest * 0.5f, 30, objList, Color.green, TextAnchor.MiddleCenter);
            Utils.CreateWorldText("3", owner, GetWorldPosition(x2, y1) + offsest * 0.5f, 30, objList, Color.green, TextAnchor.MiddleCenter);
            Utils.CreateWorldText("3", owner, GetWorldPosition(x2, y2) + offsest * 0.5f, 30, objList, Color.green, TextAnchor.MiddleCenter);
            Utils.CreateWorldText("3", owner, GetWorldPosition(x2, y3) + offsest * 0.5f, 30, objList, Color.green, TextAnchor.MiddleCenter);
            Utils.CreateWorldText("3", owner, GetWorldPosition(x3, y1) + offsest * 0.5f, 30, objList, Color.green, TextAnchor.MiddleCenter);
            Utils.CreateWorldText("3", owner, GetWorldPosition(x3, y2) + offsest * 0.5f, 30, objList, Color.green, TextAnchor.MiddleCenter);
            Utils.CreateWorldText("3", owner, GetWorldPosition(x3, y3) + offsest * 0.5f, 30, objList, Color.green, TextAnchor.MiddleCenter);

        }
    }

}
