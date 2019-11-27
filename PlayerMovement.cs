using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Range(1f, 50f)]
    public float playerSpeed = 2f;
    [Range(10f, 20f)]
    public float tiltAngle = 10f;
    Vector2 Movforward;
    Vector2 Rotation;

    
    private void Update()
    {
        checkMovement();
        checkRotation();
    }

    private void checkRotation()
    {       
        Rotation = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick)*tiltAngle;
        Quaternion target = Quaternion.Euler(0, Rotation.x*Time.deltaTime * 5, 0);
        target.eulerAngles += transform.rotation.eulerAngles;
        transform.rotation = target;
    }

    private void checkMovement()
    {
        Movforward = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        transform.localPosition += transform.forward * Movforward.y * Time.deltaTime * playerSpeed;

        transform.localPosition += transform.right * Movforward.x * Time.deltaTime * playerSpeed;
    }
}
