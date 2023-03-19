using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinkingAI : MonoBehaviour
{

    public GameObject player;
    public float thinkingTimerMax = 1f;
    private float  thinkingTimer;

    public float fleetingTimerMax = 1f;
    private float fleetingTimer;
    private Raycasting raycaster;

    public float maxSightDistance = 60;
    public int sightIterations = 100;

    void Start()
    {
        raycaster = GetComponent<Raycasting>();
    }

    void Update()
    {
        if (raycaster.CanSeePlayer(maxSightDistance, sightIterations))
        {
            thinkingTimer += Time.deltaTime;

            if (thinkingTimer >= thinkingTimerMax)
            {
                GetComponent<ChasingAI>().enabled = true;
                thinkingTimer = 0f;
                fleetingTimer = 0f;
                enabled = false;
            }
        }
        else
        {
            fleetingTimer += Time.deltaTime;

            if (fleetingTimer >= fleetingTimerMax)
            {
                GetComponent<WonderAI>().enabled = true;
                thinkingTimer = 0f;
                fleetingTimer = 0f;
                enabled = false;
            }
        }
    }
}
