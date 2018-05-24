using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
   void OnGUI()
    {
        const int btnWeight = 110;
        const int btnHeoght = 40;

        if (GUI.Button(new Rect(Screen.width/2 - (btnWeight/2),Screen.height/3 - btnHeoght/3, btnWeight, btnHeoght), "Начать сначала"))
        {
            Application.LoadLevel("action");
        }

        if (GUI.Button(new Rect(Screen.width / 2 - (btnWeight / 2), 1.5f * Screen.height / 3 - btnHeoght / 3, btnWeight, btnHeoght), "выйти из игры"))
        {
            Application.Quit();
        }
    }
}