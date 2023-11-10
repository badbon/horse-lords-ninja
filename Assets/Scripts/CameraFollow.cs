using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public float zoomSpeed = 0.125f;
    public Vector3 offset;
    public int minPixelsPerUnit, maxPixelsPerUnit;

    private float initialZ;
    public PixelPerfectCamera pixelPerfectCamera;

    private void Start()
    {
        initialZ = transform.position.z;
        pixelPerfectCamera = GetComponent<PixelPerfectCamera>();
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        smoothedPosition.z = initialZ;  // Ensure Z remains the same
        transform.position = smoothedPosition;

        // Pixel Perfect camera zooming controls
        if (pixelPerfectCamera)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            pixelPerfectCamera.assetsPPU += Mathf.RoundToInt(scroll * zoomSpeed);
            pixelPerfectCamera.assetsPPU = Mathf.Clamp(pixelPerfectCamera.assetsPPU, minPixelsPerUnit, maxPixelsPerUnit);
        }
        else
        {
            Debug.LogError("PixelPerfectCamera component not found!");
        }
    }
}
