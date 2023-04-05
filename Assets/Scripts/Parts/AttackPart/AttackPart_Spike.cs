using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPart_Spike : AttackPartBase
{
    private GameObject prefeb;

    private void Start()
    {
        prefeb = prefebs[0];
    }

    protected override void Attack()
    {
        Vector3 p0 = transform.position;
        float angle = transform.rotation.z;
        Vector3 p = Quaternion.Euler(0, 0, angle) * transform.up;
        //Instantiate(prefeb, p0 + 1f * p,Quaternion.identity);
        //Instantiate(prefeb, p0 + 1.5f * p, Quaternion.identity);
        //Instantiate(prefeb, p0 + 2f * p, Quaternion.identity);
        PoolManager.Release(prefeb, p0 + 1f * p, Quaternion.identity);
        PoolManager.Release(prefeb, p0 + 1.5f * p, Quaternion.identity);
        PoolManager.Release(prefeb, p0 + 2f * p, Quaternion.identity);
    }
}
