using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject target;

    private ExtPlayer player;
    private Rigidbody rb;

    public float closestDistance = 1.1f;

    public float damagePerSecond = 5;

    private bool damaging = false;
    private float damageTimer = 0f;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(target != null)
        {
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
