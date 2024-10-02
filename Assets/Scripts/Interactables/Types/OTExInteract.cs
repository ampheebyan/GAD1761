using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OTExInteract : BaseInteraction
{
    public void OnTriggerExit(Collider other) {
        onTrigger.Invoke();
    }
}
