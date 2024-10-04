using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugUpdate : MonoBehaviour
{
    public TMP_Text text;
    public ExtPlayer mainPlayer;

    private void Update()
    {
        text.text = $"<b>[debug]</b>:\n" +
            $"HP: {mainPlayer.GetHealth()}\n" +
            $"Stamina: {mainPlayer.stamina.x}/{mainPlayer.stamina.y}";
    }
}
