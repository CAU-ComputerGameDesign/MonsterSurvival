using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private float radius = 20f;
    
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField] 
    private float damage = 5f;
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
        /*
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);
        for (int i = 0; i < hits.Length; i++)
        {
            hits[i].GetComponent<AnimalsController>().Hit(damage);
        }
        
        gameObject.SetActive(false);*/
    }
}
