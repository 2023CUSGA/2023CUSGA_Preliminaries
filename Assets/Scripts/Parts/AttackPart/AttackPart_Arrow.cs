using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPart_Arrow : AttackPartBase
{
    [Header("弓箭预制体")]
    [SerializeField]
    private GameObject prefeb;

    protected override void Attack()
    {
        Shoot();
        Invoke(nameof(Shoot), 0.5f);
        Invoke(nameof(Shoot), 1f);
    }

    private void Shoot()
    {
        Vector3 p0 = transform.position;
        float angle_l = transform.rotation.z + 90.0f;
        Vector3 p_l = Quaternion.Euler(0, 0, angle_l) * transform.right;
        Instantiate(prefeb, p0 + 0.75f * p_l, Quaternion.Euler(0, 0, angle_l));
        float angle_r = transform.rotation.z - 90.0f;
        Vector3 p_r = Quaternion.Euler(0, 0, angle_r) * transform.right;
        Instantiate(prefeb, p0 + 0.75f * p_r, Quaternion.Euler(0, 0, angle_r));
    }
}
