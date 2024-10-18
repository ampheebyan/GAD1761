using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeaponHandler : MonoBehaviour
{
    [Header("PlayerWeaponHandler Properties")]
    public BaseWeapon[] weapons;
    public BaseWeapon currentWeapon;
    public int currentWeaponNum = 0;

    public bool currentLock = false;
    public bool shootingTrigger = false;
    public UnityEvent onWeaponChanged;

    private void Awake()
    {
        // Set currentWeapon to whatever the default is.
        currentWeapon = weapons[currentWeaponNum];
    }

    public void Handler()
    {
        // If shooting is triggered
        if(shootingTrigger == true)
        {
            // Stop if ammo.x (current) is 0
            if (currentWeapon.ammo.x == 0) return;

            if (currentWeapon.automatic)
            {
                // If automatic, just keep shooting
                currentWeapon.Shoot();
            }
            else
            {
                // If not automatic, check if lock is false
                if (currentLock == false)
                {
                    // If it is, set it to true and trigger shoot.
                    currentLock = true;
                    currentWeapon.Shoot();
                }
            }
        }
    }
    public void StartShooting() {
        // Start shooting.
        currentWeapon.OnStartShoot();
        shootingTrigger = true;
    }

    public void StopShooting()
    {
        // Stop shooting and reset lock
        currentWeapon.OnStopShoot();
        shootingTrigger = false;
        currentLock = false;
    }

    public void UpdateCurrentWeapon(int newWeapon)
    {
        // If weapon is above weapons.Length stop
        if (newWeapon > weapons.Length - 1) return;
        // If weapon is the same stop
        if (newWeapon == currentWeaponNum && weapons[newWeapon].gameObject.activeSelf == true) return;
        // If weapon not unlocked stop
        if (weapons[newWeapon].unlocked == false) return;
        
        // Set current to be hidden
        currentWeapon.gameObject.SetActive(false);

        // Change associated variables
        currentWeaponNum = newWeapon;
        currentWeapon = weapons[currentWeaponNum];
        
        // Show current
        currentWeapon.gameObject.SetActive(true);

        onWeaponChanged.Invoke();
    }

    

    public void WeaponChangeHandler()
    {
        // Could this be written better?? Sure. I don't care to do any better though.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UpdateCurrentWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UpdateCurrentWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UpdateCurrentWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UpdateCurrentWeapon(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UpdateCurrentWeapon(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            UpdateCurrentWeapon(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            UpdateCurrentWeapon(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        { 
            UpdateCurrentWeapon(7);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            UpdateCurrentWeapon(8);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            UpdateCurrentWeapon(9);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            UpdateCurrentWeapon(0);
        }
    }

    public void Update() 
    {
        // Reload if R is pressed
        if (Input.GetKeyDown(KeyCode.R)) {
            currentWeapon.Reload();
        }

        // Handle start and stop
        if (Input.GetMouseButtonDown(0)) StartShooting();
        if (Input.GetMouseButtonUp(0)) StopShooting();

        // Helper functions
        WeaponChangeHandler();
        Handler();
    }
}
