using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Bomb : MonoBehaviour, IBullet
{
    public int bulletID = 1;
    [SerializeField]
    private float radius = 20f;
    
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField] 
    private float damage = 5f;

    private Collider collider;
    private CinemachineImpulseSource impulseSource;

    private void Start()
    {
        collider = GetComponent<Collider>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void OnEnable()
    {
        collider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Explode();
        }
        else if(!other.CompareTag("Player"))
            Explode();
    }

    private void Explode()
    {
        collider.enabled = false;
        ParticlePool.Instance.GetParticle(0, transform.position);
        impulseSource.GenerateImpulse();
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);
        for (int i = 0; i < hits.Length; i++)
        {
            hits[i].GetComponent<AnimalsController>().Hit(damage);
            Rigidbody rb = hits[i].GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(damage, transform.position, radius);
            }
        }
        
        BulletPool.Instance.RecycleBullet(bulletID, gameObject);
    }

    public void SetTarget(Vector3 target)
    {
        
    }
}
