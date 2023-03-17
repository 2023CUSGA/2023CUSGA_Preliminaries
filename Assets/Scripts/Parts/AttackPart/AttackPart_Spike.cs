using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPart_Spike : AttackPartBase
{
    [Header("地刺预制体")]
    [SerializeField]
    private GameObject prefeb;

    protected override void Attack()
    {
        Vector3 p0 = transform.position;
        float angle = transform.rotation.z;
        Vector3 p = Quaternion.Euler(0, 0, angle) * transform.right;
        Instantiate(prefeb, p0 + 1f * p,Quaternion.identity);
        Instantiate(prefeb, p0 + 1.5f * p, Quaternion.identity);
        Instantiate(prefeb, p0 + 2f * p, Quaternion.identity);
    }
}
