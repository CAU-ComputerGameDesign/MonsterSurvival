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
    private Collider collider;
    private SkinnedMeshRenderer[] meshRenderers;

    private bool canMove = true;
    public bool isDead = false;

    private Color initColor;

    public float maxHp = 3f;
    public float hp = 3f;

    public float expAmount = 1f;
    public float damage = 0.3f;
    
    // Start is called before the first frame update

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        collider = GetComponent<Collider>();

        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        initColor = meshRenderers[0].material.color;
        OnEnable();
    }
    
    void OnEnable()
    {
        gameObject.layer = 6;
        isDead = false;
        target = GameManager.Instance.Player.transform;
        hp = maxHp;
        navMeshAgent.enabled = true;
        canMove = true;
        animator.Play("Run");
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;
        float dist = Vector3.Distance(transform.position, target.position);
        if (canMove == false)
        {
            navMeshAgent.speed = 0f;
            navMeshAgent.angularSpeed = 0f;
        }
        else
        {
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
        navMeshAgent.SetDestination(target.position);
        animator.SetFloat("Distance", dist);
    }

    private void FixedUpdate()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

    IEnumerator Recycle()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
    IEnumerator HitFlash()
    {
        yield return new WaitForSeconds(0.05f);
        for(int i = 0; i < meshRenderers.Length; i++)
            meshRenderers[i].material.color = initColor;
    }
    IEnumerator HitMove()
    {
        yield return new WaitForSeconds(0.5f);
        canMove = true;
    }
    public void Hit(float damage)
    {
        if (isDead) return;
        hp -= damage;
        if (hp <= 0)
        {
            isDead = true;
            navMeshAgent.speed = 0f;
            navMeshAgent.angularSpeed = 0f;
            for(int i = 0; i < meshRenderers.Length; i++)
                meshRenderers[i].material.color = Color.white;
            StartCoroutine("HitFlash");
            animator.Play("Death");
            gameObject.layer = 0;
            StartCoroutine("Recycle");
            GameObject exp = GameManager.Instance.GetExpFromPool(transform.position);
            exp.GetComponent<ExpProperty>().expAmount = expAmount;
            return;
        }
        for(int i = 0; i < meshRenderers.Length; i++)
            meshRenderers[i].material.color = Color.white;
        canMove = false;
        animator.Play("Hit");
        StartCoroutine("HitFlash");
        StartCoroutine("HitMove");  
    }

}
