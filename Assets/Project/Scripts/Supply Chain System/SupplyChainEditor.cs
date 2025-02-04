using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SupplyChainEditor : MonoBehaviour
{
    private enum State { Idle, Editing }

    [Header("References")]
    [SerializeField] private GameObject conveyorBeltPrefab;
    [SerializeField] private LineRenderer previewLine;

    [Header("Settings")]
    [SerializeField] private Color validColor = Color.white;
    [SerializeField] private Color invalidColor = Color.red;
    [SerializeField] private float lineWidth = 0.1f;

    [Header("Debug")]
    [SerializeField] private State currentState = State.Idle;
    [SerializeField] private List<Vector3> waypoints = new List<Vector3>();
    // Finalized segment colliders from completed conveyors
    [SerializeField] private List<EdgeCollider2D> segmentColliders = new List<EdgeCollider2D>();

    // Preview segment colliders from segments placed during edit mode
    private List<EdgeCollider2D> previewSegmentColliders = new List<EdgeCollider2D>();

    private Connector startConnector;
    private Stack<ICommand> commandHistory = new Stack<ICommand>();
    private Stack<ICommand> redoStack = new Stack<ICommand>();

    void Start()
    {
        Connector.OnConnectorClicked += OnConnectorClicked;
        if (previewLine != null)
        {
            previewLine.startWidth = lineWidth;
            previewLine.endWidth = lineWidth;
        }
    }

    void OnDestroy()
    {
        Connector.OnConnectorClicked -= OnConnectorClicked;
    }

    void Update()
    {
        if (currentState == State.Editing)
        {
            UpdatePreviewLine();

            // Handle Undo (Ctrl+Z) and Redo (Ctrl+Y)
            if (Input.GetKeyDown(KeyCode.Z) && commandHistory.Count > 0)
            {
                UndoLastCommand();
            }
            else if (Input.GetKeyDown(KeyCode.Y) && redoStack.Count > 0)
            {
                RedoLastCommand();
            }
        }
    }

    void OnConnectorClicked(Connector connector)
    {
        if (currentState == State.Idle && !connector.IsConnected())
        {
            StartEditMode(connector);
        }
        else if (currentState == State.Editing && connector != startConnector && !connector.IsConnected())
        {
            AttemptToCompleteLine(connector);
        }
    }

    void StartEditMode(Connector connector)
    {
        currentState = State.Editing;
        waypoints.Clear();
        waypoints.Add(connector.transform.position);
        waypoints.Add(Vector3.zero); // Placeholder for current mouse position
        previewLine.enabled = true;
        startConnector = connector;
        commandHistory.Clear();
        redoStack.Clear();
    }

    void UpdatePreviewLine()
    {
        Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        waypoints[waypoints.Count - 1] = currentMousePosition;

        previewLine.positionCount = waypoints.Count;
        previewLine.SetPositions(waypoints.ToArray());

        bool intersects = CheckForIntersections(currentMousePosition);

        previewLine.startColor = intersects ? invalidColor : validColor;
        previewLine.endColor = intersects ? invalidColor : validColor;

        if (Input.GetMouseButtonDown(0) && waypoints.Count >= 2 && !intersects)
        {
            ICommand placeCommand = new PlacePreviewConveyorSegment(this, currentMousePosition);
            commandHistory.Push(placeCommand);
            redoStack.Clear();
            placeCommand.Execute();
        }
    }

    public void AddWaypointSegment(Vector3 point)
    {
        if (waypoints.Count > 0)
        {
            waypoints[waypoints.Count - 1] = point;
        }
        waypoints.Add(Vector3.zero); // Add placeholder for next point

        // Add collider for the new segment
        AddPreviewSegmentCollider(waypoints[waypoints.Count - 3], waypoints[waypoints.Count - 2]);
    }

    public void RemoveLastWaypointSegment()
    {
        if (waypoints.Count > 2)
        {
            waypoints.RemoveAt(waypoints.Count - 2);

            // Remove the last preview collider
            RemoveLastPreviewCollider();
        }
    }

    void AttemptToCompleteLine(Connector connector)
    {
        Vector3 endPosition = connector.transform.position;
        waypoints[waypoints.Count - 1] = endPosition;

        bool intersects = CheckForIntersections(endPosition);

        if (!intersects)
        {
            // Add collider for the final segment
            AddPreviewSegmentCollider(waypoints[waypoints.Count - 2], waypoints[waypoints.Count - 1]);

            FinalizeLine(connector);
        }
        else
        {
            Debug.Log("Cannot connect to this connector. Path intersects existing segments.");
        }
    }

    void FinalizeLine(Connector endConnector)
    {
        // Clean up preview colliders before finalizing
        foreach (var previewCollider in previewSegmentColliders)
        {
            if (previewCollider != null)
            {
                // Move the collider to the finalized colliders list
                segmentColliders.Add(previewCollider);
            }
        }
        previewSegmentColliders.Clear();

        // Instantiate the conveyor belt prefab (if needed)
        GameObject conveyorObject = Instantiate(conveyorBeltPrefab);
        ConveyorBelt conveyorBelt = conveyorObject.GetComponent<ConveyorBelt>();
        conveyorBelt.Initialize(waypoints, lineWidth, startConnector, endConnector);

        // Set the conveyorObject as a child of the SupplyChainEditor (optional)
        conveyorObject.transform.parent = this.transform;

        currentState = State.Idle;
        previewLine.enabled = false;
        waypoints.Clear();
        commandHistory.Clear();
        redoStack.Clear();
        startConnector = null;
    }

    void UndoLastCommand()
    {
        if (commandHistory.Count > 0)
        {
            ICommand lastCommand = commandHistory.Pop();
            lastCommand.Undo();
            redoStack.Push(lastCommand);
        }
    }

    void RedoLastCommand()
    {
        if (redoStack.Count > 0)
        {
            ICommand nextCommand = redoStack.Pop();
            nextCommand.Execute();
            commandHistory.Push(nextCommand);
        }
    }

    bool CheckForIntersections(Vector2 newPoint)
    {
        if (waypoints.Count < 2)
            return false;

        Vector2 startPoint = waypoints[waypoints.Count - 2];
        Vector2 endPoint = newPoint;

        // Create a temporary collider for the new segment
        GameObject tempObj = new GameObject("TempSegment");
        EdgeCollider2D tempCollider = tempObj.AddComponent<EdgeCollider2D>();
        tempCollider.edgeRadius = lineWidth / 2.5f;
        tempCollider.isTrigger = true; // Set as trigger to prevent physics interactions

        // Apply trimming to the collider
        Vector2 direction = (endPoint - startPoint).normalized;
        float segmentLength = Vector2.Distance(startPoint, endPoint);
        float trimAmount = segmentLength * 0.1f; // Trim 12.5% from each end

        // Ensure we don't over-trim
        trimAmount = Mathf.Min(trimAmount, segmentLength / 2f);

        Vector2 trimmedStart = startPoint + direction * trimAmount;
        Vector2 trimmedEnd = endPoint - direction * trimAmount;

        tempCollider.points = new Vector2[] { trimmedStart, trimmedEnd };

        // Check for collisions with existing colliders
        bool intersects = false;

        // Prepare the contact filter
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = true;

        Collider2D[] results = new Collider2D[10];

        int collisionCount = tempCollider.Overlap(contactFilter, results);

        if (collisionCount > 0)
        {
            foreach (var collider in results)
            {
                if (collider != null && (segmentColliders.Contains(collider) || previewSegmentColliders.Contains(collider)))
                {
                    intersects = true;
                    break;
                }
            }
        }

        // Clean up temporary object
        Destroy(tempObj);

        return intersects;
    }

    void AddPreviewSegmentCollider(Vector2 startPoint, Vector2 endPoint)
    {
        GameObject colliderObj = new GameObject("PreviewSegmentCollider");
        colliderObj.transform.parent = this.transform;
        EdgeCollider2D edgeCollider = colliderObj.AddComponent<EdgeCollider2D>();
        edgeCollider.edgeRadius = lineWidth / 2.5f;
        edgeCollider.isTrigger = true; // Set as trigger to prevent physics interactions

        // Apply trimming to the collider
        Vector2 direction = (endPoint - startPoint).normalized;
        float segmentLength = Vector2.Distance(startPoint, endPoint);
        float trimAmount = segmentLength * 0.1f; // Trim 12.5% from each end

        // Ensure we don't over-trim
        trimAmount = Mathf.Min(trimAmount, segmentLength / 2f);

        Vector2 trimmedStart = startPoint + direction * trimAmount;
        Vector2 trimmedEnd = endPoint - direction * trimAmount;

        edgeCollider.points = new Vector2[] { trimmedStart, trimmedEnd };

        previewSegmentColliders.Add(edgeCollider);
    }

    void RemoveLastPreviewCollider()
    {
        if (previewSegmentColliders.Count > 0)
        {
            EdgeCollider2D lastCollider = previewSegmentColliders[previewSegmentColliders.Count - 1];
            previewSegmentColliders.RemoveAt(previewSegmentColliders.Count - 1);
            if (lastCollider != null)
                Destroy(lastCollider.gameObject);
        }
    }

    public int WaypointCount { get { return waypoints.Count; } }

    public Vector3 GetSecondToLastWaypoint()
    {
        if (waypoints.Count >= 2)
            return waypoints[waypoints.Count - 2];
        return Vector3.zero;
    }

    public Vector3 GetLastWaypoint()
    {
        if (waypoints.Count >= 1)
            return waypoints[waypoints.Count - 1];
        return Vector3.zero;
    }
}
