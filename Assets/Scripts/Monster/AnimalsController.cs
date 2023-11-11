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
    
    // Start is called before the first frame update

    void Start()
    {
        OnEnable();
    }
    
    void OnEnable()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        target = GameManager.Instance.Player.transform;
        
        animator.Play("Run");
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(target.position);
        if (navMeshAgent.velocity.magnitude > 0f)
        {
            animator.Play("Run");
        }
        else
        {
            animator.Play("Idle_A");
        }
    }
}
