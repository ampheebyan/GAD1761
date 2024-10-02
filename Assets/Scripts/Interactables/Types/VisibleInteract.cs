using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleInteract : BaseInteraction
{
    public void Trigger() {
        onTrigger.Invoke();
    }
}
