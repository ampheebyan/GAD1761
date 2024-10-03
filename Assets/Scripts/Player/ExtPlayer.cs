using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExtPlayer : BasePlayer
{
    public UnityEvent onLeftMouseButton;
    public UnityEvent onRightMouseButton;

    public void Update() {
        if(Input.GetMouseButtonDown(0)) onLeftMouseButton.Invoke();
        if(Input.GetMouseButtonDown(1)) onRightMouseButton.Invoke();
    }
}
