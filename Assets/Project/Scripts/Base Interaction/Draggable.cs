using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Draggable : MonoBehaviour, IDraggable
{
    public Action OnDragUpdate { get; set; }
    public Action OnDragEnd { get; set; }
    public Action OnDragStarted { get; set; }

    [SerializeField] protected float moveSmoothness = 10f;
    [SerializeField] Camera mainCamera;

    [SerializeField]string clickSoundKey = "CardClick";

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        OnMouseStartDrag();
        AudioManager.Instance?.PlayAudio(clickSoundKey);
    }

    private void OnMouseDrag()
    {
        UpdateDrag();
    }

    private void OnMouseUp()
    {
        OnMouseEndDrag();
    }

    protected virtual void OnMouseStartDrag()
    {
        // Reset the drag delay timer at the start of a new drag.
        OnDragStarted?.Invoke();
        DragManager.ResetDragDelay();
    }

    protected virtual void OnMouseEndDrag()
    {
        OnDragEnd?.Invoke();
        DragManager.StopDragging();
    }

    protected virtual void UpdateDrag()
    {
        DragManager.StartDragDelay();
        if (!DragManager.IsDragging)
            return;

        Vector3 targetPosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        targetPosition.z = 0;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSmoothness);
        OnDragUpdate?.Invoke();
    }
}