using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVolume : MonoBehaviour
{
    public BaseCharacter internals;
    public bool shouldOnlyDamagePlayer = false;

    private void OnTriggerEnter(Collider other)
    {
        // If find baseCharacter
        if(other.TryGetComponent<BaseCharacter>(out BaseCharacter baseCharacter))
        {
            // Ensure not null
            if (baseCharacter != null)
            {
                // If should only damage player
                if(shouldOnlyDamagePlayer) 
                { 
                    // Check if player
                    if(other.CompareTag("Player"))
                    {
                        // Yes!
                        internals.damaging = true;
                        internals.currentlyDamaging = baseCharacter;
                    }
                } else
                {
                    // Set currently damaging
                    internals.damaging = true;
                    internals.currentlyDamaging = baseCharacter;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<BaseCharacter>(out BaseCharacter baseCharacter))
        {
            if (baseCharacter != null)
            {
                if(baseCharacter == internals.currentlyDamaging)
                {
                    internals.damaging = false;
                    internals.currentlyDamaging = null;
                }
            }
        }
    }
}
