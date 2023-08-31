using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float minZoom, maxZoom;

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Optionally, for the zoom effect:
        float desiredZoom = Mathf.Clamp(target.localScale.x, minZoom, maxZoom); // Set minZoom and maxZoom based on your preference
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, desiredZoom, Time.deltaTime * smoothSpeed);
    }
}
