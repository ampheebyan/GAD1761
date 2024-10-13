using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : BasePlayer
{
    [Header("EnemyAI Properties")]
    // Speed, time of interest, etc
    public float speed = 5f;
    public float interestTime = 10f;
    public Transform patrolPositions;
    [HideInInspector]
    public List<Transform> patrolList = new List<Transform>();

    [Header("EnemyAI Damage Properties")]
    // How much it should damage in a range
    public Vector2 damage = new Vector2(2, 6);
    public float damageSpeed = 0.5f;

    // Internals
    private float internalInterestTimer = 0f;
    private float internalDamageTimer = 0f;
    private NavMeshAgent agent;
    private ExtPlayer player;
    public bool hitboxTrigger = false;
    public GameObject target;
    private void Awake()
    {
        if(!TryGetComponent<NavMeshAgent>(out agent))
        {
            throw new System.Exception("No NavMeshAgent.");
        } else
        {
            agent.speed = speed;
        }

        // Create list of patrol positions from children.
        if(patrolPositions != null)
        {
            foreach(Transform pos in patrolPositions)
            {
                patrolList.Add(pos);
            }
        }

        internalDamageTimer = damageSpeed;
    }
    void OnDrawGizmos()
    {
        if(agent != null)
            Handles.Label(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), $"{internalInterestTimer}/{interestTime} {hitboxTrigger}");
    }
    private void Update()
    {
        if(target != null) // Do we have a player target?
        {
            // Yep. Follow them.
            agent.SetDestination(target.transform.position);
            target.TryGetComponent<ExtPlayer>(out player);

            if (!hitboxTrigger)
            {
                // If not hitting currently, start interestTimer, and if hits max time, stop interest
                if(internalInterestTimer < interestTime)
                {
                    internalInterestTimer += Time.deltaTime;
                } else
                {
                    target = null;
                    damaging = false;
                    player = null;
                    internalInterestTimer = 0f;
                }
            } else
            {
                internalInterestTimer = 0f;
            }


        }
        else
        {
            if((agent.remainingDistance <= 0 || agent.velocity.magnitude < 0.15f) && patrolList.Count > 0)
            {
                // Random patrolling positions
                agent.SetDestination(patrolList[Random.Range(0, patrolList.Count)].position);
            }
        }

        if(damaging && target != null) // Handle damaging whatever is hit
        {
            if (internalDamageTimer >= damageSpeed)
            {
                player.RemoveHealth(Random.Range(damage.x, damage.y));
                if (player.GetHealth().x <= 0) player.OnDeath();
                internalDamageTimer = 0f;
            } else
            {
                internalDamageTimer += Time.deltaTime;
            }
        }
    }

}

/*
 * It's not pathfinding, per se, but rather just MOVE TOWARDS THE TARGET. NO OTHER OPTION. MOVE TOWARDS THE TARGET kind of AI. I don't like this.
 * I also don't like writing pathfinding, so I'm going to use Unity's navmesh system in future. This can stay as a historical "I did kinda try you know" thing.
 * 
print(Vector3.Distance(transform.position, target.transform.position));
print(target.name); // (yes, I use print as opposed to Debug.Log, it's just better for legibility in my opinion)
if (Vector3.Distance(transform.position, target.transform.position) > closestDistance)
{
    rb.MovePosition(Vector3.MoveTowards(transform.position, target.transform.position, 2 * Time.deltaTime));
    damaging = false;
}
else
{
    target.TryGetComponent<ExtPlayer>(out player);
    damaging = true;
}
transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
*/