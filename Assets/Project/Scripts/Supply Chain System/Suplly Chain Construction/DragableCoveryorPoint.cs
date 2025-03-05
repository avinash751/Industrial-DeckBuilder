using System;
using System.Collections.Generic;
using UnityEngine;

public class DragableCoveryorPoint : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] private List<Vector3> pathPoints = new List<Vector3>();
    int editableIndex;
    bool isConveyerEditMode = false;
    Connector associatedConnector;
    public void InitializeEditablePoint(LineRenderer _lineRenderer, List<Vector3> _pathPoints, int index)
    {
        lineRenderer = _lineRenderer;
        editableIndex = index;
        pathPoints = _pathPoints;
    }


    private void Update()
    {
        if (isConveyerEditMode)
        {
            Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = currentMousePosition;
        }
        else if(associatedConnector!=null)
        {
            transform.position = associatedConnector.transform.position;
        }
        UpdateLinePosition();
    }

    public void EnableConveyorEditMode(bool enable,Connector connector)
    {
        isConveyerEditMode = enable;
        GetComponent<DraggableCard>().enabled = enable ? false : true;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = enable;
        associatedConnector = connector;
    }

    void UpdateLinePosition()
    {
        if (lineRenderer == null) return;
        lineRenderer.SetPosition(editableIndex,transform.position);
        Vector3[] newPositionArray = pathPoints.ToArray();
        lineRenderer.GetPositions(newPositionArray);
        pathPoints.Clear();
        pathPoints.AddRange(newPositionArray);
    }


}
