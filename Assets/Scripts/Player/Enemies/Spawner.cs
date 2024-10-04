using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyObject;
    public float spawnFrequency = 10f;
    public float maxSpawnCount = 15f;
    public List<Transform> patrolPositions = new List<Transform>();

    private float spawnCount = 0f;
    private float internalTimer = 0f;
    private void Start()
    {
        // To trigger on game start :)
        internalTimer = spawnFrequency;
    }
    public void Update()
    {
        if(internalTimer >= spawnFrequency)
        {
            if(spawnCount != maxSpawnCount)
            {
                internalTimer = 0f;
                spawnCount += 1;

                GameObject cloned = Instantiate(enemyObject);
                cloned.TryGetComponent<EnemyAI>(out EnemyAI ai);
                ai.patrolPositions = patrolPositions;
                cloned.transform.position = transform.position;
            }
        } else
        {
            internalTimer += Time.deltaTime;
        }
    }
}
