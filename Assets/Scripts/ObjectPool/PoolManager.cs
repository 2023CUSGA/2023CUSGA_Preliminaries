using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] Pool[] pools;

    static Dictionary<GameObject, Pool> poolDic;

    private void Start()
    {
        poolDic = new Dictionary<GameObject, Pool>();
        Initialize(pools);
    }
    /// <summary>
    /// 初始化各种池子
    /// </summary>
    private void Initialize(Pool[] pools)
    {
        foreach (var pool in pools)
        {
#if UNITY_EDITOR
            if (poolDic.ContainsKey(pool.prefab))
            {
                Debug.LogError("字典里的键重复了:" + pool.prefab.name);
                continue;
            }
#endif
            poolDic.Add(pool.prefab, pool);
            Transform poolParent = new GameObject("Pool:" + pool.prefab.name).transform;

            poolParent.parent = transform;
            pool.Initialize(poolParent);
        }
    }
    /// <summary>
    /// 给其他脚本提供生成对象的方法
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab)
    {
#if UNITY_EDITOR
        if (!poolDic.ContainsKey(prefab))
        {
            Debug.LogError("没有找到该预制体的对象池" + prefab.name);
            return null;
        }
#endif
        return poolDic[prefab].PreparedObject();
    }

    /// <summary>
    /// 给其他脚本提供生成对象的方法，可指定位置
    /// </summary>
    public static GameObject Release(GameObject prefab, Vector3 position)
    {
#if UNITY_EDITOR
        if (!poolDic.ContainsKey(prefab))
        {
            Debug.LogError("没有找到该预制体的对象池" + prefab.name);
            return null;
        }
#endif
        return poolDic[prefab].PreparedObject(position);
    }




}
