using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnDamaged : MonoBehaviour
{
    private Collider collider;
    void Start()
    {
        collider = GetComponent<Collider>();
        OnEnable();
    }

    void OnEnable()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            GameManager.Instance.LooseHp(0.3f);
            Debug.Log("damaged");
        }
        else if (!other.CompareTag("Player")) ;
    }
}
