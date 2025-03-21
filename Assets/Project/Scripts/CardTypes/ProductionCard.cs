using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CustomInspector;

public class ProductionCard : Card, IResourceReceiver
{
    [ReadOnly][SerializeField][Foldout] private ProductionCardData productionCardSO;
    [SerializeField] private List<Connector> inputConnectors = new List<Connector>();
    [SerializeField] private List<Connector> outputConnectors = new List<Connector>();
    // [SerializeField] private CardFeedbacks cardFeedbacks; // Add CardFeedbacks reference - assign in inspector!


    private MultiResource receivedResources = new MultiResource();
    [ReadOnly]public bool isProductionActive = false;

    public override void InitializeCard(CardData data)
    {
        base.InitializeCard(data);
        productionCardSO = (ProductionCardData)data;

        isProductionActive = false;
        receivedResources = new MultiResource();
        inputConnectors = connectorSpawner.GetConnectors(true);
        outputConnectors = connectorSpawner.GetConnectors(false);
    }


    private void Start()
    {
        if (MonthTimer.Instance == null) return;
        MonthTimer.Instance.OnMonthEnd += HandleEndOfMonthPayment; 
    }

    private void OnDisable()
    {
        if (MonthTimer.Instance == null) return;
        MonthTimer.Instance.OnMonthEnd -= HandleEndOfMonthPayment;
    }

    protected override void HandleEndOfMonthPayment()
    {
        if (!AreAllInputConnectorsConnected() || !AreAllOutputConnectorsConnected())
        {
            return;
        }
        PayUpkeepCost();
    }

    public void ReceiveResource(Resource resource)
    {
        if (!IsValidInputResource(resource))
        {
            Debug.Log("Invalid resource received: " + resource.rawType);
            return;
        }

        AddResourceToReceived(resource);
        Destroy(resource.gameObject);

        if (!AreAllRequiredResourcesReceived() || !AreAllInputConnectorsConnected())
        {
            Debug.Log("Waiting for all required resources and input connections...");
            return;
        }

        if (!isProductionActive)
        {
            StartCoroutine(ProductionRoutine());
            isProductionActive = true;
        }
    }

    private bool IsValidInputResource(Resource resource)
    {
        // Check allowed raw resources.
        foreach (RawResource allowedResource in productionCardSO.ResourceInput.rawResources)
        {
            if (!resource.isRefined && resource.rawType == allowedResource)
                return true;
        }
        // Check allowed refined resources.
        foreach (RefinedResource allowed in productionCardSO.ResourceInput.refinedResources)
        {
            if (resource.isRefined && resource.refinedType == allowed)
                return true;
        }
        return false;
    }

    private void AddResourceToReceived(Resource resource)
    {
        if (!resource.isRefined)
        {
            if (receivedResources.rawResources == null)
                receivedResources.rawResources = new RawResource[0];

            if (!ContainsResource(receivedResources.rawResources, resource.rawType))
            {
                List<RawResource> rawList = new List<RawResource>(receivedResources.rawResources);
                rawList.Add(resource.rawType);
                receivedResources.rawResources = rawList.ToArray();
            }
        }
        else
        {
            if (receivedResources.refinedResources == null)
                receivedResources.refinedResources = new RefinedResource[0];

            if (!ContainsResource(receivedResources.refinedResources, resource.refinedType))
            {
                List<RefinedResource> refinedList = new List<RefinedResource>(receivedResources.refinedResources);
                refinedList.Add(resource.refinedType);
                receivedResources.refinedResources = refinedList.ToArray();
            }
        }
    }

    private bool AreAllRequiredResourcesReceived()
    {
        return CompareMultiResources(receivedResources, productionCardSO.ResourceInput);
    }

    private bool CompareMultiResources(MultiResource received, MultiResource required)
    {
        if (required.rawResources != null)
        {
            foreach (RawResource requiredRaw in required.rawResources)
            {
                if (!ContainsResource(received.rawResources, requiredRaw))
                    return false;
            }
        }

        if (required.refinedResources != null)
        {
            foreach (RefinedResource requiredRefined in required.refinedResources)
            {
                if (!ContainsResource(received.refinedResources, requiredRefined))
                    return false;
            }
        }

        return true;
    }

    IEnumerator ProductionRoutine()
    {
        while (AreAllInputConnectorsConnected())
        {
            ProduceResource();
            yield return new WaitForSeconds(1f / productionCardSO.ProductionRate);
        }
        isProductionActive = false;
    }

    void ProduceResource()
    {
        if (outputConnectors == null || outputConnectors.Count == 0 || !AreAllInputConnectorsConnected()) 
            return;

        if (productionCardSO.resourcePrefab == null)
        {
            Debug.LogError("ProductionCardData does not have a resourcePrefab assigned.");
            return;
        }

        List<ResourceInfo> outputResources = productionCardSO.ResourceOutput.GetAllResources();
        for (int i = 0; i < outputConnectors.Count; i++)
        {
            Connector currentOutputConnector = outputConnectors[i];
            if (currentOutputConnector == null || !currentOutputConnector.IsConnected()) continue;

            // Determine which resource to produce for this connector
            ResourceInfo resourceToProduce = default;
            if (i < outputResources.Count)
            {
                resourceToProduce = outputResources[i];
            }
            else if (outputResources.Count > 0)
            {
                resourceToProduce = outputResources[i % outputResources.Count]; // Cycle through if more connectors than resources
            }
            else
            {
                Debug.LogWarning("No output resources defined in ProductionCardData, but output connectors exist.");
                continue;
            }

            MakeAndInitializeResource(currentOutputConnector, resourceToProduce);
        }
    }

    void MakeAndInitializeResource(Connector _currentOutputConnector, ResourceInfo _resourceToProduce)
    {
        GameObject outputResObj = Instantiate(productionCardSO.resourcePrefab, _currentOutputConnector.transform.position, Quaternion.identity);
        Resource res = outputResObj.GetComponent<Resource>();
        if (res == null) return;

        if (_resourceToProduce.isRefined)
        {
            res.isRefined = true;
            res.refinedType = _resourceToProduce.refinedType;
        }
        else
        {
            res.isRefined = false;
            res.rawType = _resourceToProduce.rawType;
        }

        if (_currentOutputConnector.conveyor != null)
        {
            ResourceMover mover = outputResObj.GetComponent<ResourceMover>() ?? outputResObj.AddComponent<ResourceMover>();
            mover.Initialize(_currentOutputConnector.conveyor.GetPathPoints());
        }
    }
    private bool ContainsResource<T>(T[ ] resourceArray, T resourceType)
    {
        if (resourceArray == null) return false; // Handle null array case
        foreach (T r in resourceArray)
        {
            if (EqualityComparer<T>.Default.Equals(r, resourceType))
                return true;
        }
        return false;
    }

    private bool AreAllInputConnectorsConnected()
    {
        if (inputConnectors == null || inputConnectors.Count == 0) return true; 

        for (int i = 0; i < inputConnectors.Count; i++)
        {
            if (inputConnectors[i] == null || !inputConnectors[i].IsConnected())
                return false;
        }
        return true;
    }

    private bool AreAllOutputConnectorsConnected()
    {
        if (outputConnectors == null || outputConnectors.Count == 0) return true; 

        for (int i = 0; i < outputConnectors.Count; i++)
        {
            if (outputConnectors[i] == null || !outputConnectors[i].IsConnected())
                return false;
        }
        return true;
    }

    public override CardData GetCardData()
    {
        return productionCardSO;
    }
}