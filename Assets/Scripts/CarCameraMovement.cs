using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCameraMovement : MonoBehaviour
{
    public float SpeedV = 0.2f;
    public float SpeedH = 0.2f;
    public Vector2 ClampX;
    public Vector2 ClampY;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    // Update is called once per frame
    void Update()
    {
        rotationY += SpeedV * Input.GetAxis("Mouse X");
        rotationX -= SpeedH * Input.GetAxis("Mouse Y");

        rotationX = Mathf.Clamp(rotationX, ClampX.x, ClampX.y);
        rotationY = Mathf.Clamp(rotationY, ClampY.x, ClampY.y);


        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
    }
}
