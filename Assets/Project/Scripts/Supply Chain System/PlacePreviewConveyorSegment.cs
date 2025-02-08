using UnityEngine;

public class PlacePreviewConveyorSegment : ICommand
{
    private SupplyChainEditor editor;
    private Vector3 previousMousePosition;
    private GameObject segmentColliderObject; 
    private EdgeCollider2D segmentCollider;   
    private float colliderWidth;

    public PlacePreviewConveyorSegment(SupplyChainEditor _editor, Vector3 point, float _colliderWidth)
    {
        editor = _editor;
        previousMousePosition = point;
        colliderWidth = _colliderWidth;
    }

    public void Execute()
    {
        CreateSegmentCollider();
        ApplyTrimmingToSegmentCollider();
        editor.AddWaypointSegment(previousMousePosition); // Keep waypoint addition in SupplyChainEditor
    }

    private void CreateSegmentCollider()
    {
        segmentColliderObject = new GameObject("PreviewSegmentCollider_Command");
        segmentColliderObject.transform.parent = editor.transform; // Parent it to the editor (optional, for organization)
        segmentCollider = segmentColliderObject.AddComponent<EdgeCollider2D>();
        segmentCollider.edgeRadius = colliderWidth/ 2.5f; 
        segmentCollider.isTrigger = true;
        editor.previewSegmentColliders.Add(segmentCollider);
    }

    private void ApplyTrimmingToSegmentCollider()
    {
        Vector2 startPoint = editor.GetLastWaypoint();
        Vector2 endPoint = previousMousePosition;
        Vector2 direction = (endPoint - startPoint).normalized;
        float segmentLength = Vector2.Distance(startPoint, endPoint);
        float trimAmount = segmentLength * 0.1f;    // 10% trimming
        trimAmount = Mathf.Min(trimAmount, segmentLength / 2f);
        Vector2 trimmedStart = startPoint + direction * trimAmount;
        Vector2 trimmedEnd = endPoint - direction * trimAmount;
        segmentCollider.points = new Vector2[ ] { trimmedStart, trimmedEnd };
    }

    public void Undo()
    {
        editor.RemoveLastWaypointSegment(); 

        if (segmentColliderObject != null)
        {
            editor.previewSegmentColliders.Remove(segmentCollider);
            Object.Destroy(segmentColliderObject); 
            segmentColliderObject = null; 
            segmentCollider = null;       
        }
    }
}