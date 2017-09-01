using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour {

    [Range(0.01f, 1f)]public float DragSpeedScaling;
    [SerializeField] private Camera _cam;
    private Transform _camTransform;
    private Vector3 _dragCamStart, _dragDelta, _dragMouseScreenStart, _camLeft, _camForward;
    private bool _dragging;

    private void Start()
    {
        _dragging = false;
        _camTransform = (_cam??GetComponent<Camera>()).transform;
        DragSpeedScaling = Mathf.Clamp(DragSpeedScaling, 0.01f, 1);
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            StartDragging();

        if (Input.GetKeyUp(KeyCode.Mouse0))
            _dragging = false;

        if (Input.GetKey(KeyCode.Mouse1))
            Rotate();

        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0)
            Zoom();

        if (_dragging)
            DragCamera();
    }

    private void StartDragging()
    {
        _dragMouseScreenStart = Input.mousePosition;
        _dragCamStart = _camTransform.position;
        CalculateAndSetLeftAndForward();
        _dragging = true;
    }

    private void CalculateAndSetLeftAndForward()
    {
        _camLeft = Quaternion.Euler(0, _camTransform.eulerAngles.y, 0) * Vector3.left;
        _camForward = Quaternion.Euler(0, _camTransform.eulerAngles.y, 0) * Vector3.forward;
    }

    private void DragCamera()
    {
        _dragDelta = (_dragMouseScreenStart - Input.mousePosition) * DragSpeedScaling;
        _camTransform.position = _dragCamStart + _camForward * _dragDelta.y - _camLeft * _dragDelta.x;
    }

    private void Rotate()
    {
        _camTransform.eulerAngles = new Vector3(45, Input.GetAxisRaw("Mouse X") + _camTransform.eulerAngles.y, 0);
    }

    private void Zoom()
    {
        _cam.orthographicSize += Input.GetAxisRaw("Mouse ScrollWheel")*-10;
        _cam.orthographicSize = Mathf.Clamp(_cam.orthographicSize, 1, 20);
    }
}
