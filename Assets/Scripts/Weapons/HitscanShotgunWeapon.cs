using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanShotgunWeapon : BaseWeapon
{
    [Header("HitscanShotgunWeapon Properties")]
    public float fireRange = 5f;
    public int shotCount = 3;
    public float spread = 0.1f;
    public GameObject debugVisual;

    public override void Shoot()
    {
        base.Shoot();
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

                    if (debugVisual != null)
                    {
                        GameObject debugVis = Instantiate(debugVisual);
                        debugVis.SetActive(true);
                        debugVis.transform.position = hit.point;
                        debugVis.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
                    }
                }
                spreadVal += spread;
                temp++;
            }

        }

    }
}
