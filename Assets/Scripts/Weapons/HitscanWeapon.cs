using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class HitscanWeapon : BaseWeapon
{
    [Header("HitscanWeapon Properties")]
    public float fireRange = 5f;
    public GameObject hitPoint;
    public GameObject debugVisual;
    public virtual void HitPoint(RaycastHit hit)
    {
        if (hitPoint != null)
        {
            GameObject newHitPoint = Instantiate(hitPoint, hit.point, Quaternion.identity);
            newHitPoint.transform.SetParent(hit.transform);
            newHitPoint.SetActive(true);
        }
    }
    public virtual void DebugHitPoint(RaycastHit hit)
    {
        if (debugVisual != null)
        {
            GameObject debugVis = Instantiate(debugVisual);
            debugVis.SetActive(true);
            debugVis.transform.position = hit.point;
            debugVis.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
        }
    }

    public override void Shoot()
    {
        base.Shoot();
        if (delay >= fireRate)
        {
            ResetDelay();
            if (ammo.x == 0) return;

            ammo.x = Mathf.Clamp(ammo.x - 1, 0, ammo.y);

            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f)), out RaycastHit hit, fireRange))
            {
                if(hit.collider.gameObject.TryGetComponent<BasePlayer>(out BasePlayer player))
                {
                    HandleDamage(hit.collider);
                }

                HitPoint(hit);

                DebugHitPoint(hit);
            }
        }

    }
}
