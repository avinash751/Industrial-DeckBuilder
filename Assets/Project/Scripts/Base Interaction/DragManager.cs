using UnityEngine;

public class DragManager : MonoBehaviour
{
    private void Start()
    {
        IsDragging = false;
    }
    public static bool IsDragging { get; private set; }

    public static void StartDragging()
    {
        IsDragging = true;
    }

    public static void StopDragging()
    {
        IsDragging = false;
    }
}
