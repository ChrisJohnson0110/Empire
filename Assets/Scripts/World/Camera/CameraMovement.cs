using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float dragSpeed = 2.0f;
    public float zoomSpeed = 5.0f;
    public float minZoom = 5.0f;
    public float maxZoom = 50.0f;
    private Vector3 dragOrigin;
    private bool isDragging = false;

    void Update()
    {
        HandleDrag();
        HandleZoom();
    }

    void HandleDrag()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
        {
            dragOrigin = Input.mousePosition;
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0)) // Left mouse button released
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 difference = Input.mousePosition - dragOrigin;
            Vector3 move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(-difference.x, 0, -difference.y);
            transform.Translate(move * dragSpeed * Time.deltaTime, Space.World);
            dragOrigin = Input.mousePosition;
        }
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 zoomDirection = transform.forward * scroll * zoomSpeed;
        Vector3 newPosition = transform.position + zoomDirection;

        // Clamp zoom level
        newPosition.y = Mathf.Clamp(newPosition.y, minZoom, maxZoom);
        transform.position = newPosition;
    }
}
