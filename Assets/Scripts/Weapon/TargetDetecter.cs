using System;
using System.Collections;
using System.Collections.Generic;
using MimicSpace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class TargetDetecter : MonoBehaviour
{
    
    public float scanRange;
    public LayerMask targetLayer;
    public Collider[] targets;
    
    public Transform target;

    private bool[] canShoot;
    
    public List<IWeapon> weapons = new List<IWeapon>();
    
    public void Start()
    {
        IWeapon[] list = GetComponentsInChildren<IWeapon>(true);
    }

    public void GetNearest()
    {
        targets = Physics.OverlapSphere(transform.position, scanRange, targetLayer);
        Array.Sort(targets, (Collider x, Collider y) => Vector3.Distance(transform.position,  x.transform.position).CompareTo(Vector3.Distance(transform.position,  y.transform.position)));
    }
}
