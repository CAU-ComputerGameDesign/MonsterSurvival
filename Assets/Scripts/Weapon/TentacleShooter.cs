using System.Collections;
using System.Collections.Generic;
using MimicSpace;
using UnityEngine;

public class TentacleShooter : MonoBehaviour, IWeapon
{
    public int bulletID = 2;

    private Mimic mimic;
    private List<Tentacle> tentacles = new List<Tentacle>();

    public int weaponLevel = 0;
    public TargetDetecter targetDetecter;

    public bool canShoot = true;

    public float range = 3f;
    
    public float[] damageOnLevel;
    public float[] shootRateOnLevel;
    public int[] weaponCountOnLevel;

    public Ability ability;

    public void Start()
    {
        mimic = GetComponentInParent<Mimic>();
        tentacles.Add(mimic.GetTentacle());

        damageOnLevel = ability.damages;
        shootRateOnLevel = ability.fireRates;
        weaponCountOnLevel = ability.counts;
    }
    public void WeaponLevelUp()
    {
        weaponLevel++;
        while (weaponCountOnLevel[weaponLevel] > tentacles.Count)
        {
            tentacles.Add(mimic.GetTentacle());
        }

        for (int i = 0; i < tentacles.Count; i++)
        {
            tentacles[i].hitRate = shootRateOnLevel[weaponLevel];
        }
    }

    public void Update()
    {
        bool targetDetected = false;
        for (int i = 0; i < weaponCountOnLevel[weaponLevel] && i < targetDetecter.targets.Length; i++)
        {
            if (tentacles[i].canAttack)
            {
                if (!targetDetected)
                {
                    targetDetecter.GetNearest();
                    targetDetected = true;
                }
                if (targetDetecter.hasTarget && Vector3.Distance(targetDetecter.targets[i].transform.position, transform.position) < range)
                {
                    Attack(i, targetDetecter.targets[i].transform.position);
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
    public void Attack(int i, Vector3 targetPosition)
    {
        tentacles[i].Attack(targetPosition, damageOnLevel[weaponLevel]);
    }

    public void Attack()
    {
        
    }
}
