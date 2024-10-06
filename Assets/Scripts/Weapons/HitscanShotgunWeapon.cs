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
        if (delay >= fireRate)
        {
            ResetDelay();
            if (ammo.x == 0) return;

            ammo.x = Mathf.Clamp(ammo.x - 1, 0, ammo.y);

            float spreadVal = 0.5f - spread;
            int temp = 0;
            while(temp < shotCount)
            {
                if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(spreadVal,Random.Range(0.5f - spread, 0.5f + spread))), out RaycastHit hit, fireRange))
                {
                    if (hit.collider.gameObject.TryGetComponent<BasePlayer>(out BasePlayer player))
                    {
                        HandleDamage(hit.collider);
                    }

                    HitPoint(hit);

                    DebugHitPoint(hit);
                }
                spreadVal += spread;
                temp++;
            }

        }

    }
}
