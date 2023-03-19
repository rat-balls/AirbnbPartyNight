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
    public float fleetingTimerMax = 1f;
    private float fleetingTimer;

    public float killRange = 8;
    public bool isDead;

    private void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        raycaster = GetComponent<Raycasting>();
    }

    void Update()
    {
        enemy.SetDestination(player.position);
        transform.LookAt(FocusTarget());
        enemy.speed = 23;

        float distance = Vector3.Distance(transform.position, player.position);

        if (raycaster.CanSeePlayer(maxSightDistance, sightIterations))
        {   
            if (distance <= killRange)
            {
                GetComponent<PatrolAI>().enabled = true;
                isDead = true;
                enabled = false;
            }
        }
        else
        {
            fleetingTimer += Time.deltaTime;
            if (fleetingTimer >= fleetingTimerMax)
            {
                enemy.speed = 2;
                fleetingTimer = 0;
                enabled = false;
                GetComponent<WonderAI>().enabled = true;
                enemy.SetDestination(transform.position);
            }
        }
    }

    private Vector3 FocusTarget(){
        Vector3 pos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        return pos;
    }

}