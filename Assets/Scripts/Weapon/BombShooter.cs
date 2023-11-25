using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShooter : MonoBehaviour, IWeapon
{
    public int bulletID = 1;

    public TargetDetecter targetDetecter;

    public bool canShoot = true;
    [SerializeField]
    private float reachTime = 1f;
    [SerializeField]
    private float predictAmount = 2f;
    
    
    public int weaponLevel = 0;

    
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

    public void WeaponLevelUp()
    {
        weaponLevel++;
    }

    public bool CanAttack()
    {
        return true;
    }
    public bool HasTarget()
    {
        return true;
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


    private IEnumerator shootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootRateOnLevel[weaponLevel]);
        canShoot = true;
    }
    
    public void Attack(Vector3 targetPosition)
    {
        Vector3 currentPosition = transform.position;
        currentPosition.y += 0.5f;
        GameObject bomb = BulletPool.Instance.GetBullet(bulletID, currentPosition, Quaternion.identity, targetPosition, damageOnLevel[weaponLevel]);

        float gravity = Physics.gravity.y;

        Vector3 deltaXZ = new Vector3(targetPosition.x - currentPosition.x, 0,
            targetPosition.z - currentPosition.z);

        deltaXZ -= new Vector3(deltaXZ.normalized.x, 0, deltaXZ.normalized.z) * predictAmount;
        
        float deltaY = targetPosition.y - currentPosition.y;
        float Vy = deltaY / reachTime - gravity * reachTime / 2f;

        Vector3 initialVelocity = new Vector3(deltaXZ.x / reachTime, Vy, deltaXZ.z / reachTime);

        Rigidbody bombRigidbody = bomb.GetComponent<Rigidbody>();
        bombRigidbody.velocity = initialVelocity;
    }

    public void Attack()
    {
        
    }
}
