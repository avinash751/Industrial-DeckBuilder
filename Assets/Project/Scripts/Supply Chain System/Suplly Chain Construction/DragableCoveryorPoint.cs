using System;
using System.Collections.Generic;
using UnityEngine;

public class DragableCoveryorPoint : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] private List<Vector3> pathPoints = new List<Vector3>();
    int editableIndex;

    public bool moveWithMouse = false;

    public void InitializeEditablePoint(LineRenderer _lineRenderer, List<Vector3> _pathPoints, int index)
    {
        lineRenderer = _lineRenderer;
        editableIndex = index;
        pathPoints = _pathPoints;
    }

    private void Update()
    {
        if (moveWithMouse)
        {
            Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = currentMousePosition;
            updateLinePosition(currentMousePosition);
            GetComponent<DraggableCard>().enabled = false;
        }
        else
        {
            updateLinePosition(transform.position);
            GetComponent<DraggableCard>().enabled = false;
        }
    }

   

    void updateLinePosition(Vector3 position)
    {
        if (lineRenderer == null) return;
        lineRenderer.SetPosition(editableIndex, position);
        Vector3[] newPositionArray = pathPoints.ToArray();
        lineRenderer.GetPositions(newPositionArray);
        pathPoints.Clear();
        pathPoints.AddRange(newPositionArray);
    }


}
