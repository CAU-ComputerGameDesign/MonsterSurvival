using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISetter : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI time;
    public TextMeshProUGUI level;
    public Slider hpSlider;

    [SerializeField] AbilityIcon _abilityIconPrefab;
    [SerializeField] Transform _abilityIconParent;
    private Dictionary<int, AbilityIcon> _abilityIcons;

    private void Start()
    {
        _abilityIcons = new Dictionary<int, AbilityIcon>();
    }

    public void Update()
    {
        GameManager gm = GameManager.Instance;
        slider.value = gm.playerExp / gm.nextExp;
        hpSlider.value = gm.hpValue;

        float remainTime = gm.maxTime - gm.gameTime;
        int min = Mathf.FloorToInt(remainTime / 60);
        int sec = Mathf.FloorToInt(remainTime % 60);

        time.text = string.Format("{0:D2}:{1:D2}", min, sec);
        level.text = string.Format("Lv : {0:D1}", gm.level);
    }

    public void SetAbilityUI(Weapon weapon)
    {
        int id = weapon.BulletID;

        if (_abilityIcons.ContainsKey(id) == false)
        {
            _abilityIcons.Add(id, Instantiate(_abilityIconPrefab, _abilityIconParent));
        }
        _abilityIcons[id].SetData(weapon);
    }
}
