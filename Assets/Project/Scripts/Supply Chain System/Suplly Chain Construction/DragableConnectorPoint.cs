using System.Collections.Generic;
using UnityEngine;

public class DragableConnectorPoint : DragableCoveryorPoint
{ 
    bool isConveyerEditMode = false;
    Connector associatedConnector;
    ConveyorBelt associatedConveyor;

    public override void InitializeEditablePoint(ConveyorBelt conveyor, LineRenderer _lineRenderer, List<Vector3> _pathPoints, int index)
    {
        base.InitializeEditablePoint(conveyor, _lineRenderer, _pathPoints, index);
        associatedConveyor = conveyor;
    }

    private void Update()
    {
        if (isConveyerEditMode)
        {
            Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = currentMousePosition;
        }
        else if (associatedConnector != null)
        {
            transform.position = associatedConnector.transform.position;
        }
        UpdateLinePosition();
    }

    public void EnableConveyorEditMode(bool enable, Connector connector)
    {
        isConveyerEditMode = enable;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = enable;
        associatedConnector = connector;
    }
}
