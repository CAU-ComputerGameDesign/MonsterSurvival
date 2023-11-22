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

    public void Update()
    {
        GameManager gm = GameManager.Instance;
        slider.value = gm.playerExp / gm.nextExp;

        float remainTime = gm.maxTime - gm.gameTime;
        int min = Mathf.FloorToInt(remainTime / 60);
        int sec = Mathf.FloorToInt(remainTime % 60);

        time.text = string.Format("{0:D2}:{1:D2}", min, sec);
        level.text = string.Format("Lv : {0:D1}", gm.level);
    }
}
