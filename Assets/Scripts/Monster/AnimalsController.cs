using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalsController : MonoBehaviour
{
    public Animator animator;

    public float speed;
    public Transform target;

    public NavMeshAgent navMeshAgent;

    private Rigidbody rigid;
    
    // Start is called before the first frame update

    void Start()
    {
        OnEnable();
    }
    
    void OnEnable()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        target = GameManager.Instance.Player.transform;
        
        animator.Play("Run");
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(target.position);

        float dist = Vector3.Distance(transform.position, target.position);
        animator.SetFloat("Distance", dist);

        if (dist > 10f)
        {
            navMeshAgent.speed = 1.0f;
            navMeshAgent.angularSpeed = 500f;
        }
        else if (dist > 2f)
        {
            navMeshAgent.speed = 2.5f;
            navMeshAgent.angularSpeed = 500f;
        }
        else
        {
            navMeshAgent.speed = 0f;
            navMeshAgent.angularSpeed = 0f;
        }
    }

    private void FixedUpdate()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }
}
