using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;

public class SupplyChainEditor : MonoBehaviour
{
    private enum State { Idle, SupplyChainEdit, ConveyorEdit }

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
    [SerializeField] private List<EdgeCollider2D> finalizedSegmentColliders = new List<EdgeCollider2D>();
    [SerializeField] public List<EdgeCollider2D> previewSegmentColliders = new List<EdgeCollider2D>();
    private CommandManager commandManager = new CommandManager();
    private Connector startConnector;
    private Connector previousConveyorConnector;
    private DragableConnectorPoint currentEditablePoint;
    private ConveyorBelt existingConveyor;
    int mouse0ClickCounter;

    void Start()
    {
        Connector.OnConnectorClicked += HandleAllConnectionTypes;
        Connector.OnConnectorDisconnect += DisconnectExistingConveyorBelt;
        DragableConnectorPoint.OnAttemptToEnterConveyorEditMode += EnterCoveyorEditThroughConnectorPoint;
        if (previewLine != null)
        {
            previewLine.startWidth = lineWidth *0.8f;
            previewLine.endWidth = lineWidth*0.8f;
        }
    }

    void OnDestroy()
    {
        Connector.OnConnectorClicked -= HandleAllConnectionTypes;
        Connector.OnConnectorDisconnect -= DisconnectExistingConveyorBelt;
        DragableConnectorPoint.OnAttemptToEnterConveyorEditMode -= EnterCoveyorEditThroughConnectorPoint;
    }

    void HandleAllConnectionTypes(Connector connector)
    {
        if (currentState == State.ConveyorEdit && !connector.IsConnected())
        {
            currentState = State.Idle;
            connector.Connect(existingConveyor, currentEditablePoint);
            currentEditablePoint = null;
            existingConveyor = null;
            previousConveyorConnector = null;
            return;
        }

        if (currentState == State.Idle && !connector.IsConnected())
        {
            StartSupplyChainEditMode(connector);
        }
        else if (currentState == State.SupplyChainEdit && connector != startConnector && !connector.IsConnected())
        {
            AttemptToCompleteSupplyChainLine(connector);
        }

    }

    void DisconnectExistingConveyorBelt(Connector connector, ConveyorBelt conveyor, DragableConnectorPoint _editablePoint)
    {
        if (currentState == State.SupplyChainEdit)
        {
            return;
        }
        if (currentState == State.Idle && connector.IsConnected())
        {
            currentState = State.ConveyorEdit;
            currentEditablePoint = _editablePoint;
            existingConveyor = conveyor;
            previousConveyorConnector = connector;
            previousConveyorConnector.Disconnect();
            mouse0ClickCounter = 0;
        }

    }

    void EnterCoveyorEditThroughConnectorPoint(DragableConnectorPoint connectorEditPoint, ConveyorBelt conveyor)
    {
        currentState = State.ConveyorEdit;
        currentEditablePoint = connectorEditPoint;
        existingConveyor = conveyor;
        connectorEditPoint.EnableConveyorEditMode(true,false, true, null);
    }



    void StartSupplyChainEditMode(Connector connector)
    {
        currentState = State.SupplyChainEdit;
        waypoints.Clear();
        waypoints.Add(connector.transform.position);
        previewLine.enabled = true;
        startConnector = connector;
        commandManager.ClearCommandHistory();
    }

    void Update()
    {
        HandleInputInEditMode();
    }

    private void HandleInputInEditMode()
    {

        if (currentState == State.ConveyorEdit)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                currentState = State.Idle;
                previousConveyorConnector.Connect(existingConveyor, currentEditablePoint);
                currentEditablePoint = null;
                previousConveyorConnector = null;
                existingConveyor = null;
                return;
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                mouse0ClickCounter++;
                if (mouse0ClickCounter < 2) return;
                currentState = State.Idle;
                currentEditablePoint.EnableConveyorEditMode(false, true, true, null);
                currentEditablePoint = null;
                previousConveyorConnector = null;
                existingConveyor = null;
                mouse0ClickCounter = 0;
                return;
            }
        }


        if (currentState != State.SupplyChainEdit) return;

        UpdatePreviewLine();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            commandManager.UndoCommand();
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            commandManager.RedoCommand();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            DestroyAllPreviewColliders();
            EndEditMode();
        }
    }

    void UpdatePreviewLine()
    {
        Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool intersects = CheckForIntersections(currentMousePosition);
        if (Input.GetMouseButtonDown(0) && waypoints.Count >= 1 && !intersects)
        {
            ICommand placeCommand = new PlacePreviewConveyorSegment(waypoints, this, currentMousePosition, lineWidth);
            commandManager.ExecuteCommand(placeCommand);
        }
        waypoints[waypoints.Count - 1] = currentMousePosition;

        previewLine.positionCount = waypoints.Count;
        previewLine.SetPositions(waypoints.ToArray());

        previewLine.startColor = intersects ? invalidColor : validColor;
        previewLine.endColor = intersects ? invalidColor : validColor;
    }

    void AttemptToCompleteSupplyChainLine(Connector connector)
    {
        Vector3 endPosition = connector.transform.position;
        waypoints[waypoints.Count - 1] = endPosition;
        bool intersects = CheckForIntersections(endPosition);

        if (!intersects)
        {
            FinalizeLine(connector);
        }
        else
        {
            Debug.Log("Cannot connect to this connector. Path intersects existing segments.");
        }
    }

    bool CheckForIntersections(Vector2 newPoint)
    {
        if (waypoints.Count < 2)
            return false;

        Vector2 startPoint = waypoints[waypoints.Count - 2];
        Vector2 endPoint = newPoint;

        // Create a temporary collider for the new segment
        GameObject tempColliderObj = new GameObject("TempSegment");
        EdgeCollider2D tempCollider = tempColliderObj.AddComponent<EdgeCollider2D>();
        tempCollider.edgeRadius = lineWidth / 2.5f;
        tempCollider.isTrigger = true;

        // Apply trimming to the collider
        Vector2 direction = (endPoint - startPoint).normalized;
        float segmentLength = Vector2.Distance(startPoint, endPoint);
        float trimAmount = segmentLength * 0.1f; // Trim 10% from each end 
        trimAmount = Mathf.Min(trimAmount, segmentLength / 2f);

        Vector2 trimmedStart = startPoint + direction * trimAmount;
        Vector2 trimmedEnd = endPoint - direction * trimAmount;
        tempCollider.points = new Vector2[] { trimmedStart, trimmedEnd };

        bool intersects = false;
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = true;
        Collider2D[] results = new Collider2D[10];
        int collisionCount = tempCollider.Overlap(contactFilter, results);

        if (collisionCount > 0)
        {
            foreach (var collider in results)
            {
                if (collider != null && finalizedSegmentColliders.Contains(collider) || previewSegmentColliders.Contains(collider))
                {
                    intersects = true;
                    break;
                }
            }
        }

        // Clean up temporary object
        Destroy(tempColliderObj);
        return intersects;
    }

    void FinalizeLine(Connector endConnector)
    {
        GameObject conveyorObject = Instantiate(conveyorBeltPrefab);
        ConveyorBelt conveyorBelt = conveyorObject.GetComponent<ConveyorBelt>();
        conveyorBelt.InitializeConnection(waypoints, lineWidth, startConnector, endConnector);
        conveyorObject.transform.parent = transform;
        EndEditMode();
    }

    void EndEditMode()
    {
        currentState = State.Idle;
        previewLine.enabled = false;
        waypoints.Clear();
        commandManager.ClearCommandHistory();
        startConnector = null;
    }

    public void DestroyAllPreviewColliders()
    {
        while (commandManager.GetUndoCommandsCount() > 0)
        {
            commandManager.UndoCommand();
        }
    }
}