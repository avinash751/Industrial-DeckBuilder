using UnityEngine;
using System;

public class Connector : MonoBehaviour
{
    public bool isInput;
    public ConveyorBelt conveyor;
    DragableCoveryorPoint editablePoint;

    public static event Action<Connector> OnConnectorClicked;
    public static event Action<Connector,ConveyorBelt,DragableCoveryorPoint> OnConnectorDisconnect;

    private void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    void OnMouseDown()
    {
        if (IsConnected())
        {
            OnConnectorDisconnect?.Invoke(this,conveyor,editablePoint);
            return;
        }

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
    public void Connect(ConveyorBelt belt, DragableCoveryorPoint _editablePoint)
    {
        conveyor = belt;
        editablePoint = _editablePoint;
        editablePoint.EnableConveyorEditMode(false,this);
        conveyor.Connected = true;
    }
    public void Disconnect()
    {
        conveyor.Connected = false;
        editablePoint.EnableConveyorEditMode(true,this);
        conveyor.Connected = false;
        conveyor = null;
        editablePoint = null;      
    }
}
