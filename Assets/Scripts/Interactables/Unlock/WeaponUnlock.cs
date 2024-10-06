using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUnlock : MonoBehaviour
{
    public void UnlockWeapon(int weapon)
    {
        if (GlobalReferences.localPlayerWeapons.weapons[weapon] != null)
        {
            if (GlobalReferences.localPlayerWeapons.weapons[weapon].unlocked == false)
            {
                GlobalReferences.localPlayerWeapons.weapons[weapon].unlocked = true;
            }
        }
    }
}
