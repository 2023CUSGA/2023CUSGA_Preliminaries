using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPart_StoneBullet : AttackPartBase
{
    private GameObject prefeb;
    private GameObject prefeb_buff;

    private void Start()
    {
        prefeb = prefebs[0];
        prefeb_buff = prefebs[1];
    }

    protected override void Attack()
    {
        Shoot();
        Invoke(nameof(Shoot), 0.25f);
        Invoke(nameof(Shoot_buff), 0.5f);
    }

    private void Shoot()
    {
        Vector3 p0 = transform.position;
        float angle_l = transform.rotation.eulerAngles.z + 180f;
        Vector3 p_l = Quaternion.Euler(0, 0, 90f) * transform.up;
        //Instantiate(prefeb, p0 + 0.75f * p_l, Quaternion.Euler(0, 0, angle_l - 30f));
        //Instantiate(prefeb, p0 + 0.75f * p_l, Quaternion.Euler(0, 0, angle_l));
        //Instantiate(prefeb, p0 + 0.75f * p_l, Quaternion.Euler(0, 0, angle_l + 30f));
        PoolManager.Release(prefeb, p0 + 0.75f * p_l, Quaternion.Euler(0, 0, angle_l - 30f));
        PoolManager.Release(prefeb, p0 + 0.75f * p_l, Quaternion.Euler(0, 0, angle_l));
        PoolManager.Release(prefeb, p0 + 0.75f * p_l, Quaternion.Euler(0, 0, angle_l + 30f));
        float angle_r = transform.rotation.eulerAngles.z;
        Vector3 p_r = Quaternion.Euler(0, 0, -90f) * transform.up;
        //Instantiate(prefeb, p0 + 0.75f * p_r, Quaternion.Euler(0, 0, angle_r - 30f));
        //Instantiate(prefeb, p0 + 0.75f * p_r, Quaternion.Euler(0, 0, angle_r));
        //Instantiate(prefeb, p0 + 0.75f * p_r, Quaternion.Euler(0, 0, angle_r + 30f));
        PoolManager.Release(prefeb, p0 + 0.75f * p_r, Quaternion.Euler(0, 0, angle_r - 30f));
        PoolManager.Release(prefeb, p0 + 0.75f * p_r, Quaternion.Euler(0, 0, angle_r));
        PoolManager.Release(prefeb, p0 + 0.75f * p_r, Quaternion.Euler(0, 0, angle_r + 30f));
    }

    private void Shoot_buff()
    {
        Vector3 p0 = transform.position;
        float angle_l = transform.rotation.eulerAngles.z + 180f;
        Vector3 p_l = Quaternion.Euler(0, 0, 90f) * transform.up;
        //Instantiate(prefeb_buff, p0 + 0.75f * p_l, Quaternion.Euler(0, 0, angle_l - 30f));
        //Instantiate(prefeb_buff, p0 + 0.75f * p_l, Quaternion.Euler(0, 0, angle_l));
        //Instantiate(prefeb_buff, p0 + 0.75f * p_l, Quaternion.Euler(0, 0, angle_l + 30f));
        PoolManager.Release(prefeb_buff, p0 + 0.75f * p_l, Quaternion.Euler(0, 0, angle_l - 30f));
        PoolManager.Release(prefeb_buff, p0 + 0.75f * p_l, Quaternion.Euler(0, 0, angle_l));
        PoolManager.Release(prefeb_buff, p0 + 0.75f * p_l, Quaternion.Euler(0, 0, angle_l + 30f));
        float angle_r = transform.rotation.eulerAngles.z;
        Vector3 p_r = Quaternion.Euler(0, 0, -90f) * transform.up;
        //Instantiate(prefeb_buff, p0 + 0.75f * p_r, Quaternion.Euler(0, 0, angle_r - 30f));
        //Instantiate(prefeb_buff, p0 + 0.75f * p_r, Quaternion.Euler(0, 0, angle_r));
        //Instantiate(prefeb_buff, p0 + 0.75f * p_r, Quaternion.Euler(0, 0, angle_r + 30f));
        PoolManager.Release(prefeb_buff, p0 + 0.75f * p_r, Quaternion.Euler(0, 0, angle_r - 30f));
        PoolManager.Release(prefeb_buff, p0 + 0.75f * p_r, Quaternion.Euler(0, 0, angle_r));
        PoolManager.Release(prefeb_buff, p0 + 0.75f * p_r, Quaternion.Euler(0, 0, angle_r + 30f));
    }
}
