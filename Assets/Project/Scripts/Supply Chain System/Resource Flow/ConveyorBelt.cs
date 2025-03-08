using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private DragableCoveryorPoint editablePointPrefab;
    [SerializeField] private LineRenderer line;
    [SerializeField] private List<Vector3> pathPoints = new List<Vector3>();
    [SerializeField] private List<DragableCoveryorPoint> editablePointsList;
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
            ConstructEditablePoints();
        }
        _startConnector.Connect(this, editablePointsList.First());
        _endConnector.Connect(this, editablePointsList.Last());
    }

    void ConstructEditablePoints()
    {
        for (int i = 0; i < pathPoints.Count; i++)
        {
            DragableCoveryorPoint newEditablePoint = Instantiate(editablePointPrefab, pathPoints[i], Quaternion.identity, transform);
            newEditablePoint.InitializeEditablePoint(line, pathPoints, i);
            editablePointsList.Add(newEditablePoint);
        }
    }
    public List<Vector3> GetPathPoints()
    {
        return pathPoints;
    }
}
