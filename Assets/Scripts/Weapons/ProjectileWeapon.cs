using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : BaseWeapon
{
    [Header("ProjectileWeapon Properties")]

    public GameObject projectile;
    public float firingForce = 2f;
    public override void Shoot()
    {
        if(reloading) return;
        
        base.Shoot();
        if(delay >= fireRate) {
            ResetDelay();
            if(ammo.x == 0) return;
            ammo.x = Mathf.Clamp(ammo.x - 1, 0, ammo.y);
            GameObject temporaryProjectile = (GameObject) Instantiate(projectile, tip.position, tip.rotation);
            temporaryProjectile.SetActive(true);
            if(temporaryProjectile.TryGetComponent<BaseProjectileObject>(out BaseProjectileObject obj))
                obj.SetBase(this);

            Rigidbody temporaryRigidbody = temporaryProjectile.GetComponent<Rigidbody>();
            temporaryRigidbody.AddForce(firingForce * tip.transform.forward, ForceMode.Impulse);
        }
    }
}
