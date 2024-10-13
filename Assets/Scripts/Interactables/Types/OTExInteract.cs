using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OTExInteract : BaseInteraction
{
    public bool onlyPlayer = false;
    public void OnTriggerExit (Collider other)
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
