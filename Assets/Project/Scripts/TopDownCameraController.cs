using UnityEngine;

public class TopDownCameraController : MonoBehaviour
{
    [Header("Base Movement Settings")]
    [Tooltip("Damping factor for smooth camera movement. Higher values mean less smoothing (0 for no smoothing).")]
    [Range(0f, 10f)]
    [SerializeField] private float moveDamping = 0.2f;

    [Header("Keyboard/Mouse Edge Scrolling Settings")]
    [Tooltip("Speed of camera movement with WASD keys.")]
    [SerializeField] private float keyboardMoveSpeed = 5f;
    [Tooltip("Percentage of screen edge to activate edge scrolling (0-1).")]
    [Range(0f, 0.5f)]
    [SerializeField] private float edgeScrollZonePercentage = 0.05f;
    [Tooltip("Speed of camera movement when mouse is at screen edges.")]
    [SerializeField] private float edgeMoveSpeed = 3f;

    [Header("Mouse Drag Movement Settings")]
    [Range(0, 2)]
    [SerializeField] int mouseDragInput;
    [Range(0, 1)]
    [SerializeField] float mouseDragSensitivity;


    [Header("Zoom Settings")]
    [Tooltip("Speed of zooming with mouse wheel.")]
    [SerializeField] private float zoomSpeed = 10f;
    [Tooltip("Damping factor for smooth zooming. Higher values mean less smoothing (0 for no smoothing).")]
    [Range(0f, 10f)]
    [SerializeField] private float zoomDamping = 0.1f;
    [Tooltip("Minimum allowed orthographic size (zoom level).")]
    [SerializeField] private float minZoom = 1f;
    [Tooltip("Maximum allowed orthographic size (zoom level).")]
    [SerializeField] private float maxZoom = 20f;

    [Header("Movement Clamp Settings")]
    [Tooltip("Minimum X position the camera can reach in world units.")]
    [SerializeField] private float minXClamp = -10f;
    [Tooltip("Maximum X position the camera can reach in world units.")]
    [SerializeField] private float maxXClamp = 10f;
    [Tooltip("Minimum Y position the camera can reach in world units.")]
    [SerializeField] private float minYClamp = -10f;
    [Tooltip("Maximum Y position the camera can reach in world units.")]
    [SerializeField] private float maxYClamp = 10f;

    private Camera cam;
    private Vector3 targetPosition;
    private float targetZoom;

    private Vector3 startMouseDragPosition;
    private Vector3 currentMousePosition;

    void Start()
    {
        cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("No Main Camera found in the scene! Make sure you have a Camera tagged as 'MainCamera'.");
            enabled = false;
            return;
        }
        targetPosition = transform.position;
        targetZoom = cam.orthographicSize;
    }

    void Update()
    {
        HandleKeyboardMovement();
        HandleEdgeScrolling();
        HandleMouseDragging();
        HandleZoom();
        ApplyDamping();
    }

    void HandleKeyboardMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;
        Vector3 newTargetPosition = targetPosition + moveDirection * keyboardMoveSpeed *Time.deltaTime;
        targetPosition = ClampPosition(newTargetPosition);
    }

    void HandleEdgeScrolling()
    {
        currentMousePosition = Input.mousePosition;
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float edgeZoneWidth = screenWidth * edgeScrollZonePercentage;
        float edgeZoneHeight = screenHeight * edgeScrollZonePercentage;

        Vector3 edgeMoveDir = Vector3.zero;

        if (currentMousePosition.x < edgeZoneWidth)
        {
            edgeMoveDir += Vector3.left;
        }
        if (currentMousePosition.x > screenWidth - edgeZoneWidth)
        {
            edgeMoveDir += Vector3.right;
        }
        if (currentMousePosition.y < edgeZoneHeight)
        {
            edgeMoveDir += Vector3.down;
        }
        if (currentMousePosition.y > screenHeight - edgeZoneHeight)
        {
            edgeMoveDir += Vector3.up;
        }
        Vector3 newTargetPosition = targetPosition + edgeMoveDir.normalized *edgeMoveSpeed *Time.deltaTime;
        targetPosition = ClampPosition(newTargetPosition);
    }


    void HandleMouseDragging()
    {
        if (Input.GetMouseButtonDown(mouseDragInput))
        {
            startMouseDragPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(mouseDragInput))
        {
            currentMousePosition = Input.mousePosition;

            Vector3 mouseDragDirection = currentMousePosition - startMouseDragPosition;
            Vector3 mouseDragPosition = mouseDragDirection *mouseDragSensitivity;
            Vector3 newTargetPosition = targetPosition + mouseDragPosition * Time.deltaTime;
            targetPosition = ClampPosition(newTargetPosition);
        }
    }

    void HandleZoom()
    {
        float scrollInput = Input.mouseScrollDelta.y;

        if (scrollInput != 0)
        {
            float newTargetZoom = targetZoom - (scrollInput * zoomSpeed * Time.deltaTime * 10f);
            targetZoom = Mathf.Clamp(newTargetZoom, minZoom, maxZoom);
        }
    }

    void ApplyDamping()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveDamping);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomDamping);
    }

    private Vector3 ClampPosition(Vector3 positionToClamp)
    {
        Vector3 clampedPosition = positionToClamp;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minXClamp, maxXClamp);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minYClamp, maxYClamp);
        clampedPosition.z = -10;
        return clampedPosition;
    }
}
