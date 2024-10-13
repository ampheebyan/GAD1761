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

    // Hitpoint handlers
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
    }
}
