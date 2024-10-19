using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : BaseWeapon
{
    // Simple projectile weapon
    [Header("ProjectileWeapon Properties")]
    public GameObject projectile;
    public Transform tip;
    public float firingForce = 2f;

    public override void Shoot()
    {
        base.Shoot();
        // If reload ignore (safety measure)
        if (reloading) return;
        if (delay >= fireRate) {

            ResetDelay();
            if(ammo.x == 0) return; // If ammo is 0, stop

            // Remove ammo
            ammo.x = Mathf.Clamp(ammo.x - 1, 0, ammo.y);
            // Create new projectile
            GameObject temporaryProjectile = (GameObject) Instantiate(projectile, tip.position, tip.rotation);

            // Normalize so it's not got any funky position/rotation jank
            temporaryProjectile.transform.position.Normalize();
            temporaryProjectile.transform.rotation.Normalize();
            // Show it
            temporaryProjectile.SetActive(true);

            // If it has an actual BOP object, set us as its base.
            if(temporaryProjectile.TryGetComponent<BaseProjectileObject>(out BaseProjectileObject obj))
                obj.SetBase(this);

            // Add force!
            Rigidbody temporaryRigidbody = temporaryProjectile.GetComponent<Rigidbody>();
            temporaryRigidbody.AddForce(firingForce * tip.transform.forward, ForceMode.Impulse);
        }
    }
}
