using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Entity
{

    [SerializeField]
    private LayerMask targetLayer;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float reloadTime;

    [SerializeField]
    private Transform bulletPos;




    private void Attack()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            var bullet = Instantiate(bulletPrefab, bulletPos.transform.position, bulletPos.transform.rotation).GetComponent<Projectile>();
            bullet.Damage = stat.Damage;
            bullet.SetupProjectile(targetLayer, (int)stat.AttackSpeed, 15f);
        }
    }

   

}
