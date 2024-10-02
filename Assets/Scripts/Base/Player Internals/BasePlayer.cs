using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    #region Health
    private float health = 100f;
    private float maxHealth = 100f;

    // Helper functions to get/set health.
    public float GetHealth()
    {
        return health;
    }
    public bool SetHealth(float value)
    {
        health = Mathf.Clamp(value, 0, maxHealth);
        return true;
    }
    public bool AddHealth(float value)
    {
        health = Mathf.Clamp(health + value, 0, maxHealth);
        return true;
    }
    public bool RemoveHealth(float value)
    {
        health = Mathf.Clamp(health - value, 0, maxHealth);
        return true;
    }
    #endregion
}
