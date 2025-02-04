using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField]LineRenderer line;
    private List<GameObject> resources = new List<GameObject>();
    [SerializeField] Connector startConnector;
    [SerializeField] Connector endConnector;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if(startConnector && endConnector !=null)
        {
            line.SetPosition(0,startConnector.transform.position);
            line.SetPosition(line.positionCount-1,endConnector.transform.position);

        }
    }

    public void Initialize(List<Vector3> path,float lineWidth, Connector _start, Connector _end)
    {

        line.positionCount = path.Count;
        line.SetPositions(path.ToArray());
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        startConnector = _start;
        endConnector = _end;
        startConnector.Connect(this);
        endConnector.Connect(this);
    }

    public void MoveResources()
    {
        // Implement resource movement logic here
    }
}