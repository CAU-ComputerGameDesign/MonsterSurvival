using System.Collections;
using System.Collections.Generic;
using MimicSpace;
using UnityEngine;

public class TentacleShooter : MonoBehaviour, IWeapon
{
    public int bulletID = 2;

    private Mimic mimic;
    private Tentacle tentacle;

    public int weaponLevel = 0;
    public float damage = 1.5f;
    
    public TargetDetecter targetDetecter;

    public bool canShoot = true;
    
    public float[] damageOnLevel;
    public float[] shootRateOnLevel;
    public int[] weaponCountOnLevel;

    public Ability ability;

    public void Start()
    {
        mimic = GetComponentInParent<Mimic>();
        tentacle = mimic.GetTentacle();

        damageOnLevel = ability.damages;
        shootRateOnLevel = ability.fireRates;
        weaponCountOnLevel = ability.counts;
    }
    public bool HasTarget()
    {
        return true;
    }

    public bool CanAttack()
    {
        return tentacle.canAttack;
    }
    
    public void WeaponLevelUp()
    {
        weaponLevel++;
    }

    public void Update()
    {
        if (canShoot && tentacle.canAttack)
        {
            targetDetecter.GetNearest();
            StartCoroutine("shootCooldown");
            for (int i = 0; i < weaponCountOnLevel[weaponLevel]; i++)
            {
                Attack(targetDetecter.targets[i].transform.position);
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
        tentacle.Attack(targetPosition, damage);
    }

    public void Attack()
    {
        
    }
}
