using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShooter : MonoBehaviour, IWeapon
{
    public int bulletID = 1;

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
        
        // 현재 위치
        Vector3 currentPosition = transform.position;

        // 상대 위치와의 거리
        float distance = Vector3.Distance(currentPosition, targetPosition);

        // 폭탄이 도착해야 할 곳과의 거리와 시간을 계산하여 발사 속도 계산
        float timeToReachTarget = 3.0f; // 터지는 데 걸리는 시간
        float gravity = Physics.gravity.y; // 중력 값
        float initialVelocityY = (targetPosition.y - currentPosition.y) / timeToReachTarget + 0.5f * gravity * timeToReachTarget;
        float initialVelocityXZ = distance / timeToReachTarget;

        // 폭탄의 초기 속도 벡터 계산
        Vector3 initialVelocity = (targetPosition - currentPosition).normalized * initialVelocityXZ;
        initialVelocity.y = - initialVelocityY;

        GameObject bomb = BulletPool.Instance.GetBullet(bulletID, transform.position, Quaternion.identity, targetPosition);
        Rigidbody bombRigidbody = bomb.GetComponent<Rigidbody>();
        bombRigidbody.velocity = initialVelocity;
    }

    public void Attack()
    {
        
    }
}
