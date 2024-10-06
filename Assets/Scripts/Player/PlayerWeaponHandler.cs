using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public void UpdateCurrentWeapon(int newWeapon)
    {
        if (newWeapon > weapons.Length) return;
        if (newWeapon == currentWeaponNum) return;
        if (weapons[newWeapon].unlocked == false) return;
        
        currentWeapon.gameObject.SetActive(false);
        currentWeaponNum = newWeapon;
        currentWeapon = weapons[currentWeaponNum];
        currentWeapon.gameObject.SetActive(true);
    }
    public void Update() 
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            currentWeapon.Reload();
        }

        if (Input.GetMouseButtonDown(0)) StartShooting();
        if (Input.GetMouseButtonUp(0)) StopShooting();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UpdateCurrentWeapon(0);
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UpdateCurrentWeapon(1);
        } else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UpdateCurrentWeapon(2);
        } else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UpdateCurrentWeapon(3);
        } else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UpdateCurrentWeapon(4);
        } else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            UpdateCurrentWeapon(5);
        }

        Handler();
    }
}
