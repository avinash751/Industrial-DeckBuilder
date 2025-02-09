using System.Collections.Generic;
using UnityEngine;

public class PlacePreviewConveyorSegment : ICommand
{
    private SupplyChainEditor editor;
    List<Vector3> waypoints = new List<Vector3>();
    private Vector3 previousMousePosition;
    private GameObject segmentColliderObject;
    private EdgeCollider2D segmentCollider;
    private float colliderWidth;

    public PlacePreviewConveyorSegment(List<Vector3> _waypoints, SupplyChainEditor _editor, Vector3 point, float _colliderWidth)
    {
        waypoints = _waypoints;
        editor = _editor;
        previousMousePosition = point;
        colliderWidth = _colliderWidth;
    }

    public void Execute()
    {
        if (waypoints.Count >1)
        {
            CreateSegmentCollider();
            ApplyTrimmingToSegmentCollider();
        }
        AddWaypointSegment(previousMousePosition);
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
        Vector2 startPoint = GetLastWaypoint();
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
        RemoveLastWaypointSegment();

        if (segmentColliderObject != null)
        {
            editor.previewSegmentColliders.Remove(segmentCollider);
            Object.Destroy(segmentColliderObject);
            segmentColliderObject = null;
            segmentCollider = null;
        }
    }


    public void AddWaypointSegment(Vector3 point)
    {
        if (waypoints.Count > 0)
        {
            waypoints[waypoints.Count - 1] = point;
        }
        waypoints.Add(Vector3.zero); // Add placeholder for next point
    }

     void RemoveLastWaypointSegment()
    {
        if (waypoints.Count > 2)
        {
            waypoints.RemoveAt(waypoints.Count - 2);
        }
    }

     Vector3 GetLastWaypoint()
    {
        if (waypoints.Count > 1)
        {
            return waypoints[waypoints.Count - 2]; // Return the second to last waypoint
        }
        return Vector3.zero;
    }
}