using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    public void SetTarget(Vector3 target);
    public void SetDamage(float damage);
}