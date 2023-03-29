using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Raycasting : MonoBehaviour
{
    public float apertureAngle = 300;
    private float cHeight;

    void Start(){
        cHeight = GetComponent<NavMeshAgent>().height;
    }

    public bool CanSeePlayer(float distance, int iterations)
    {
        float angleStep = apertureAngle / (float)iterations;
        Vector3 startingPoint = transform.position;
        startingPoint.y = transform.position.y + cHeight/2;

        RaycastHit hit;

        for (int i = 0; i <= iterations; i++)
        {
            float angle = (-apertureAngle * 0.5f) + (i * angleStep);
            Vector3 sightVector = Quaternion.AngleAxis(angle, transform.up) * transform.forward;

            Debug.DrawRay(startingPoint, sightVector * distance, Color.red);

            if (Physics.Raycast(startingPoint, sightVector, out hit, distance))
            {
                if (hit.transform.tag == "Player" && !GetComponent<ChasingAI>().isDead)
                {
                    return true;
                }
            }
        }

        if (Physics.Raycast(startingPoint, transform.forward, out hit, distance)){
            if (hit.transform.tag == "Player" && !GetComponent<ChasingAI>().isDead)
            {
                return true;
            }
        }

        return false;
    }
}
