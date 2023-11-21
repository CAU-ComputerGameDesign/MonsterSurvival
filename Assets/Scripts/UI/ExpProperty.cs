using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpProperty : MonoBehaviour
{
    public float expAmount = 1f;
    private void FixedUpdate()
    {
        Vector3 target = GameManager.Instance.Player.transform.position;

        if (Vector3.Distance(target, transform.position) < GameManager.Instance.expRange)
        {
            transform.position = Vector3.LerpUnclamped(transform.position, target, Time.deltaTime * 5f);
            if (Vector3.Distance(target, transform.position) < 1f)
            {
                GameManager.Instance.RecycleEXPObject(gameObject);
            }
        }
    }
}
