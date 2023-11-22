using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySetter : MonoBehaviour
{
    public Ability ability;
    public Image icon;
    public TextMeshProUGUI abilityName;
    public TextMeshProUGUI abilityDesc;

    public void Update()
    {
        icon.sprite = ability.abilityIcon;
        abilityName.text = ability.abilityName;
        abilityDesc.text = ability.abilityDescription;
    }
}
