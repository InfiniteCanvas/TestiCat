using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    [Range(0.01f, 1f)] public float DragSpeedScaling;
    [SerializeField] private Camera _cam;
    private Transform _camTransform;
    private Vector3 _dragCamStart, _dragDelta, _dragMouseScreenStart, _camLeft, _camForward;
    private bool _dragging;

    private void Start()
    {
        _dragging = false;
        if (_cam == null)
            _cam = GetComponent<Camera>();
        _camTransform = transform;
        DragSpeedScaling = Mathf.Clamp(DragSpeedScaling, 0.01f, 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            StartDragging();

        if (Input.GetKeyUp(KeyCode.Mouse0))
            StopDragging();

        if (Input.GetKey(KeyCode.Mouse1))
            Rotate();

        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0)
            Zoom();

        if (_dragging)
            DragCamera();
    }

    #region Implementation

    private void StartDragging()
    {
        _dragMouseScreenStart = Input.mousePosition;
        _dragCamStart = _camTransform.position;
        CalculateAndSetLeftAndForward();
        _dragging = true;
    }

    private void StopDragging()
    {
        _dragging = false;
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
        _camTransform.eulerAngles = new Vector3(_camTransform.eulerAngles.x - Input.GetAxisRaw("Mouse Y"), _camTransform.eulerAngles.y + Input.GetAxisRaw("Mouse X"), 0);
    }

    private void Zoom()
    {
        _cam.orthographicSize += Input.GetAxisRaw("Mouse ScrollWheel") * -10;
        _cam.orthographicSize = Mathf.Clamp(_cam.orthographicSize, 1, 20);
    }

    #endregion
}