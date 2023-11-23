using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseAbility : MonoBehaviour
{
    public List<GameObject> weaponList = new List<GameObject>();
    
    public void SelectAbility(int bulletID)
    {
        for (int i = 0; i < weaponList.Count; i++)
        {
            if (i != bulletID)
                weaponList[i].SetActive(false);
            else
                weaponList[i].SetActive(true);
        }

        GameManager.Instance.Player.GetComponent<AutoShooter>().weapon = weaponList[bulletID].GetComponent<IWeapon>();
    }
}
