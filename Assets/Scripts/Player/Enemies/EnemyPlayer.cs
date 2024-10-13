using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class EnemyPlayer : BasePlayer
{
    // Simple enemy class, exist to have a reference to enemy specific classes, and enemy specific death logic.
    public override void OnDeath()
    {
        base.OnDeath();

        Debug.Log("EnemyPlayer: OnDeath()");
        Destroy(transform.parent.gameObject);
    }

    void OnDrawGizmos()
    {
        Handles.Label(transform.position, $"{transform.parent.gameObject.name}: {GetHealth().x} / {GetHealth().y}");
    }
}
