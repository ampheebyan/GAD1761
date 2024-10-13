using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVolume : MonoBehaviour
{
    public BaseCharacter internals;
    public bool shouldOnlyDamagePlayer = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<BaseCharacter>(out BaseCharacter baseCharacter))
        {
            if (baseCharacter != null)
            {
                if(shouldOnlyDamagePlayer) 
                { 
                    if(other.CompareTag("Player"))
                    {
                        internals.damaging = true;
                        internals.currentlyDamaging = baseCharacter;
                    }
                } else
                {
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
