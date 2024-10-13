using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanHitPoint : MonoBehaviour
{
    // Simple object that will destroy itself after destroyTime.
    public float destroyTime = 5f;
    private void Awake()
    {
        Destroy(gameObject, destroyTime);
    }
}
