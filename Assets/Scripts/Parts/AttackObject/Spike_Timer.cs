using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike_Timer : MonoBehaviour
{
    [Header("¥Ê‘⁄ ±º‰")]
    [SerializeField]
    private float time;

    private float t;
    private Bullet spike;

    void Start()
    {
        t = 0;
        spike = gameObject.GetComponent<Bullet>();
    }

    void Update()
    {
        t+= Time.deltaTime;
        if (t > time)
        {
            spike.DestroyGameObject();
        }
    }
}
