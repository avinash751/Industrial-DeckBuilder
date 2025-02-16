using EditorAttributes;
using UnityEngine;

public class TopDownCameraController : MonoBehaviour
{
    [Header("Base Movement Settings")]
    [Tooltip("Damping factor for smooth camera movement. Higher values mean less smoothing (0 for no smoothing). Range: 0-10")]
    [Range(0f, 10f)]
    [SerializeField] private float moveDamping = 0.2f;

    [Line("#327ba8")]
    [FoldoutGroup("Keyboard Movement", drawInBox: true, nameof(enableKeyboardMovement), nameof(keyboardMoveSpeed))]
    [SerializeField] bool enableKeyboardMovement = true;
    [Tooltip("Speed of camera movement with WASD keys.")]
    [ShowField(nameof(enableKeyboardMovement))]
    [SerializeField, HideInInspector] private float keyboardMoveSpeed = 5f;

    [Line("#327ba8")]
    [FoldoutGroup("Mouse Edge Scrolling", drawInBox: true, nameof(enableMouseEdgeScrolling), nameof(edgeScrollZonePercentage), nameof(edgeMoveSpeed))]
    [SerializeField] bool enableMouseEdgeScrolling = true;
    [Tooltip("Percentage of screen edge to activate edge scrolling (0-1). Range: 0-0.5")]
    [ShowField(nameof(enableMouseEdgeScrolling))]
    [SerializeField, HideInInspector] private float edgeScrollZonePercentage = 0.05f;
    [Tooltip("Speed of camera movement when mouse is at screen edges.")]
    [ShowField(nameof(enableMouseEdgeScrolling))]
    [SerializeField, HideInInspector] private float edgeMoveSpeed = 3f;

    [Line("#327ba8")]
    [FoldoutGroup("Mouse Drag Movement", drawInBox: true, nameof(enableMouseDragging), nameof(mouseDragInput), nameof(mouseDragSensitivity))]
    [SerializeField] bool enableMouseDragging = true;
    [Tooltip("Input button for mouse drag (0: Left, 1: Right, 2: Middle). Range: 0-2")]
    [ShowField(nameof(enableMouseDragging))]
    [SerializeField, HideInInspector] int mouseDragInput = 0;
    [Tooltip("Sensitivity of mouse drag movement (adjust for drag speed). Range: 0-1")]
    [ShowField(nameof(enableMouseDragging))]
    [SerializeField, HideInInspector] float mouseDragSensitivity = 0.1f;

    [Line("#327ba8")]
    [FoldoutGroup("Camera Zoom", drawInBox: true, nameof(enableCameraZooming), nameof(zoomSpeed), nameof(zoomDamping), nameof(minZoom), nameof(maxZoom))]
    [SerializeField] bool enableCameraZooming = true;
    [Tooltip("Speed of zooming with mouse wheel.")]
    [ShowField(nameof(enableCameraZooming))]
    [SerializeField, HideInInspector] private float zoomSpeed = 10f;
    [Tooltip("Damping factor for smooth zooming. Higher values mean less smoothing (0 for no smoothing). Range: 0-10")]
    [ShowField(nameof(enableCameraZooming))]
    [SerializeField, HideInInspector] private float zoomDamping = 0.1f;
    [Tooltip("Minimum allowed orthographic size (zoom level).")]
    [ShowField(nameof(enableCameraZooming))]
    [SerializeField, HideInInspector] private float minZoom = 1f;
    [Tooltip("Maximum allowed orthographic size (zoom level).")]
    [ShowField(nameof(enableCameraZooming))]
    [SerializeField, HideInInspector] private float maxZoom = 20f;

    [Line("#327ba8")]
    [Title("<b>Movement Clamping Settings</b>",12,13,false,0,TextAnchor.MiddleLeft)]
    [Tooltip("Minimum X position the camera can reach in world units.")]
    [HorizontalGroup(true, nameof(minXClamp), nameof(maxXClamp))]
    [SerializeField]private float minXClamp = -10f;
    [Tooltip("Maximum X position the camera can reach in world units.")]
    [SerializeField,HideInInspector]private float maxXClamp = 10f;
    [HorizontalGroup(true, nameof(minYClamp), nameof(maxYClamp))]
    [Tooltip("Minimum Y position the camera can reach in world units.")]
    [SerializeField] private float minYClamp = -10f;
    [Tooltip("Maximum Y position the camera can reach in world units.")]
    [SerializeField,HideInInspector] private float maxYClamp = 10f;
    [Line("#327ba8")]

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
        if (!enableKeyboardMovement) return;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;
        Vector3 newTargetPosition = targetPosition + moveDirection * keyboardMoveSpeed *Time.deltaTime;
        targetPosition = ClampPosition(newTargetPosition);
    }

    void HandleEdgeScrolling()
    {
        if (!enableMouseEdgeScrolling) return;

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
        if (!enableMouseDragging) return;
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
        if (!enableCameraZooming) return;
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