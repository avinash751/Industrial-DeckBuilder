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

    private void Start()
    {
        mainCamera = Camera.main;
    }


    private void OnMouseDown()
    {
        OnMouseStartDrag();
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
        OnDragStarted?.Invoke();
        DragManager.StartDragging();
    }

    protected virtual void OnMouseEndDrag()
    {
        OnDragEnd?.Invoke();
        DragManager.StopDragging();
    }

    protected virtual void  UpdateDrag()
    {

        Vector3 targetPosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        targetPosition.z = 0;
        transform.position = Vector3.Lerp(transform.position, targetPosition,
                             Time.deltaTime * moveSmoothness);
        OnDragUpdate?.Invoke();
    }
}