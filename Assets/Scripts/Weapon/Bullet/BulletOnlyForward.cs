using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletOnlyForward : MonoBehaviour, IBullet
{
    private int bulletID = 0;

    public Vector3 target;

    public Rigidbody rigid;
    public float speed;
    public float duration = 4f;

    public float damage = 1f;

    public void SetTarget(Vector3 target)
    {
        this.target = target;
        Vector3 direction = target - transform.position;
        rigid.velocity = direction.normalized * speed;
    }
    // Update is called once per frame
    void OnEnable()
    {
        StartCoroutine("BulletDuration");
    }

    IEnumerator BulletDuration()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            AnimalsController controller = other.GetComponent<AnimalsController>();
            if(!controller.isDead)
                BulletPool.Instance.RecycleBullet(bulletID, gameObject);
            controller.Hit(damage);
        }
        else if(!other.CompareTag("Player"))
            BulletPool.Instance.RecycleBullet(bulletID, gameObject);
    }
}
