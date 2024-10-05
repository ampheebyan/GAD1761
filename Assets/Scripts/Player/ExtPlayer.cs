using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExtPlayer : BasePlayer
{
    [Header("ExtPlayer")]
    public Vector2 stamina = new Vector2 {
        x = 20f,
        y = 20f
    };

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

    public void AddStamina(float value) {
        stamina.x = Mathf.Clamp(stamina.x += value, 0, stamina.y);
    }

    public void RemoveStamina(float value) {
        stamina.x = Mathf.Clamp(stamina.x -= value, 0, stamina.y);
    }

    public void StaminaHandler() {
        if(movementHandler.isMoving[0] && movementHandler.isMoving[1]) {
            if(staminaDrainTime >= staminaDrainSpeed) {
                RemoveStamina((float)System.Math.Round(Random.Range(0.05f, 0.3f), 2));
                staminaDrainTime = 0f;
            }

            staminaDrainTime += Time.deltaTime;
            staminaResetTime = 0f;
        } else {
            staminaResetTime += Time.deltaTime;
        }

        if(staminaResetTime >= staminaResetSpeed && !Input.GetKey(KeyCode.LeftShift)) {
            AddStamina((float)System.Math.Round(Random.Range(0.05f, 0.25f), 2));
            staminaResetTime = 0f;
        }
    }
    #endregion

    public override void OnDeath()
    {
        GlobalReferences.movementHandler.enabled = false;
        GlobalReferences.localPlayerWeapons.enabled = false;
    }
    public void Update() {
        StaminaHandler();
    }
}
