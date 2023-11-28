using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseAbility : MonoBehaviour
{
    [SerializeField] List<Weapon> weaponList;
    [SerializeField] List<AbilitySetter> _setterUIs;

    private void Start()
    {
        for (int i = 0; i < _setterUIs.Count; i++)
        {
            _setterUIs[i].Init(SelectAbility);
        }

        ReadyToChoose();
    }

    public void ReadyToChoose()
    {
        List<Weapon> weaponToChoose = weaponList.ChooseRandom(_setterUIs.Count);

        for (int i = 0; i < _setterUIs.Count; i++)
        {
            _setterUIs[i].SetData(weaponToChoose[i]);
        }
    }

    public void SelectAbility(int bulletID)
    {
        weaponList[bulletID].WeaponLevelUp();

        if(!GameManager.Instance.isGameStarted)
            GameManager.Instance.GameStart();

        gameObject.SetActive(false);
    }
}
