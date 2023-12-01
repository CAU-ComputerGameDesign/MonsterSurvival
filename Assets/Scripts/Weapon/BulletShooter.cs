using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : Weapon
{
    public override int BulletID => 0;

    public TargetDetecter targetDetecter;

    public bool canShoot = true;

    public float[] damageOnLevel;
    public float[] shootRateOnLevel;
    public int[] weaponCountOnLevel;

    public void Start()
    {
        damageOnLevel = _ability.damages;
        shootRateOnLevel = _ability.fireRates;
        weaponCountOnLevel = _ability.counts;
    }

    public void Update()
    {
        if (canShoot)
        {
            targetDetecter.GetNearest();
            StartCoroutine("shootCooldown");
            for (int i = 0; i < weaponCountOnLevel[_weaponLevel] && i < targetDetecter.targets.Length; i++)
            {
                if (targetDetecter.hasTarget)
                {
                    Attack(targetDetecter.targets[i].transform.position);
                }
            }
        }
    }
    
    private IEnumerator shootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootRateOnLevel[_weaponLevel]);
        canShoot = true;
    }

    public void Attack(Vector3 targetPosition)
    {
        targetPosition.y += 0.2f;
        Quaternion rot = Quaternion.LookRotation(targetPosition - transform.position);
        BulletPool.Instance.GetBullet(BulletID, transform.position, rot, targetPosition, damageOnLevel[_weaponLevel]);
        SoundEffect.instance.PlayBullet();
    }

    public void Attack()
    {
        
    }
}
