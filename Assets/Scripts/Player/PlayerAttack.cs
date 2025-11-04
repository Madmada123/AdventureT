using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AttackBase
{
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform firePoint;

    public void CastFireball()
    {
        if (!CanAttack()) return;

        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        fireball.GetComponent<FireballProjectile>().Init(damage);
    }
}
