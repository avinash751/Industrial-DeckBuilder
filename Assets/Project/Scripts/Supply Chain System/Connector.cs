using UnityEngine;

public class Connector : MonoBehaviour
{
    public bool isInput;
    public ConveyorBelt conveyor;

    public static event System.Action<Connector> OnConnectorClicked;

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnConnectorClicked?.Invoke(this);
        }
    }

    public bool IsConnected() => conveyor != null;

    public void Connect(ConveyorBelt belt) => conveyor = belt;

    public void Disconnect() => conveyor = null;
}
