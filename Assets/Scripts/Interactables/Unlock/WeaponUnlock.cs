using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUnlock : MonoBehaviour
{
    // Weapon unlocking helper class.

    public void UnlockWeapon(int weapon)
    {
        // If not null at this index
        if (GlobalReferences.localPlayerWeapons.weapons[weapon] != null)
        {
            // If not already unlocked
            if (GlobalReferences.localPlayerWeapons.weapons[weapon].unlocked == false)
            {
                // Unlock it.
                GlobalReferences.localPlayerWeapons.weapons[weapon].unlocked = true;
            }
        }
    }
}
