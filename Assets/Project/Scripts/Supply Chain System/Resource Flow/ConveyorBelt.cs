using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private DragableCoveryorPoint editablePointPrefab;
    [SerializeField] private DragableConnectorPoint editableConnectorPointPrefab;
    [SerializeField] private LineRenderer line;
    [SerializeField] private List<Vector3> pathPoints = new List<Vector3>();
    [SerializeField] private List<DragableCoveryorPoint> editableConveyorPointsList;
    private DragableConnectorPoint startEditableConnectorPoint;
    private DragableConnectorPoint endEditableConnectorPoint;

    public bool Connected = false;
    private void Start()
    {
        line ??= GetComponent<LineRenderer>();
    }


    // Initialize the belt with a given path, line width, and connectors.
    public void InitializeConnection(List<Vector3> path, float lineWidth, Connector _startConnector, Connector _endConnector)
    {
        pathPoints = new List<Vector3>(path);
        line.positionCount = pathPoints.Count;
        line.SetPositions(pathPoints.ToArray());
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        if (editablePointPrefab != null)
        {
            ConstructConveyorEditablePoints();
        }
        if (editableConnectorPointPrefab != null)
        {
            ConstructEditableConnectorPoints();
        }

        if (startEditableConnectorPoint != null)
        {
            _startConnector.Connect(this, startEditableConnectorPoint);
        }
        if (endEditableConnectorPoint != null)
        {
            _endConnector.Connect(this, endEditableConnectorPoint);
        }
    }

    void ConstructConveyorEditablePoints()
    {
        for (int i = 0; i < pathPoints.Count; i++)
        {
            if (i == 0 || i == pathPoints.Count - 1) continue;
            DragableCoveryorPoint newEditablePoint = Instantiate(editablePointPrefab, pathPoints[i], Quaternion.identity, transform);
            newEditablePoint.InitializeEditablePoint(this,line, pathPoints, i);
            editableConveyorPointsList.Add(newEditablePoint);
        }
    }

    void ConstructEditableConnectorPoints()
    {
        Vector3 firstPathPointPosition = pathPoints.First();
        startEditableConnectorPoint = Instantiate(editableConnectorPointPrefab, firstPathPointPosition, Quaternion.identity, transform);
        startEditableConnectorPoint.InitializeEditablePoint(this,line, pathPoints, 0);
        Vector3 lastPathPointPosition = pathPoints.Last();
        endEditableConnectorPoint = Instantiate(editableConnectorPointPrefab, lastPathPointPosition, Quaternion.identity, transform);
        endEditableConnectorPoint.InitializeEditablePoint(this,line, pathPoints, pathPoints.Count - 1);
    }
    public List<Vector3> GetPathPoints()
    {
        return pathPoints;
    }
}
