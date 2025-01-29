using UnityEngine.InputSystem;
using UnityEngine;
using System;
using CustomInspector;
public class HoverManager : MonoBehaviour
{
    public static HoverManager Instance { get; private set; }

    [SerializeField] private LayerMask cardLayer;
    private Camera mainCamera;
    [ReadOnly] public CardHoverHandler currentHoveredCard;

    // Events for other systems to listen to
    public static event Action<CardHoverHandler> OnHoverStarted;
    public static event Action<CardHoverHandler> OnHoverEnded;

    private void Awake()
    {
        Instance = this;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 50f, cardLayer);

        HandleHoverState(hit);
    }

    private void HandleHoverState(RaycastHit2D hit)
    {
        // Exit if no change
        if (currentHoveredCard != null && hit.collider?.gameObject == currentHoveredCard.gameObject)
            return;

        // End previous hover
        if (currentHoveredCard != null)
        {
            OnHoverEnded?.Invoke(currentHoveredCard);
            currentHoveredCard = null;
        }
        if (DragManager.Instance != null)
        {
            if (DragManager.Instance.currentDraggedCard != null) return;
        }
        // Start new hover
        if (hit.collider != null && hit.collider.TryGetComponent(out CardHoverHandler card))
        {
            currentHoveredCard = card;
            OnHoverStarted?.Invoke(currentHoveredCard);
        }
    }
}