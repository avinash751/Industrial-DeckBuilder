using UnityEngine;
using System;

public class Connector : MonoBehaviour
{
    public bool isInput;
    public ConveyorBelt conveyor;
    DragableConnectorPoint editableConnectorPoint;

    public static event Action<Connector> OnConnectorClicked;
    public static event Action<Connector,ConveyorBelt,DragableConnectorPoint> OnConnectorDisconnect;

    string connectedAudioKey = "CardConnect";
    string disconnectedAudioKey = "CardDisconnect";
    string connectorClickedAudioKey = "ConnectorClick";


    private void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    void OnMouseDown()
    {
        if (IsConnected())
        {
            OnConnectorDisconnect?.Invoke(this,conveyor,editableConnectorPoint);
            return;
        }
        AudioManager.Instance?.PlayAudio(connectorClickedAudioKey);
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
    public void Connect(ConveyorBelt belt, DragableConnectorPoint _editablePoint)
    {
        conveyor = belt;
        editableConnectorPoint = _editablePoint;
        editableConnectorPoint.EnableConveyorEditMode(false,false,false,this);
        conveyor.Connected = true;
        AudioManager.Instance?.PlayAudio(connectedAudioKey);
    }
    public void Disconnect()
    {
        conveyor.Connected = false;
        editableConnectorPoint.EnableConveyorEditMode(true,true,true,this);
        conveyor.Connected = false;
        conveyor = null;
        editableConnectorPoint = null;
        AudioManager.Instance?.PlayAudio(disconnectedAudioKey);
    }
}
