using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    [SerializeField]private List<Vector3> pathPoints = new List<Vector3>();
    [SerializeField] private Connector startConnector;
    [SerializeField] private Connector endConnector;

    private void Start()
    {
        if (line == null)
            line = GetComponent<LineRenderer>();
        // You can define pathPoints manually in the editor or via code.
    }

    private void Update()
    {
        if (startConnector && endConnector)
        {
            line.SetPosition(0, startConnector.transform.position);
            line.SetPosition(line.positionCount - 1, endConnector.transform.position);
            Vector3[] positionsArray = pathPoints.ToArray();
            line.GetPositions(positionsArray);
            pathPoints.Clear();
            pathPoints.AddRange(positionsArray);
        }
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
        startConnector.Connect(this);
        endConnector.Connect(this);
    }

    // Provides the path for moving resources.
    public List<Vector3> GetPathPoints()
    {
        return pathPoints;
    }
}
