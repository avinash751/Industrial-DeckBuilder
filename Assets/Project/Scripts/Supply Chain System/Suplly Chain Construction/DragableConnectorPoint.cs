using System;
using System.Collections.Generic;
using UnityEngine;

public class DragableConnectorPoint : DragableCoveryorPoint
{
    bool isConveyerEditMode = false;
    Connector associatedConnector;
    ConveyorBelt associatedConveyor;

    public static Action<DragableConnectorPoint,ConveyorBelt> OnAttemptToEnterConveyorEditMode;


    public override void InitializeEditablePoint(ConveyorBelt conveyor, LineRenderer _lineRenderer, List<Vector3> _pathPoints, int index)
    {
        base.InitializeEditablePoint(conveyor, _lineRenderer, _pathPoints, index);
        associatedConveyor = conveyor;
    }

    private void Update()
    {
        if (isConveyerEditMode)
        {
            Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentMousePosition.z = -0.1f;
            transform.position = currentMousePosition;
        }
        else if (associatedConnector != null)
        {
            Vector3 associatedConnectorPosition = associatedConnector.transform.position;
            associatedConnectorPosition.z = -0.1f;
            transform.position = associatedConnectorPosition;

        }
        UpdateLinePosition();
    }

    private void OnMouseDown()
    {
        if (!isConveyerEditMode && !associatedConveyor.Connected)
        {
            OnAttemptToEnterConveyorEditMode?.Invoke(this, associatedConveyor);
        }
    }

    public void EnableConveyorEditMode(bool enabledEditMode, bool colliderEnabled, bool rendererEnabled, Connector connector)
    {
        isConveyerEditMode = enabledEditMode;
        GetComponent<CircleCollider2D>().enabled = colliderEnabled;
        GetComponent<SpriteRenderer>().enabled = rendererEnabled;
        associatedConnector = connector;
    }
}
