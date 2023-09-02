using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LookAtCamera : MonoBehaviour
{
    public Camera mainCamera;
    public Transform targetPosition;
    public Vector3 offset;
    public bool followTarget = false;
    public TMP_Text text;

    private void Start()
    {
        // Make sure we are attached to world canvas
        transform.SetParent(GameObject.Find("WorldSpace").transform);

        mainCamera = Camera.main;
        if (targetPosition != null)
        {
            transform.position = targetPosition.position + offset;
        }

        Invoke("DestroyText", 1.2f);
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

    public void DestroyText()
    {
        Destroy(gameObject);
    }
}
