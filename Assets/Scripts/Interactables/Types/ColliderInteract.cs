using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderInteract : BaseInteraction
{
    // Simple interactable object that fires UnityEvent if collider hit.
    public bool onlyPlayer = false;
    public void OnCollisionEnter (Collision other)
    {
        if (onlyPlayer) 
        {
            if(other.gameObject.CompareTag("Player"))
                onTrigger.Invoke();
        } else 
        {
            onTrigger.Invoke();
        }
    }
}
