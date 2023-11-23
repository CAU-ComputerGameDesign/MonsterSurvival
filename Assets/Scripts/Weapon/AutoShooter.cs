using System.Collections;
using System.Collections.Generic;
using MimicSpace;
using UnityEngine;
using UnityEngine.Networking;

public class AutoShooter : MonoBehaviour
{
    
    public float scanRange;
    public LayerMask targetLayer;
    public Collider[] targets;


    public Transform target;

    private bool canShoot = true;
    
    public IWeapon weapon;

    public float damage;
    public float shootRate;
    public int weaponCount;

    public float[] damageOnLevel;
    public float[] shootRateOnLevel;
    public int[] weaponCountOnLevel;

    public void Start()
    {
        weapon = GetComponentInChildren<IWeapon>();
    }

    public void Initialize(Ability ability)
    {
        damage = ability.baseDamage;
        shootRate = ability.baseFireRate;
        weaponCount = ability.baseCount;

        damageOnLevel = ability.damages;
        shootRateOnLevel = ability.fireRates;
        weaponCountOnLevel = ability.counts;
    }
    
    
    private IEnumerator shootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootRate);
        canShoot = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        GetNearest();
        if (weapon != null)
        {
            if (canShoot && weapon.CanAttack())
            {
                StartCoroutine("shootCooldown");
                if (weapon.HasTarget() == true)
                {
                    if (target != null)
                    {
                        weapon.Attack(target.position);
                    }
                }
                else
                {
                    weapon.Attack();
                }
            
            }
        }
    }

    void GetNearest()
    {
        targets = Physics.OverlapSphere(transform.position, scanRange, targetLayer);
        Transform result = null;
        float diff = Mathf.Infinity;
        for (int i = 0; i < targets.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, targets[i].transform.position);
            if (dist < diff)
            {
                diff = dist;
                result = targets[i].transform;
            }
        }

        target = result;
    }
}
