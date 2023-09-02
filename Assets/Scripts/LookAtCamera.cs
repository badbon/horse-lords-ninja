using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Camera mainCamera;
    public Transform targetPosition;
    public Vector3 offset;
    public bool followTarget = false;

    private void Start()
    {
        mainCamera = Camera.main;
        if (targetPosition != null)
        {
            transform.position = targetPosition.position + offset;
        }
    }

    private void Update()
    {
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                        mainCamera.transform.rotation * Vector3.up);

        if (followTarget)
        {
            transform.position = targetPosition.position + offset;
        }
    }
}
