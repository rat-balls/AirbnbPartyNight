using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingAI : MonoBehaviour
{
    public GameObject player;
    private float currentLookingTime = 0f;
    private Raycasting raycaster;

    public float maxSightDistance;
    public int sightIterations;
    private float Fleetingtimer = 0f;


    void Start()
    {
        raycaster = GetComponent<Raycasting>();
    }

    void Update()
    {
        
        transform.LookAt(player.transform);

        if (raycaster.CanSeePlayer(maxSightDistance, sightIterations))
        {
            currentLookingTime += Time.deltaTime;

            if (currentLookingTime >= 3f)
            {
                enabled = false;
                GetComponent<ChasingAI>().enabled = true;
                currentLookingTime = 0;
                Fleetingtimer = 0;


            }
        }
        else
        {
            Fleetingtimer += Time.deltaTime;

            if (Fleetingtimer >= 1f)
            {
                currentLookingTime = 0;
                enabled = false;
                GetComponent<WonderAI>().enabled = true;
                Fleetingtimer = 0f;
            }
        }
 
    }
    

}
