using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBase : MonoBehaviour 
{
    public float moveSpeed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TouchEvent(collision);
    }

    /// <summary>
    /// 由子物体调用，看情况来调用【扣血】或者【拯救人数+1】方法
    /// </summary>
    public virtual void TouchEvent(Collider2D collision)
    {
        
    }

    private void Start()
    {

    }

    private void OnEnable()
    {
        Init();
    }

    /// <summary>
    /// 给子类进行初始化的方法
    /// </summary>
    public virtual void Init()
    {

    }

    private void Update()
    {
        UpdateFunc();
    }

    /// <summary>
    /// 给子类每帧调用的方法
    /// </summary>
    public virtual void UpdateFunc()
    {

    }
}
