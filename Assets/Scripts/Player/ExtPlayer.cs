using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExtPlayer : BasePlayer
{
    [Header("ExtPlayer Properties")]
    // x = current, y = max
    public Vector2 stamina = new Vector2 {
        x = 20f,
        y = 20f
    };

    // Reset and drain speeds
    public float staminaResetSpeed = .75f;
    public float staminaDrainSpeed = .25f;

    private MovementHandler movementHandler;

    public void Awake() 
    {
        if(!TryGetComponent<MovementHandler>(out movementHandler)) {
            throw new System.Exception("No MovementHandler on Player object!");
        }
    }

    #region Stamina
    private float staminaResetTime = 0f;
    private float staminaDrainTime = 0f;

    // Add/remove helper functions
    public void AddStamina(float value) {
        stamina.x = Mathf.Clamp(stamina.x += value, 0, stamina.y);
    }

    public void RemoveStamina(float value) {
        stamina.x = Mathf.Clamp(stamina.x -= value, 0, stamina.y);
    }

    public void StaminaHandler() {
        // If the player is moving but also sprinting
        if(movementHandler.isMoving[0] && movementHandler.isMoving[1]) {
            if(staminaDrainTime >= staminaDrainSpeed) {
                // Drain stamina
                RemoveStamina((float)System.Math.Round(Random.Range(0.05f, 0.3f), 2));
                staminaDrainTime = 0f;
            }
            staminaDrainTime += Time.deltaTime;
            staminaResetTime = 0f;
        } else {
            staminaResetTime += Time.deltaTime;
        }

        if(staminaResetTime >= staminaResetSpeed && !Input.GetKey(movementHandler.sprintKey)) {
            // If not sprinting and also not holding sprintKey, increase slowly.
            AddStamina((float)System.Math.Round(Random.Range(0.05f, 0.25f), 2));
            staminaResetTime = 0f;
        }
    }
    #endregion

    public override void OnDeath()
    {
        base.OnDeath();
        Debug.Log("ExtPlayer: OnDeath()");
        
        GlobalReferences.movementHandler.enabled = false;
        GlobalReferences.localPlayerWeapons.enabled = false;
    }
    public void Update() {
        StaminaHandler();
    }
}
