using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstEffect : MonoBehaviour
{
    [SerializeField]
    private int particleID = 0;
    private ParticleSystem particleSystem;

    public void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        if(particleSystem.isStopped)
            ParticlePool.Instance.RecycleParticle(particleID, gameObject);
    }
}
