using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    [Header("BasePlayer Properties")]
    // This should be visible in the inpsector.
    [SerializeField]
    private Vector2 health = new Vector2(100f, 100f);

    #region Health

    // Helper functions to get/set health.
    public Vector2 GetHealth()
    {
        return health;
    }
    public bool SetHealth(float value)
    {
        health.x = Mathf.Clamp(value, 0, health.y);
        return true;
    }
    public bool AddHealth(float value)
    {
        health.x = Mathf.Clamp(health.x + value, 0, health.y);
        return true;
    }
    public bool RemoveHealth(float value)
    {
        health.x = Mathf.Clamp(health.x - value, 0, health.y);
        return true;
    }
    #endregion

    public virtual void OnDeath()
    {

    }

    private void Update()
    {
        if(health.x <= 0)
        {
            OnDeath();
        }
    }
}
