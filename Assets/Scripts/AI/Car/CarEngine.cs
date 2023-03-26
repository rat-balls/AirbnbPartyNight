using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour
{
    public Transform path;
    public float maxSteerAngle = 45f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;

    private List<Transform> nodes;
    private int currentNode = 0;

    void Start()
    {
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
	    nodes = new List<Transform>();

	     for(int i = 0; i < pathTransforms.Length; i++)
	     {
	    	   if(pathTransforms[i] != path.transform)
	    	   {
	    		   nodes.Add(pathTransforms[i]);
	    	   }
	       }
        
    }
    private void Update()
    {
        ApplySteer();
        Drive();
        CheckWaypointDistance();
    }
    private void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        Debug.Log(newSteer);
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
    }
    private void Drive()
    {
        wheelFL.motorTorque = 10f;
        wheelFR.motorTorque = 10f;
    }
    private void CheckWaypointDistance()
    {
        if(Vector3.Distance(transform.position, nodes[currentNode].position) < 0.05f)
        {
            if(currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
    }
}
