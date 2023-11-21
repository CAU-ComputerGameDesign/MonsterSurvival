using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour, IWeapon
{
    public int bulletID = 0;
    public bool HasTarget()
    {
        return true;
    }

    public void Attack(Vector3 targetPosition)
    {
        targetPosition.y += 0.2f;
        Quaternion rot = Quaternion.LookRotation(targetPosition - transform.position);
        BulletPool.Instance.GetBullet(bulletID, transform.position, rot, targetPosition);
    }

    public void Attack()
    {
        
    }
}
