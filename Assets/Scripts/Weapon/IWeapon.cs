using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public bool CanAttack();
    public bool HasTarget();
    public void Attack(Vector3 targetPosition);
    public void Attack();
}
