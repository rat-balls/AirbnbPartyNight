using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ChasingAI : MonoBehaviour
{   
    public GameObject playerObj;
    public Transform player;
    public NavMeshAgent enemy;
    private Raycasting raycaster;

    public float maxSightDistance;
    public int sightIterations;
    private float fleetingTimer = 0;

    private void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        raycaster = GetComponent<Raycasting>();
    }

    void Update()
    {

        enemy.SetDestination(player.position);
        transform.LookAt(player.transform);
        enemy.speed = 23;

        float distance = Vector3.Distance(transform.position, player.position);

        if (raycaster.CanSeePlayer(maxSightDistance, sightIterations))
        {   
            if (distance <= 5)
            {
                Destroy(playerObj);
            }
        }
        else
        {
            fleetingTimer += Time.deltaTime;
            if (fleetingTimer >= 3)
            {
                enemy.speed = 2;
                fleetingTimer = 0;
                enabled = false;
                GetComponent<WonderAI>().enabled = true;
                enemy.SetDestination(transform.position);
            }
        }
    }
}