using Consts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy : Entity
{
    [SerializeField]
    private LayerMask targetLayer;

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletPos;

    private float time = 0;

    private void TimeReset()
    {
        time = stat.Reloading;
    }

    private void RangeAttack()
    {
        time -= Time.deltaTime;
        if (time > 0 ) { return; }

        time = stat.Reloading;

        Debug.Log("АјАн");
        var bullet = Instantiate(bulletPrefab, bulletPos.transform.position, bulletPos.transform.rotation).GetComponent<Projectile>();
        bullet.Damage = stat.Damage;
        bullet.SetupProjectile(targetLayer, (int)stat.AttackSpeed, 15f);
    }


}
