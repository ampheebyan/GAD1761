using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayer : BasePlayer
{

    public override void OnDeath()
    {
        base.OnDeath();

        print("Enemy died!");
        Destroy(transform.parent.gameObject);
    }

}
