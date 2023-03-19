using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonderAI : MonoBehaviour
{
    public float Speed = 3f;
    public float SpeedRotation = 100f;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;
    private Raycasting raycaster;

    public float maxSightDistance;
    public int sightIterations;

    private float timer = 0f;

    void Start()
    {
        raycaster = GetComponent<Raycasting>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWandering == false)
        {
            StartCoroutine(Wander());
        }
        if (isRotatingRight == true)
        {
            //gameObjet.GetComponent<Animator>().Play("");
            transform.Rotate(transform.up * Time.deltaTime * SpeedRotation);

        }
        if (isRotatingLeft == true)
        {
            //gameObjet.GetComponent<Animator>().Play("");
            transform.Rotate(transform.up * Time.deltaTime * -SpeedRotation);

        }
        if (isWalking == true)
        {
            //gameObjet.GetComponent<Animator>().Play("");
            transform.position += transform.forward * Time.deltaTime * Speed;

        }

        if (raycaster.CanSeePlayer(maxSightDistance, sightIterations))
        {
            enabled = false;
            GetComponent<LookingAI>().enabled = true;
            timer = 0;
            StopAllCoroutines();
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= 7)
            {
                enabled = false;
                GetComponent<PatrolAI>().enabled = true;
                timer = 0;
                StopAllCoroutines();
            }
        }
    }

    IEnumerator Wander()
    {
        int rotTime = Random.Range(1, 2);
        int rotWait = 0;
        int rotLorR = Random.Range(1, 2);
        int walkWait = 0;
        int walkTime = Random.Range(1, 6);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(rotWait);
        if (rotLorR == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;
        }
        if (rotLorR == 2)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;
        }
        isWandering = false;
    }
}
