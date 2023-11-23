using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShooter : MonoBehaviour, IWeapon
{
    public int bulletID = 1;

    private float reachTime = 1f;

    public bool CanAttack()
    {
        return true;
    }
    public bool HasTarget()
    {
        return true;
    }

    public void Attack(Vector3 targetPosition)
    {
        Vector3 currentPosition = transform.position;
        currentPosition.y += 0.5f;
        GameObject bomb = BulletPool.Instance.GetBullet(bulletID, currentPosition, Quaternion.identity, targetPosition);

        float gravity = Physics.gravity.y;

        Vector3 deltaXZ = new Vector3(targetPosition.x - currentPosition.x, 0,
            targetPosition.z - currentPosition.z);
        float deltaY = targetPosition.y - currentPosition.y;
        float Vy = deltaY / reachTime - gravity * reachTime / 2f;

        Vector3 initialVelocity = new Vector3(deltaXZ.x / reachTime, Vy, deltaXZ.z / reachTime);

        Rigidbody bombRigidbody = bomb.GetComponent<Rigidbody>();
        Debug.Log(initialVelocity);
        bombRigidbody.velocity = initialVelocity;
    }

    public void Attack()
    {
        
    }
}
