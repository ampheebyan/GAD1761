using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseWeapon : MonoBehaviour
{
    public enum weaponType {
        physical,
        hitscan
    }

    [Header("BaseWeapon Properties")]
    public weaponType type;

    public bool automatic = false;
    public float fireRate = 0.05f;
    public float damageOutput = 2f;
    public float reloadTime = 1f;

    public Transform tip;

    public Vector2Int ammo = new Vector2Int(0,30);

    public UnityEvent onStartReload;
    public UnityEvent onFinishReload;

    public float delay = 0;
    public bool reloading = false;
    private void Awake()
    {
        ammo.x = ammo.y;
        delay = fireRate;
    }

    public virtual void HandleDamage(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            if(other.gameObject.TryGetComponent<ExtPlayer>(out ExtPlayer player)) {
                player.RemoveHealth(damageOutput);                
            }
        }
    }
        
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

    public virtual void Shoot() {

    }

    #region Ammo Handling
    public int GetCurrentAmmo()
    {
        return ammo.x;
    }

    public void Reload()
    {
        StartCoroutine(ReloadCoroutine());
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

        // Trigger onFinishReload
        onFinishReload.Invoke();
    }
    #endregion
}
