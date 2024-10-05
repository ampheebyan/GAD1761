using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseWeapon : MonoBehaviour
{
    [Header("BaseWeapon Properties")]
    public bool unlocked = false;

    public bool automatic = false;
    public float fireRate = 0.05f;
    public float damageOutput = 2f;
    public float reloadTime = 1f;

    public Vector2Int ammo = new Vector2Int(0,30);

    public UnityEvent onStartReload;
    public UnityEvent onFinishReload;

    public UnityEvent onShoot;

    public float delay = 0;
    public bool reloading = false;
    private Coroutine currentReload;
    private void Awake()
    {
        ammo.x = ammo.y;
        delay = fireRate;
    }

    #region Damage
    public virtual void HandleDamage(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            if(other.gameObject.TryGetComponent<BasePlayer>(out BasePlayer player)) {
                player.RemoveHealth(damageOutput);                
            }
        }
    }
    #endregion

    #region Fire Rate
    protected void Update() {
        if(delay < fireRate) {
            delay += Time.deltaTime;
        }
    }

    public void ResetDelay() {
        delay = 0f;
    }

    #endregion

    #region Shooting
    public virtual void Shoot() {
        if (reloading) return;
        onShoot.Invoke();
    }

    public virtual void OnStartShoot()
    {
    }

    public virtual void OnStopShoot()
    {
    }
    #endregion

    #region Ammo Handling
    public int GetCurrentAmmo()
    {
        return ammo.x;
    }

    public void Reload()
    {
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
        unlocked = value;
    }
    #endregion
}
