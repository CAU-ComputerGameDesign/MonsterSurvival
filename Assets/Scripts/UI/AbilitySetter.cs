using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySetter : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] Image _icon;
    [SerializeField] TextMeshProUGUI _abilityLevel;
    [SerializeField] TextMeshProUGUI _abilityName;
    [SerializeField] TextMeshProUGUI _abilityDesc;

    private IWeapon _current;

    public void Init(Action<int> onButtonClicked)
    {
        _button.onClick.AddListener(() => onButtonClicked?.Invoke(_current.BulletID));
    }

    public void SetData(IWeapon weapon)
    {
        _current = weapon;

        Ability ability = weapon.GetAbility();

        _icon.sprite = ability.abilityIcon;
        _abilityLevel.text = string.Format("Lv: " + weapon.GetLevel().ToString());
        _abilityName.text = ability.abilityName;
        _abilityDesc.text = ability.abilityDescription;
    }
}
