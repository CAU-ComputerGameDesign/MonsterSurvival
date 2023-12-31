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
            transform.position += (target - transform.position) / 7f;
            if (Vector3.Distance(target, transform.position) < 1f)
            {
                GameManager.Instance.playerExp += expAmount;
                GameManager.Instance.RecycleEXPObject(gameObject);
            }
        }
    }
}
