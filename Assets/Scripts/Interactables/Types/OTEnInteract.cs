using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OTEnInteract : BaseInteraction
{
    public void OnTriggerEnter(Collider other) {
        onTrigger.Invoke();
    }
}
