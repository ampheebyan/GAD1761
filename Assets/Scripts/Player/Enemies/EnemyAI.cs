using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("EnemyAI Properties")]
    public GameObject target;
    public float closestDistance = 1.1f;
    public float damagePerSecond = 5;
    public bool damaging = false;
    public float speed = 5f;

    public List<Transform> patrolPositions = new List<Transform>();

    private float damageTimer = 0f;
    private NavMeshAgent agent;
    private ExtPlayer player;
    private void Awake()
    {
        if(!TryGetComponent<NavMeshAgent>(out agent))
        {
            throw new System.Exception("No NavMeshAgent.");
        } else
        {
            agent.speed = speed;
        }
    }
    void OnDrawGizmos()
    {
        if(agent != null)
            Handles.Label(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), $"{agent.remainingDistance}");
    }
    private void Update()
    {
        if(target != null)
        {
            agent.SetDestination(target.transform.position);
            target.TryGetComponent<ExtPlayer>(out player);
            /*
             * It's not pathfinding, per se, but rather just MOVE TOWARDS THE TARGET. NO OTHER OPTION. MOVE TOWARDS THE TARGET kind of AI. I don't like this.
             * I also don't like writing pathfinding, so I'm going to use Unity's navmesh system in future. This can stay as a historical "I did kinda try you know" thing.
             * 
            print(Vector3.Distance(transform.position, target.transform.position));
            print(target.name);
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
        }
        else
        {
            if(agent.remainingDistance <= 0 || agent.velocity.magnitude < 0.15f)
            {
                agent.SetDestination(patrolPositions[Random.Range(0, patrolPositions.Count)].position);
            }
        }

        if(damaging)
        {
            if (damageTimer >= 1)
            {
                player.RemoveHealth(damagePerSecond);
                damageTimer = 0f;
            } else
            {
                damageTimer += Time.deltaTime;
            }
        }
    }

}
