using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : BaseWeapon
{
    public GameObject projectile;
    public float firingForce = 2f;

    public void Shoot() {
        GameObject temporaryProjectile = (GameObject) Instantiate(projectile, tip.position, projectile.transform.rotation);
        
        if(temporaryProjectile.TryGetComponent<BaseProjectileObject>(out BaseProjectileObject obj))
            obj.SetBase(this);

        Rigidbody temporaryRigidbody = temporaryProjectile.GetComponent<Rigidbody>();
        temporaryRigidbody.AddForce(firingForce * Vector3.forward, ForceMode.Impulse);
    }


}
