using UnityEngine;
using System.Collections.Generic;

public class SupplyChainManager : MonoBehaviour
{
    public static SupplyChainManager Instance { get; private set; }

    [SerializeField] List<ConveyorBelt> conveyorBelts = new List<ConveyorBelt>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persistent across scenes if necessary
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterConveyorBelt(ConveyorBelt belt)
    {
        conveyorBelts.Add(belt);
    }

    public void DeregisterConveyorBelt(ConveyorBelt belt)
    {
        conveyorBelts.Remove(belt);
    }

    // Additional methods for managing belts and resource flow...
}
