using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShooter : Weapon
{
    public override int BulletID => 1;

    public TargetDetecter targetDetecter;

    public bool canShoot = true;
    [SerializeField]
    private float reachTime = 1f;
    [SerializeField]
    private float predictAmount = 2f;
    
    public float[] damageOnLevel;
    public float[] shootRateOnLevel;
    public int[] weaponCountOnLevel;

    public void Start()
    {
        damageOnLevel = _ability.damages;
        shootRateOnLevel = _ability.fireRates;
        weaponCountOnLevel = _ability.counts;
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
        Vector3 currentPosition = transform.position;
        currentPosition.y += 0.5f;
        GameObject bomb = BulletPool.Instance.GetBullet(BulletID, currentPosition, Quaternion.identity, targetPosition, damageOnLevel[_weaponLevel]);

        float gravity = Physics.gravity.y;

        Vector3 deltaXZ = new Vector3(targetPosition.x - currentPosition.x, 0,
            targetPosition.z - currentPosition.z);

        deltaXZ -= new Vector3(deltaXZ.normalized.x, 0, deltaXZ.normalized.z) * predictAmount;
        
        float deltaY = targetPosition.y - currentPosition.y;
        float Vy = deltaY / reachTime - gravity * reachTime / 2f;

        Vector3 initialVelocity = new Vector3(deltaXZ.x / reachTime, Vy, deltaXZ.z / reachTime);

        Rigidbody bombRigidbody = bomb.GetComponent<Rigidbody>();
        bombRigidbody.velocity = initialVelocity;
        SoundEffect.instance.PlayBomb();
    }

    public void Attack()
    {
        
    }
}
