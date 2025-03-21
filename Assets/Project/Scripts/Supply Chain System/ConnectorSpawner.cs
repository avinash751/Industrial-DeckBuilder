using CustomInspector;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorSpawner : MonoBehaviour
{
    [HorizontalLine("References", 3, FixedColor.DustyBlue)]
    [SerializeField] Connector inputConnectorPrefab;
    [SerializeField] Connector outputConnectorPrefab;

    [HorizontalLine("Spawn Locations", 3, FixedColor.DustyBlue)]
    [SerializeField] Transform[ ] inputSpawnPointsArray;
    [SerializeField] Transform[ ] outputSpawnPointsArray;

    [HorizontalLine("Debug Info", 3, FixedColor.CherryRed)]
    [ReadOnly][SerializeField] int inputConnectorsRequired = 1;
    [ReadOnly][SerializeField] int outputConnectorsRequired = 0;
    [SerializeField]List<Connector> spawnedInputConnectors = new List<Connector>();
    [SerializeField]List<Connector> spawnedOutputConnectors = new List<Connector>();

    public void InitializeConnectors(CardData cardData)
    {
        if (!CanSpawnConnectors(cardData)) return;

        SpawnExtractionCardConnectors(cardData);
        SpawnProductionCardConnectors(cardData);
    }

    private void SpawnExtractionCardConnectors(CardData cardData)
    {
        if (cardData is ExtractionCardData extractionCardData)
        {
            inputConnectorsRequired =1;
            Vector3 spawnposition = outputSpawnPointsArray[0].position;
            Quaternion spawnRotation = outputSpawnPointsArray[0].rotation;
            spawnposition.z = -0.2f;
            Connector newOutPutConnector = Instantiate(outputConnectorPrefab,spawnposition,spawnRotation, transform);
            spawnedOutputConnectors.Add(newOutPutConnector);
        }
    }

    private void SpawnProductionCardConnectors(CardData cardData)
    {
        if (cardData is ProductionCardData productionCardData)
        {
            inputConnectorsRequired = productionCardData.ResourceInput.rawResources.Length +
                                                    productionCardData.ResourceInput.refinedResources.Length;
            outputConnectorsRequired = productionCardData.ResourceOutput.rawResources.Length +
                                                    productionCardData.ResourceOutput.refinedResources.Length;

            for (int i = 0; i < inputConnectorsRequired; i++)
            {
                Vector3 spawnposition = inputSpawnPointsArray[i].position;
                Quaternion spawnRotation = inputSpawnPointsArray[i].rotation;
                spawnposition.z = -0.2f;
                Connector newInputConnector = Instantiate(inputConnectorPrefab,spawnposition,spawnRotation, transform);

                spawnedInputConnectors.Add(newInputConnector);
            }
            for (int i = 0; i < outputConnectorsRequired; i++)
            {
                Vector3 spawnposition = outputSpawnPointsArray[i].position;
                Quaternion spawnRotation = outputSpawnPointsArray[i].rotation;
                spawnposition.z = -0.2f;
                Connector newOutputConnector = Instantiate(outputConnectorPrefab,spawnposition,spawnRotation, transform);
                spawnedOutputConnectors.Add((newOutputConnector));
            }

        }
    }

    bool CanSpawnConnectors(CardData cardData)
    {
        if (inputConnectorPrefab == null || outputConnectorPrefab == null)
        {
            Debug.LogError("Please assign input and output connector prefabs in Connector Spawner as they are null.");
            return false;
        }

        if (inputSpawnPointsArray.Length is 0 || outputSpawnPointsArray.Length is 0)
        {
            Debug.LogError("Please assign input and output connector spawn points in Connector Spawner as they are empty.");
            return false;
        }

        if (cardData is null)
        {
            Debug.LogError("The given card data is null, therefore cannot spawn connectors.");
            return false;
        }
        return true;
    }

    public List<Connector> GetConnectors(bool getInput)
    {
        if (getInput)
        {
            return spawnedInputConnectors;
        }
        else
        {
            return spawnedOutputConnectors;
        }
    }
}
