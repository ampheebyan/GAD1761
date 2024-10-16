using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanBasicWeapon : HitscanWeapon
{
    // Basic single shot weapon derived from HitscanWeapon
    public override void Shoot()
    {
        base.Shoot(); // Trigger base.Shoot();
        // If reload ignore (safety measure)
        if (reloading) return;
        if (delay >= fireRate)
        {
            ResetDelay();
            // If ammo is 0 stop
            if (ammo.x == 0) return;

            // Remove ammo
            ammo.x = Mathf.Clamp(ammo.x - 1, 0, ammo.y);

            // Raycast to find player from center of camera.
            if (Physics.Raycast(cameraPos != null ? new Ray(cameraPos.position, cameraPos.forward) : Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f)), out RaycastHit hit, fireRange))
            {
                if(hit.collider.gameObject.TryGetComponent<BasePlayer>(out BasePlayer player))
                {
                    // If found, trigger damage
                    HandleDamage(hit.collider);
                }

                // Show hitpoints
                HitPoint(hit);

                DebugHitPoint(hit);
            }
        }

    }
}
