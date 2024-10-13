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
        if (inCooldown) return;

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
