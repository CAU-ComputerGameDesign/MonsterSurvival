using System.Collections;
using System.Collections.Generic;
using MimicSpace;
using UnityEngine;

public class TentacleShooter : MonoBehaviour, IWeapon
{
    public int bulletID = 2;

    private Mimic mimic;
    private Tentacle tentacle;

    
    public void Start()
    {
        mimic = GetComponentInParent<Mimic>();
        tentacle = mimic.GetTentacle();
    }
    public bool HasTarget()
    {
        return true;
    }

    public bool CanAttack()
    {
        return tentacle.canAttack;
    }

    public void Attack(Vector3 targetPosition)
    {
        
        tentacle.Attack(targetPosition);
    }

    public void Attack()
    {
        
    }
}
