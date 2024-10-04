using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    public BaseWeapon[] weapons;
    public BaseWeapon currentWeapon;
    public int currentWeaponNum = 0;

    public bool currentLock = false;
    public bool shootingTrigger = false;

    private void Awake()
    {
        currentWeapon = weapons[currentWeaponNum];
    }

    public void Handler()
    {
        if(shootingTrigger == true)
        {
            if (currentWeapon.ammo.x == 0) return;

            if (currentWeapon.automatic)
            {
                currentWeapon.Shoot();
            }
            else
            {
                if (currentLock == false)
                {
                    currentLock = true;
                    currentWeapon.Shoot();
                }
            }
        }
    }

    public void StartShooting() {
        currentWeapon.OnStartShoot();
        shootingTrigger = true;
    }

    public void StopShooting()
    {
        currentWeapon.OnStopShoot();
        shootingTrigger = false;
        currentLock = false;
    }

    public void Update() 
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            currentWeapon.Reload();
        }

        if (Input.GetMouseButtonDown(0)) StartShooting();
        if (Input.GetMouseButtonUp(0)) StopShooting();
        Handler();
    }
}
