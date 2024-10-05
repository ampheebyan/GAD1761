using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionVolume : MonoBehaviour
{
    public EnemyAI internals;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            internals.target = other.gameObject;
            internals.hitboxTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            internals.hitboxTrigger = false;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            internals.hitboxTrigger = true;
    }
}
