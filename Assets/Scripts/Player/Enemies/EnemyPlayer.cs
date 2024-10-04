using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class EnemyPlayer : BasePlayer
{
    public override void OnDeath()
    {
        base.OnDeath();

        Destroy(transform.parent.gameObject);
    }

    void OnDrawGizmos()
    {
        Handles.Label(transform.position, $"{transform.parent.gameObject.name}: {GetHealth().x} / {GetHealth().y}");
    }
}
