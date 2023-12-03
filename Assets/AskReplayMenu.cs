using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class AskReplayMenu : MonoBehaviour
{
    public bool iscontinue=false;
    public TextMeshProUGUI Score;

    private void Start()
    {
        iscontinue = true;
    }

    public void Update()
    {
        GameManager gm = GameManager.Instance;
        if(gm.isGameOver==true) {
            Score.text = ((int)gm.gameTime).ToString();
        }
    }

}
