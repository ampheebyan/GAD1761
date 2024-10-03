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

    public weaponType type;
    
    public float fireRate = 1f;
    public float damageOutput = 2f;
    public float reloadTime = 1f;

    public Transform tip;

    public int maxAmmo = 30;

    public UnityEvent onStartReload;
    public UnityEvent onFinishReload;

    private float delta;
    private int currentAmmo = 0;

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

    #region Physical Projectile
    public GameObject projectile;
    public float firingForce = 2f;
    public void ShootProjectile() {
        delta += Time.deltaTime;

        if(delta < fireRate) {
            GameObject temporaryProjectile = (GameObject) Instantiate(projectile, tip.position, projectile.transform.rotation);
            temporaryProjectile.SetActive(true);
            if(temporaryProjectile.TryGetComponent<BaseProjectileObject>(out BaseProjectileObject obj))
                obj.SetBase(this);

            Rigidbody temporaryRigidbody = temporaryProjectile.GetComponent<Rigidbody>();
            temporaryRigidbody.AddForce(firingForce * Vector3.forward, ForceMode.Impulse);
            delta = 0f;
        }
    }
    #endregion

    #region Hitscan

    #endregion

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
