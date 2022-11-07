using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;

    public float currentZoom = 4f;

    public float pitch = 0f;

    public float zoomSpeed = 4f;
    public float minZoom = 1f;
    public float maxZoom = 8f;

    void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        //TODO: Figure out how to rotate camera
    }

    void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom;

        transform.LookAt(target.position + Vector3.up * pitch);
    }
}
