using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityIcon : MonoBehaviour
{
    [SerializeField] Image _imgAbility;
    [SerializeField] TextMeshProUGUI _txtLv;

    public void SetData(Weapon weapon)
    {
        Ability ability = weapon.GetAbility();

        _imgAbility.sprite = ability.abilityIcon;
        if (weapon.IsMaxLevel)
            _txtLv.text = string.Format("Lv: Max");
        else
            _txtLv.text = string.Format("Lv: " + weapon.GetLevel().ToString());
    }
}
