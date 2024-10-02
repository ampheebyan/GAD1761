using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderInteract : BaseInteraction
{
    public bool onlyPlayer = false;
    public void OnCollisionEnter(Collision other) {
        if(onlyPlayer) {
            if(other.gameObject.CompareTag("Player"))
                onTrigger.Invoke();
        } else {
            onTrigger.Invoke();
        }
    }
}
