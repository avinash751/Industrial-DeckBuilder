using UnityEngine;
using IndustrialBuilder.Input;
using System;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public CardControlsInput cardControlsInput;
    private Camera mainCamera;

    public event Action<InputAction.CallbackContext> OnLeftClickStarted = delegate { };
    public event Action<InputAction.CallbackContext> OnLeftClickReleased = delegate { };
    public Action<RaycastHit2D> OnMouseHovering;

    private void Awake()
    {
        Instance = this;
        cardControlsInput = new CardControlsInput();
        cardControlsInput.Enable();
        mainCamera = Camera.main;

        cardControlsInput.CardControls.LeftClick.started += HandleLeftClickStarted;
        cardControlsInput.CardControls.LeftClick.canceled += HandleLeftClickReleased;
    }

    private void OnDestroy()
    {
        cardControlsInput.Disable();
        cardControlsInput.CardControls.LeftClick.started -= HandleLeftClickStarted;
        cardControlsInput.CardControls.LeftClick.canceled -= HandleLeftClickReleased;
    }


    private void Update()
    {
        GetRaycastHit();
    }

    private void HandleLeftClickStarted(InputAction.CallbackContext context)
    {
        OnLeftClickStarted?.Invoke(context);
    }

    private void HandleLeftClickReleased(InputAction.CallbackContext context)
    {
        OnLeftClickReleased?.Invoke(context);
    }


    void GetRaycastHit()
    {
        Vector2 currentMousePosition = cardControlsInput.CardControls.MousePosition.ReadValue<Vector2>();
        Vector2 worldPos = mainCamera.ScreenToWorldPoint(currentMousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        OnMouseHovering?.Invoke(hit);
    }

    public Vector3 GetMousePositionInWorldSpace()
    {
        Vector2 currentMousePosition = cardControlsInput.CardControls.MousePosition.ReadValue<Vector2>();
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(currentMousePosition);
        return worldPos;

    }
}
