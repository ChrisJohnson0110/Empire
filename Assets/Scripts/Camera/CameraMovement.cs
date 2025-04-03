using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    //camera settigns
    [SerializeField] private float _dragSpeed = 2.0f;
    [SerializeField] private float _zoomSpeed = 5.0f;
    [SerializeField] private float _minZoom = 3.0f;
    [SerializeField] private float _maxZoom = 50.0f;

    //camera info
    private Vector3 _dragOrigin;
    private bool _isDragging = false;

    void Update()
    {
        HandleDrag();
        HandleZoom();
    }

    //mouse drag camera movement
    private void HandleDrag()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
            {
                _dragOrigin = Input.mousePosition;
                _isDragging = true;

            }
        }
        if (Input.GetMouseButtonUp(0)) // Left mouse button released
        {
            _isDragging = false;
        }

        if (_isDragging)
        {
            Vector3 difference = Input.mousePosition - _dragOrigin;
            Vector3 move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(-difference.x, 0, -difference.y);
            //transform.Translate(move * dragSpeed * Time.deltaTime, Space.World);
            transform.position += (move * _dragSpeed * Time.deltaTime);
            _dragOrigin = Input.mousePosition;
        }
    }

    //mouse scroll for zoom
    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 zoomDirection = transform.forward * scroll * _zoomSpeed;
        Vector3 newPosition = transform.position + zoomDirection;

        // Clamp zoom level
        newPosition.y = Mathf.Clamp(newPosition.y, _minZoom, _maxZoom);
        transform.position = newPosition;
    }
}
