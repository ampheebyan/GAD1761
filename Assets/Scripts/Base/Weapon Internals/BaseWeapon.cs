using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseWeapon : MonoBehaviour
{
    public float fireRate = 1f;
    public float damageOutput = 2f;
    public float reloadTime = 1f;

    public Transform tip;

    public int maxAmmo = 30;
    private int currentAmmo = 0;

    public UnityEvent onStartReload;
    public UnityEvent onFinishReload;

    private void Awake()
    {
        currentAmmo = maxAmmo;
    }

    public void HandleDamage(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            if(other.gameObject.TryGetComponent<ExtPlayer>(out ExtPlayer player)) {
                player.RemoveHealth(damageOutput);                
            }
        }
    }

    #region Ammo Handling
    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public void Reload()
    {
        StartCoroutine(ReloadCoroutine());
    }

    IEnumerator ReloadCoroutine()
    {
        // Trigger onStartReload
        onStartReload.Invoke();

        // Wait
        yield return new WaitForSeconds(reloadTime);

        // Set ammo correspondingly
        currentAmmo = maxAmmo;

        // Trigger onFinishReload
        onFinishReload.Invoke();
    }
    #endregion
}
