using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingAI : MonoBehaviour
{
    public GameObject player;
    private Raycasting raycaster;

    public float maxSightDistance;
    public int sightIterations;

    public float fleetingTimerMax = 1f;
    private float fleetingTimer;

    public float lookingTimerMax = 1f;
    private float  lookingTimer;


    void Start()
    {
        raycaster = GetComponent<Raycasting>();
    }

    void Update()
    {
        transform.LookAt(FocusTarget());

        if (raycaster.CanSeePlayer(maxSightDistance, sightIterations))
        {
            lookingTimer += Time.deltaTime;

            if (lookingTimer >= lookingTimerMax)
            {
                GetComponent<ChasingAI>().enabled = true;
                lookingTimer = 0f;
                fleetingTimer = 0f;
                enabled = false;
            }
        }
        else
        {
            fleetingTimer += Time.deltaTime;

            if (fleetingTimer >= fleetingTimerMax)
            {
                enabled = false;
                GetComponent<WonderAI>().enabled = true;
                fleetingTimer = 0f;
                lookingTimer = 0f;
            }
        }
    }
    
    private Vector3 FocusTarget(){
        Vector3 pos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        return pos;
    }
}
