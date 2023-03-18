using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycasting : MonoBehaviour
{
    public float apertureAngle;

    void Update()
    {

    }

    public bool CanSeePlayer(float distance, int iterations)
    {
        float angleStep = apertureAngle / (float)iterations;
        Vector3 startingPoint = transform.position;

        RaycastHit hit;

        for (int i = 0; i <= iterations; i++)
        {
            float angle = (-apertureAngle * 0.5f) + (i * angleStep);
            Vector3 sightVector = Quaternion.AngleAxis(angle, transform.up) * transform.forward;

            Debug.DrawRay(startingPoint, sightVector * distance, Color.red);

            if (Physics.Raycast(startingPoint, sightVector, out hit, distance))
            {

                Debug.Log(hit.transform.tag);
                if (hit.transform.tag == "Player")
                {
                    return true;
                 }
            }
        }

        if (Physics.Raycast(startingPoint, transform.forward, out hit, distance)){
            Debug.Log(hit.transform.tag);
            if (hit.transform.tag == "Player")
            {
                return true;
            }
        }

        return false;
    }
}
