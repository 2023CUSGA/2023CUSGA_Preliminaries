using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike_Timer : MonoBehaviour
{
    [Header("存在时间")]
    [SerializeField]
    private float time;

    private float t;
    private Bullet spike;

    private void Start()
    {
        spike = gameObject.GetComponent<Bullet>();
    }

    private void OnEnable()
    {
        t = 0;
    }

    private void Update()
    {
        t+= Time.deltaTime;
        if (t > time)
        {
            spike.DestroyGameObject();
        }
    }
}
