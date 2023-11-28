using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChooseAbility : MonoBehaviour
{
    [SerializeField] List<Weapon> weaponList;
    [SerializeField] List<AbilitySetter> _setterUIs;

    private Action _onComplete;

    public void Init(Action onComplete)
    {
        _onComplete = onComplete;

        for (int i = 0; i < _setterUIs.Count; i++)
        {
            _setterUIs[i].Init(SelectAbility);
        }
    }

    public void ReadyToChoose()
    {
        List<Weapon> weaponToChoose = weaponList.Where(weapon => weapon.IsMaxLevel == false).ToList();
        
        int chooseCount = Math.Min(_setterUIs.Count, weaponToChoose.Count);
        if (chooseCount == 0) { _onComplete?.Invoke(); return; }

        weaponToChoose = weaponToChoose.ChooseRandom(chooseCount);

        int i = 0;
        for (; i < weaponToChoose.Count; i++)
        {
            _setterUIs[i].SetData(weaponToChoose[i]);
        }
        for (;  i < _setterUIs.Count; i++)
        {   // 선택할 수 있는 능력 개수가 UI 개수보다 작은 경우 비활성화
            _setterUIs[i].gameObject.SetActive(false);
        }
    }

    public void SelectAbility(int bulletID)
    {
        weaponList[bulletID].WeaponLevelUp();

        _onComplete?.Invoke();
    }
}
