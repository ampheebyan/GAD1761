using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    public BaseWeapon[] weapons;
    public int currentWeapon = 0;

    public bool currentLock = false;
    public void ShootCurrentWeapon() {
        BaseWeapon current = weapons[currentWeapon];

        if(current.automatic) {
            current.Shoot();
        } else {
            if(currentLock == false) {
                currentLock = true;
                current.Shoot();
            }
        }

    }

    public void Update() {
        if(Input.GetKeyDown(KeyCode.R)) {
            BaseWeapon current = weapons[currentWeapon];
            current.Reload();
        }
        if(Input.GetMouseButtonUp(0)) currentLock = false;

        if(Input.GetMouseButton(0)) ShootCurrentWeapon();
    }
}
