using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OTEnInteract : BaseInteraction
{
    public bool onlyPlayer = false;
    public void OnTriggerEnter(Collider other)
    {
        if (onlyPlayer)
        {
            if (other.gameObject.CompareTag("Player"))
                onTrigger.Invoke();
        }
        else
        {
            onTrigger.Invoke();
        }
    }
}
