using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float minZoom, maxZoom;


    private float initialZ;

    private void Start()
    {
        initialZ = transform.position.z;
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        smoothedPosition.z = initialZ;  // Ensure Z remains the same
        transform.position = smoothedPosition;

        // Zoom effect: TODO
    }
}
