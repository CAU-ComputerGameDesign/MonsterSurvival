using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour, IWeapon
{
    public int bulletID = 0;

    public TargetDetecter targetDetecter;

    public int weaponLevel = 0;

    public bool canShoot = true;

    public float[] damageOnLevel;
    public float[] shootRateOnLevel;
    public int[] weaponCountOnLevel;

    public Ability ability;

    public void Start()
    {
        damageOnLevel = ability.damages;
        shootRateOnLevel = ability.fireRates;
        weaponCountOnLevel = ability.counts;
    }

    public void Update()
    {
        if (canShoot)
        {
            targetDetecter.GetNearest();
            StartCoroutine("shootCooldown");
            for (int i = 0; i < weaponCountOnLevel[weaponLevel] && i < targetDetecter.targets.Length; i++)
            {
                if (targetDetecter.hasTarget)
                {
                    Attack(targetDetecter.targets[i].transform.position);
                }
            }
        }
    }
    
    public void WeaponLevelUp()
    {
        weaponLevel++;
    }
    private IEnumerator shootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootRateOnLevel[weaponLevel]);
        canShoot = true;
    }

    public void Attack(Vector3 targetPosition)
    {
        targetPosition.y += 0.2f;
        Quaternion rot = Quaternion.LookRotation(targetPosition - transform.position);
        BulletPool.Instance.GetBullet(bulletID, transform.position, rot, targetPosition, damageOnLevel[weaponLevel]);
    }

    public void Attack()
    {
        
    }
}
