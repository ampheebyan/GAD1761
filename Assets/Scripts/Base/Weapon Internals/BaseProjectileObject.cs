using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseProjectileObject : MonoBehaviour
{
    // Helper function to set the weapon the projectile comes from.
    public void SetBase(BaseWeapon baseProjectile) {
        weapon = baseProjectile;
    }

    // UnityEvent for things that should occur on hit.
    public UnityEvent<Collision> onHit;
    // Maximum bounces/alivetime/destroy area
    public int maxBounces = 2;
    public float maxAliveTime = 10;
    public float yLevelDestroy = -10f;

    // Internal variables
    private int currentBounces = 0;
    private BaseWeapon weapon;
    private float aliveTime;
    
    // Initialise and ensure all variables are clear!
    public void Awake() {
        currentBounces = 0;
        aliveTime = 0;
    }

    // Polling to check if should destroy based on yLevel or maxAliveTime. I don't care if polling is bad sometimes. It's functional for me.
    public void Update() {
        aliveTime += Time.deltaTime;

        if(aliveTime >= maxAliveTime) {
            Destroy(gameObject);
        }

        if(transform.position.y <= yLevelDestroy)
        {
            Destroy(gameObject);
        }
    }

    
    public void OnCollisionEnter(Collision other) {
        // Trigger UnityEvent on hit.
        onHit.Invoke(other);

        if(currentBounces > maxBounces)
        {
            // Handle damage on maxBounces. HandleDamage checks if the collider has the required components.
            weapon.HandleDamage(other.collider);
            Destroy(gameObject);
        } else
        {
            // Increase bounces.
            currentBounces++;
            // Check if the other object is a player, if it is, it should still handle damage.
            if(other.gameObject.CompareTag("Player"))
            {
                weapon.HandleDamage(other.collider);
                Destroy(gameObject);
            }
        }
    }
}
