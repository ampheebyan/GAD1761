using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BaseWeapon : MonoBehaviour
{
    [Header("BaseWeapon Properties")]
    public bool unlocked = false;
    // Weapon properties
    public bool automatic = false;
    public float fireRate = 0.05f;
    public float damageOutput = 2f;
    public float reloadTime = 1f;

    // x = current, y = max
    public Vector2Int ammo = new Vector2Int(0,30);

    // UnityEvents for specific actions
    public UnityEvent onStartReload;
    public UnityEvent onFinishReload;

    public UnityEvent onShoot;

    // API variables
    public float delay = 0;
    public bool reloading = false;
    private Coroutine currentReload;
    private void Awake()
    {
        ammo.x = ammo.y;
        delay = fireRate;
    }

    #region Damage
    // Damage handling.
    public virtual void HandleDamage(Collider other) {
        // Find BasePlayer and remove damage based on the damageOutput variable
        if(other.gameObject.TryGetComponent<BasePlayer>(out BasePlayer player)) {
            player.RemoveHealth(damageOutput);  
                
            // If player dies, trigger OnDeath
            if(player.GetHealth().x <= 0)
            {
                player.OnDeath();
            }
        }
    }
    #endregion
    protected void Update()
    {
        DelayHandler();
    }

    #region Fire Rate
    private void DelayHandler() 
    {
        // If fireRate bigger, add to delay.
        if(delay < fireRate) {
            delay += Time.deltaTime;
        }
    }

    public void ResetDelay() 
    {
        // Reset Delay.
        delay = 0f;
    }

    #endregion

    #region Shooting
    public virtual void Shoot() 
    {
        // If reload ignore
        if (reloading) return;
        // Trigger onShoot UnityEvent
        onShoot.Invoke();

        // Let derived classes determine how shooting logic works.
    }

    public virtual void OnStartShoot()
    {
        // Placeholder so derived classes can have logic here.
    }

    public virtual void OnStopShoot()
    {
        // Placeholder so derived classes can have logic here.
    }
    #endregion

    #region Ammo Handling
    public int GetCurrentAmmo()
    {
        // Return current ammo.
        return ammo.x;
    }

    public void Reload()
    {
        // Start reload coroutine.
        currentReload = StartCoroutine(ReloadCoroutine());
    }

    protected IEnumerator ReloadCoroutine()
    {
        // Trigger onStartReload
        onStartReload.Invoke();

        reloading = true;

        // Wait
        yield return new WaitForSeconds(reloadTime);

        // Set ammo correspondingly
        ammo.x = ammo.y;

        reloading = false;

        currentReload = null;
        // Trigger onFinishReload
        onFinishReload.Invoke();
    }
    #endregion

    #region Disable/Enable Handling
    private void OnDisable()
    {
        // If disabled, stop reloading and trigger what happens after reloading.
        if (reloading == true)
        {
            StopCoroutine(currentReload);
            reloading = false;
            onFinishReload.Invoke();
        }
    }
    #endregion

    #region
    public void SetUnlocked(bool value)
    {
        // Set unlocked to whatever is provided.
        unlocked = value;
    }
    #endregion
}
