using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseAbility : MonoBehaviour
{
    public List<GameObject> weaponList = new List<GameObject>();
    
    public void SelectAbility(int bulletID)
    {
        weaponList[bulletID].GetComponent<IWeapon>().WeaponLevelUp();
        if(!GameManager.Instance.isGameStarted)
            GameManager.Instance.GameStart();
    }
}
