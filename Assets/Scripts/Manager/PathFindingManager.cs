using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingManager : MonoBehaviour
{
    public static PathFindingManager instance;

    public int width = 25;
    public int height = 15;

    Vector3Int[] offsets;
    public int[,] gridArry;


    private void Awake()
    {
        instance = this;
        instance.offsets = new Vector3Int[] { new Vector3Int(1, 0), new Vector3Int(0, -1), 
            new Vector3Int(-1, 0), new Vector3Int(0, 1) };    // 相邻格子的偏移量
    }

    public int manhattan(Vector3Int a, Vector3Int b)    // 曼哈顿算法
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    public Stack<Vector3Int> IAStar(EnemyBase enemy, Vector3 start, Vector3 end)
    {
        Vector3Int startPoint = Vector3Int.RoundToInt(start);
        Vector3Int endPoint = Vector3Int.RoundToInt(end);
        List<Vector3Int> sortList = new List<Vector3Int>();
        Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();

        sortList.Add(startPoint);
        cameFrom[startPoint] = new Vector3Int(-1, -1);
        bool hasRoute = false;
        while (sortList.Count > 0)
        {
            sortList.Sort((a, b) =>
            {
                return (manhattan(startPoint, a) + manhattan(endPoint, a)) - (manhattan(startPoint, b) + manhattan(endPoint, b));
            });
            Vector3Int current = sortList[0];
            sortList.RemoveAt(0);

            // 找到目标位置
            if (current == endPoint)
            {
                hasRoute = true;
                break;
            }
            // 四个相邻方向
            foreach (Vector3Int offset in offsets)
            {
                Vector3Int newPos = current + offset;
                // 超出边界
                if (newPos.x < 0 || newPos.y < 0 || newPos.x >= gridArry.GetLength(0) || newPos.y >= gridArry.GetLength(1))
                {
                    continue;
                }
                // 已经在队列中
                if (cameFrom.ContainsKey(newPos))
                {
                    continue;
                }
                // 障碍物
                if (gridArry[newPos.x, newPos.y] == 2)
                {
                    continue;
                }
                // 把相邻方向格子加进队列
                sortList.Add(newPos);
                // newPos 是由于 current 添加进来的，因此current是newPos的父节点
                cameFrom[newPos] = current;
            }
        }

        Stack<Vector3Int> trace = new Stack<Vector3Int>();

        if (hasRoute)
        {
            Vector3Int pos = endPoint;
            while (cameFrom.ContainsKey(pos))
            {
                trace.Push(pos);
                pos = cameFrom[pos];
            }

            return trace;
        }
        return null;
    }











}
