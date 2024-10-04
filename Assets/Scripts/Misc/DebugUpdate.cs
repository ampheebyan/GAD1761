using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugUpdate : MonoBehaviour
{
    public TMP_Text text;
    private void Update()
    {
        text.text = $"<b>[debug]</b>:\n" +
            $"HP: {GlobalReferences.localPlayer.GetHealth().x}/{GlobalReferences.localPlayer.GetHealth().y}\n" +
            $"Stamina: {GlobalReferences.localPlayer.stamina.x}/{GlobalReferences.localPlayer.stamina.y}\n" +
            $"Current: {GlobalReferences.localPlayerWeapons.currentWeapon.name}\n" +
            $"Ammo: {GlobalReferences.localPlayerWeapons.currentWeapon.ammo.x}/{GlobalReferences.localPlayerWeapons.currentWeapon.ammo.y}";
    }
}
