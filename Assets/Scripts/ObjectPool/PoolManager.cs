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

    private void Initialize(Pool[] pools)
    {
        foreach (var pool in pools)
        {
#if UNITY_EDITOR
            if (poolDic.ContainsKey(pool.prefab))
            {
                Debug.LogError("�ֵ���ļ��ظ���:" + pool.prefab.name);
                continue;
            }
#endif
            poolDic.Add(pool.prefab, pool);
            Transform poolParent = new GameObject("Pool:" + pool.prefab.name).transform;

            poolParent.parent = transform;
            pool.Initialize(poolParent);
        }
    }

    public static GameObject Release(GameObject prefab)
    {
#if UNITY_EDITOR
        if (!poolDic.ContainsKey(prefab))
        {
            Debug.LogError("û���ҵ���Ԥ����Ķ����" + prefab.name);
            return null;
        }
#endif
        return poolDic[prefab].PreparedObject();
    }

    public static GameObject Release(GameObject prefab, Vector3 position)
    {
#if UNITY_EDITOR
        if (!poolDic.ContainsKey(prefab))
        {
            Debug.LogError("û���ҵ���Ԥ����Ķ����" + prefab.name);
            return null;
        }
#endif
        return poolDic[prefab].PreparedObject(position);
    }




}
