using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVolume : MonoBehaviour
{
    public EnemyAI internals;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) internals.damaging = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player")) internals.damaging = false;
    }
}
