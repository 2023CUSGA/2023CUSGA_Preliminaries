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

#if UNITY_EDITOR
    private void OnDestroy()
    {
        CheckPoolSize(pools);
    } 
#endif

    /// <summary>
    /// 检查每个池子初始化时的最大prefab数量
    /// </summary>
    /// <param name="pools"></param>
    void CheckPoolSize(Pool[] pools)
    {
        foreach (var pool in pools)
        {
            if (pool.RuntimeSize > pool.Size)
            {
                Debug.LogWarning(string.Format("Pool:{0} has a runtime size {1} bigger than its initial size{2}",
                    pool.prefab.name, pool.RuntimeSize, pool.Size));
            }
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

    /// <summary>
    /// 给其他脚本提供生成对象的方法，可指定位置，指定旋转
    /// </summary>
    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation)
    {
#if UNITY_EDITOR
        if (!poolDic.ContainsKey(prefab))
        {
            Debug.LogError("没有找到该预制体的对象池" + prefab.name);
            return null;
        }
#endif
        return poolDic[prefab].PreparedObject(position, rotation);
    }


}
