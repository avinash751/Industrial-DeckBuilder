using UnityEngine;
using UnityEngine.InputSystem;
using IndustrialBuilder.Input;

public class DragManager : MonoBehaviour
{
    public static DragManager Instance { get; private set; }
    CardControlsInput cardControlsInput;

    private Camera mainCamera;
    public  CardDraggable currentDraggedCard;
    private Vector2 currentMousePosition;

    private void Awake()
    {
        Instance = this;
        mainCamera = Camera.main;
        cardControlsInput = new CardControlsInput();

        cardControlsInput.CardControls.Enable();
        cardControlsInput.CardControls.LeftClick.started+= OnLeftClickStarted;
        cardControlsInput.CardControls.LeftClick.canceled += OnLeftClickReleased;
    }

    private void OnDestroy()
    {
        cardControlsInput.CardControls.Disable();
        cardControlsInput.CardControls.LeftClick.started -= OnLeftClickStarted;
        cardControlsInput.CardControls.LeftClick.canceled -= OnLeftClickReleased;
    }

    private void Update()
    {
        currentMousePosition = cardControlsInput.CardControls.MousePosition.ReadValue<Vector2>();

        if (currentDraggedCard != null)
            currentDraggedCard.UpdateDrag(mainCamera.ScreenToWorldPoint(currentMousePosition));
    }

    private void OnLeftClickStarted(InputAction.CallbackContext context)
    {
        Vector2 worldPos = mainCamera.ScreenToWorldPoint(currentMousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider.TryGetComponent(out CardDraggable card))
        {
            currentDraggedCard = card;
            card.StartDrag();
        }
    }

    private void OnLeftClickReleased(InputAction.CallbackContext context)
    {
        if (currentDraggedCard != null)
        {
            currentDraggedCard.EndDrag();
            currentDraggedCard = null;
        }
    }
}