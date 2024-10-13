using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    [Header("InteractionHandler Properties")]
    public float maxRange = 5f;
    public float interactionCooldown = 3f;

    private bool inCooldown = false;
    private float cooldownTimer = 0f;

    private void Update()
    {
        // Cooldown timer handling. Could be done with coroutines but I'm more partial to this method.
        if(inCooldown)
        {
            if(cooldownTimer >= interactionCooldown)
            {
                inCooldown = false;
                cooldownTimer = 0f;
            } else
            {
                cooldownTimer += Time.deltaTime;
            }
        }
    }
    public void Raycast()
    {
        // Ignore if in cooldown
        if (inCooldown) return;

        // Raycast from center of screen, then trigger if VisibleInteract is found before hit maxRange. Start cooldown!
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f)), out RaycastHit hit, maxRange))
        {
            if (hit.collider.gameObject.TryGetComponent<VisibleInteract>(out VisibleInteract interact))
            {
                interact.Trigger();
                inCooldown = true;
            }
        }
    }
}
