using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseProjectileObject : MonoBehaviour
{
    public void SetBase(BaseWeapon baseProjectile) {
        weapon = baseProjectile;
    }
    public UnityEvent<Collision> onHit;
    public int maxBounces = 2;
    private int currentBounces = 0;
    private BaseWeapon weapon;

    public void Awake() {
        currentBounces = 0;
    }

    public void OnCollisionEnter(Collision other) {
        if(currentBounces < maxBounces) {
            currentBounces += 1;
        } else {
            onHit.Invoke(other);
            Destroy(gameObject);
        }
    }
}
