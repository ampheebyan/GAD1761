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
    public float maxAliveTime = 10;
    public float yLevelDestroy = -10f;

    
    private int currentBounces = 0;
    private BaseWeapon weapon;
    private float aliveTime;
    
    public void Awake() {
        currentBounces = 0;
        aliveTime = 0;
    }

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
        onHit.Invoke(other);

        if(currentBounces > maxBounces)
        {
            weapon.HandleDamage(other.collider);
            Destroy(gameObject);
        } else
        {
            currentBounces++;
            if(other.gameObject.CompareTag("Player"))
            {
                weapon.HandleDamage(other.collider);
                Destroy(gameObject);
            }
        }
    }
}
