using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReparentTrigger : MonoBehaviour
{
    protected void OnTriggerEnter(Collider other) {
        other.transform.parent = transform;
    }

    protected void OnTriggerExit(Collider other) {
        other.transform.parent = null;
    }
}
