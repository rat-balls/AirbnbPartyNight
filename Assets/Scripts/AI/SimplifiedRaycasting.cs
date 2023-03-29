using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimplifiedRaycasting : MonoBehaviour
{
    public float distance = 10;
    private float cHeight;
    private GameObject player;
    void Start(){
        cHeight = GetComponent<NavMeshAgent>().height; //Gets height
        player = GameObject.FindWithTag("Player");
    }
    public bool CanSeePlayer(float distance){
        Vector3 startingPoint = transform.position;
        startingPoint.y = transform.position.y+cHeight/2 ; //makes starting point middle of ting
        Debug.DrawRay(startingPoint, player.transform.position - startingPoint);
        
        RaycastHit hit;
        
        if (Physics.Raycast(startingPoint, player.transform.position, out hit, distance)){
            if (hit.transform.tag == "Player" && !GetComponent<ChasingAI>().isDead)
            {
                return true;
            }
        }
        return false;
    }
}
