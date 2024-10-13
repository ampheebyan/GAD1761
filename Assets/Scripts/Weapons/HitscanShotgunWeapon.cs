using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanShotgunWeapon : HitscanWeapon
{
    [Header("HitscanShotgunWeapon Properties")]
    public int shotCount = 3;
    public float spread = 0.1f;

    public override void Shoot()
    {
        base.Shoot(); // Trigger base.Shoot();
        if (delay >= fireRate)
        {
            ResetDelay();
            if (ammo.x == 0) return; // Don't shoot if the ammo is 0.

            // Remove ammo
            ammo.x = Mathf.Clamp(ammo.x - 1, 0, ammo.y);

            // Figure out the spread
            float spreadVal = 0.5f - spread;
            
            // Temporary variable to house how many shots to fire
            int temp = 0;
            while(temp < shotCount)
            {
                // Find player from center of camera but with altered spread point
                if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(spreadVal,Random.Range(0.5f - spread, 0.5f + spread))), out RaycastHit hit, fireRange))
                {
                    if (hit.collider.gameObject.TryGetComponent<BasePlayer>(out BasePlayer player))
                    {
                        // Damage if found
                        HandleDamage(hit.collider);
                    }

                    // Show hitpoints
                    HitPoint(hit);

                    DebugHitPoint(hit);
                }
                // Alter spread point
                spreadVal += spread;
                temp++;
            }

        }

    }
}
