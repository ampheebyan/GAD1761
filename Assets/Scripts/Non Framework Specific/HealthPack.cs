using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public void Run(float health)
    {
            GlobalReferences.localPlayer.AddHealth(health);
    }
}
