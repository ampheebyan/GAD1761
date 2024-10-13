using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyObject;
    public float spawnFrequency = 10f;
    public float maxSpawnCount = 15f;

    public Transform patrolPositions;

    private List<Transform> patrolList = new List<Transform>();
    private float spawnCount = 0f;
    private float internalTimer = 0f;

    private void Awake()
    {
        foreach(Transform x in patrolPositions)
        {
            patrolList.Add(x);
        }    
    }

    private void Start()
    {
        // To trigger on game start :)
        internalTimer = spawnFrequency;
    }
    public void Update()
    {
        // Every single time the timer hits the frequency set, spawn a new player, give them our current patrolList and tell them to get started.
        if(internalTimer >= spawnFrequency)
        {
            if(spawnCount != maxSpawnCount)
            {
                internalTimer = 0f;
                spawnCount += 1;

                GameObject cloned = Instantiate(enemyObject);
                cloned.TryGetComponent<EnemyAI>(out EnemyAI ai);
                ai.patrolList = patrolList;
                cloned.transform.position = transform.position;
            }
        } else
        {
            internalTimer += Time.deltaTime;
        }
    }
}
