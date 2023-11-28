using System.Collections;
using System.Collections.Generic;
using MimicSpace;
using UnityEngine;

public class TentacleShooter : Weapon
{
    public override int BulletID => 2;

    private Mimic mimic;
    private List<Tentacle> tentacles = new List<Tentacle>();

    public TargetDetecter targetDetecter;

    public bool canShoot = true;

    public float range = 3f;
    
    public float[] damageOnLevel;
    public float[] shootRateOnLevel;
    public int[] weaponCountOnLevel;

    public void Start()
    {
        mimic = GetComponentInParent<Mimic>();
        tentacles.Add(mimic.GetTentacle());

        damageOnLevel = _ability.damages;
        shootRateOnLevel = _ability.fireRates;
        weaponCountOnLevel = _ability.counts;
    }

    protected override void OnWeaponLevelUp()
    {
        base.OnWeaponLevelUp();
        while (weaponCountOnLevel[_weaponLevel] > tentacles.Count)
        {
            tentacles.Add(mimic.GetTentacle());
        }

        for (int i = 0; i < tentacles.Count; i++)
        {
            tentacles[i].hitRate = shootRateOnLevel[_weaponLevel];
        }
    }

    public void Update()
    {
        bool targetDetected = false;
        for (int i = 0; i < weaponCountOnLevel[_weaponLevel] && i < targetDetecter.targets.Length; i++)
        {
            if (tentacles[i].canAttack)
            {
                if (!targetDetected)
                {
                    targetDetecter.GetNearest();
                    targetDetected = true;
                }
                if (targetDetecter.HasTarget(i) &&
                    Vector3.Distance(targetDetecter.targets[i].transform.position, transform.position) < range)
                {
                    Attack(i, targetDetecter.targets[i].transform.position);
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
    public void Attack(int i, Vector3 targetPosition)
    {
        tentacles[i].Attack(targetPosition, damageOnLevel[_weaponLevel]);
    }

    public void Attack()
    {
        
    }
}
