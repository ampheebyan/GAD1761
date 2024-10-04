using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBubble : MonoBehaviour
{
    private EnemyAI internals;
    private void Awake()
    {
        if (!transform.parent.TryGetComponent<EnemyAI>(out internals))
        {
            throw new System.Exception("No EnemyAI");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) internals.damaging = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player")) internals.damaging = false;
    }
}
