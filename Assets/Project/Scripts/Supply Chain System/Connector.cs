using UnityEngine;
using System;

public class Connector : MonoBehaviour
{
    public bool isInput;
    public ConveyorBelt conveyor;

    public static event Action<Connector> OnConnectorClicked;

    private void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
            OnConnectorClicked?.Invoke(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Resource res = collision.GetComponent<Resource>();
        if (res != null && isInput)
        {
            // Get the parent card that implements IResourceReceiver.
            IResourceReceiver receiver = transform.root.GetComponent<IResourceReceiver>();
            if (receiver != null)
            {
                receiver.ReceiveResource(res);
            }
        }
    }

    public bool IsConnected() => conveyor != null;
    public void Connect(ConveyorBelt belt, DragableCoveryorPoint startPoint)
    {
        conveyor = belt;
        editablePoint = startPoint;
        editablePoint.moveWithMouse = false;
        editablePoint.gameObject.SetActive(false);
        belt.connected = true;
    }
    public void Disconnect()
    {
        editablePoint.moveWithMouse = true;
        editablePoint.gameObject.SetActive(true);
        conveyor.connected = false;
        conveyor = null;
        editablePoint = null;
    }
}
