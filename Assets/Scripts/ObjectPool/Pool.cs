using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Pool
{
    public GameObject prefab;
    [SerializeField] int size = 1;
    public int Size => size;
    public int RuntimeSize => queue.Count;

    Queue<GameObject> queue;
    Transform parent;

    public void Initialize(Transform parent)
    {
        queue = new Queue<GameObject>();
        this.parent = parent;

        for (int i = 0; i < size; i++)
        {
            queue.Enqueue(Copy());
        }
    }
    
    GameObject Copy()
    {
        var copy = GameObject.Instantiate(prefab, parent);
        copy.SetActive(false);

        return copy;
    }
    /// <summary>
    /// 在队列中找一个可用对象
    /// </summary>
    /// <returns></returns>
    GameObject AvailableObject()
    {
        GameObject availableObject = null;
        if (queue.Count > 0 && !queue.Peek().activeSelf)    // 有可用的就出列
        {
            availableObject = queue.Dequeue();
        }
        else
        {
            availableObject = Copy();       // 没有可用的就再复制一个
        }
        queue.Enqueue(availableObject);

        return availableObject;
    }
    /// <summary>
    /// 外部调用获取对象
    /// </summary>
    public GameObject PreparedObject()
    {
        GameObject preparedObject = AvailableObject();

        preparedObject.SetActive(true);

        return preparedObject;
    }
    /// <summary>
    /// 外部调用获取对象，在指定位置生成
    /// </summary>
    public GameObject PreparedObject(Vector3 position)
    {
        GameObject preparedObject = AvailableObject();

        preparedObject.SetActive(true);
        preparedObject.transform.position = position;

        return preparedObject;
    }
    /// <summary>
    /// 外部调用获取对象，在指定位置，指定旋转生成
    /// </summary>
    public GameObject PreparedObject(Vector3 position, Quaternion rotation)
    {
        GameObject preparedObject = AvailableObject();

        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;

        return preparedObject;
    }


    public void Return(GameObject gameObject) => queue.Enqueue(gameObject);
    



}
