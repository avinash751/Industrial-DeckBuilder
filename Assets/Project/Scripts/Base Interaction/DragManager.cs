using System.Collections;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    // Singleton instance for convenience.
    public static DragManager Instance;

    // Delay (in seconds) before dragging becomes active.
    [SerializeField] private float dragDelay = 0.5f;

    // Indicates if dragging is currently active.
    public static bool IsDragging { get; private set; }

    // A reference to the running coroutine (if any) for the delay.
    private Coroutine dragDelayCoroutine;

    private void Awake()
    {
        // Ensure a single instance.
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Resets the drag delay timer.
    /// </summary>
    public static void ResetDragDelay()
    {
        if (Instance != null && Instance.dragDelayCoroutine != null)
        {
            Instance.StopCoroutine(Instance.dragDelayCoroutine);
            Instance.dragDelayCoroutine = null;
        }
        IsDragging = false;
    }

    /// <summary>
    /// Call this method from OnMouseDrag to check the timer.
    /// If no delay coroutine is running, it will start one.
    /// </summary>
    public static void StartDragDelay()
    {
        if (Instance != null && Instance.dragDelayCoroutine == null)
        {
            Instance.dragDelayCoroutine = Instance.StartCoroutine(Instance.DragDelayCoroutine());
        }
    }

    /// <summary>
    /// Stops dragging by resetting the delay.
    /// </summary>
    public static void StopDragging()
    {
        ResetDragDelay();
    }

    /// <summary>
    /// Coroutine that waits for the defined delay.
    /// Once the delay is over, dragging becomes active.
    /// </summary>
    private IEnumerator DragDelayCoroutine()
    {
        yield return new WaitForSeconds(dragDelay);
        IsDragging = true;
        dragDelayCoroutine = null;
    }
}