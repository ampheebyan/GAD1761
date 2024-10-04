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
                RemoveStamina(0.5f);
                staminaDrainTime = 0f;
            }

            staminaDrainTime += Time.deltaTime;
            staminaResetTime = 0f;
        } else {
            staminaResetTime += Time.deltaTime;
        }

        if(staminaResetTime >= staminaResetSpeed) {
            AddStamina(0.5f);
            staminaResetTime = 0f;
        }
    }
    #endregion


    [Header("Event Triggers")]
    public UnityEvent onLeftMouseButton;
    public UnityEvent onRightMouseButton;

    public void Update() {
        if(Input.GetMouseButtonDown(0)) onLeftMouseButton.Invoke();
        if(Input.GetMouseButtonDown(1)) onRightMouseButton.Invoke();

        StaminaHandler();
    }
}
