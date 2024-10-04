using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionBubble : MonoBehaviour
{
    private EnemyAI internals;

    public float followTime = 12f;
    private float followTimeInternal = 0f;

    private bool inTrigger = false;

    private void Awake()
    {
        if(!transform.parent.TryGetComponent<EnemyAI>(out internals))
        {
            throw new System.Exception("No EnemyAI");
        }
    }

    private void Update()
    {
        if(inTrigger == false && internals.target != null)
        {
            followTimeInternal += Time.deltaTime;
        }

        if(followTimeInternal >= followTime && internals.target != null)
        {
            internals.target = null;
            followTimeInternal = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            internals.target = other.gameObject;
            inTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            inTrigger = false;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
            followTimeInternal = 0;
    }
}
