using System;
using System.Collections.Generic;
using UnityEngine;

public class DragableCoveryorPoint : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] private List<Vector3> pathPoints = new List<Vector3>();
    int editableIndex;

    public void InitializeEditablePoint(LineRenderer _lineRenderer,List<Vector3>_pathPoints,int index)
    {
        lineRenderer = _lineRenderer;
        editableIndex = index;
        pathPoints = _pathPoints;
    }

    private void Update()
    {
        if (lineRenderer == null) return;
        lineRenderer.SetPosition(editableIndex,transform.position);
        Vector3[] newPositionArray = pathPoints.ToArray();
        lineRenderer.GetPositions(newPositionArray);
        pathPoints.Clear();
        pathPoints.AddRange(newPositionArray);
    }
}
