using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPart_StoneBullet : AttackPartBase
{
    [Header("石弹预制体")]
    [SerializeField]
    private GameObject prefeb;
    [Header("有击退buff的石弹预制体")]
    [SerializeField]
    private GameObject prefeb_buff;

    protected override void Attack()
    {
        Shoot();
        Invoke(nameof(Shoot), 0.5f);
        Invoke(nameof(Shoot_buff), 1f);
    }

    private void Shoot()
    {
        Vector3 p0 = transform.position;
        float angle_l = transform.rotation.z + 90.0f;
        Vector3 p_l = Quaternion.Euler(0, 0, angle_l) * transform.right;
        Instantiate(prefeb, p0 + 0.75f * p_l, Quaternion.Euler(0, 0, angle_l - 30f));
        Instantiate(prefeb, p0 + 0.75f * p_l, Quaternion.Euler(0, 0, angle_l));
        Instantiate(prefeb, p0 + 0.75f * p_l, Quaternion.Euler(0, 0, angle_l + 30f));
        float angle_r = transform.rotation.z + 90.0f;
        Vector3 p_r = Quaternion.Euler(0, 0, angle_r) * transform.right;
        Instantiate(prefeb, p0 + 0.75f * p_r, Quaternion.Euler(0, 0, angle_r - 30f));
        Instantiate(prefeb, p0 + 0.75f * p_r, Quaternion.Euler(0, 0, angle_r));
        Instantiate(prefeb, p0 + 0.75f * p_r, Quaternion.Euler(0, 0, angle_r + 30f));
    }

    private void Shoot_buff()
    {
        Vector3 p0 = transform.position;
        float angle_l = transform.rotation.z + 90.0f;
        Vector3 p_l = Quaternion.Euler(0, 0, angle_l) * transform.right;
        Instantiate(prefeb_buff, p0 + 0.75f * p_l, Quaternion.Euler(0, 0, angle_l - 30f));
        Instantiate(prefeb_buff, p0 + 0.75f * p_l, Quaternion.Euler(0, 0, angle_l));
        Instantiate(prefeb_buff, p0 + 0.75f * p_l, Quaternion.Euler(0, 0, angle_l + 30f));
        float angle_r = transform.rotation.z + 90.0f;
        Vector3 p_r = Quaternion.Euler(0, 0, angle_r) * transform.right;
        Instantiate(prefeb_buff, p0 + 0.75f * p_r, Quaternion.Euler(0, 0, angle_r - 30f));
        Instantiate(prefeb_buff, p0 + 0.75f * p_r, Quaternion.Euler(0, 0, angle_r));
        Instantiate(prefeb_buff, p0 + 0.75f * p_r, Quaternion.Euler(0, 0, angle_r + 30f));
    }
}
