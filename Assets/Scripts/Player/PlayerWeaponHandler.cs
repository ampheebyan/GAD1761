using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    public BaseWeapon[] weapons;
    public int currentWeapon = 0;

    public void ShootCurrentWeapon() {
        BaseWeapon current = weapons[currentWeapon];

        switch(current.type) {
            case BaseWeapon.weaponType.physical:
                current.ShootProjectile();
            break;
            case BaseWeapon.weaponType.hitscan:
            // Unimplemented.
            break;
        }
    }
}
