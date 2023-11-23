using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseAbility : MonoBehaviour
{
    public List<GameObject> weaponList = new List<GameObject>();
    
    public void SelectAbility(int bulletID)
    {
        AutoShooter autoShooter = GameManager.Instance.Player.GetComponent<AutoShooter>();
        for (int i = 0; i < weaponList.Count; i++)
        {
            if (i != bulletID)
                weaponList[i].SetActive(false);
            else
                weaponList[i].SetActive(true);
            
        }

        autoShooter.weapon = weaponList[bulletID].GetComponent<IWeapon>();
        GameManager.Instance.GameStart();
    }
}
