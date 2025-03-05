using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private DragableCoveryorPoint editablePointPrefab;
    [SerializeField] private LineRenderer line;
    [SerializeField] private List<Vector3> pathPoints = new List<Vector3>();
    [SerializeField] private List<DragableCoveryorPoint> editablePointsList;
    [SerializeField] private Connector startConnector;
    [SerializeField] private Connector endConnector;
    public bool connected = false;
    private void Start()
    {
        if (line == null)
            line = GetComponent<LineRenderer>();
        // You can define pathPoints manually in the editor or via code.
    }

 
    // Initialize the belt with a given path, line width, and connectors.
    public void Initialize(List<Vector3> path, float lineWidth, Connector _start, Connector _end)
    {
        pathPoints = new List<Vector3>(path);
        line.positionCount = pathPoints.Count;
        line.SetPositions(pathPoints.ToArray());
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        startConnector = _start;
        endConnector = _end;
        if (editablePointPrefab != null)
        {
            ConstructEditablePoints();
        }
        startConnector.Connect(this,editablePointsList.First());
        endConnector.Connect(this, editablePointsList.Last());
        connected = true;

      
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

    // Provides the path for moving resources.
    public List<Vector3> GetPathPoints()
    {
        return pathPoints;
    }
}
