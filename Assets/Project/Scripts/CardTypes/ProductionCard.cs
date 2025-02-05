using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CustomInspector;

public class ProductionCard : Card, IResourceReceiver
{
    [ReadOnly][SerializeField][Foldout] private ProductionCardData productionCardSO;
    [SerializeField] private Connector inputConnector;
    [SerializeField] private Connector outputConnector;

    private MultiResource receivedResources = new MultiResource();  // Stores received resource types
    private bool isProductionActive = false;

    public override void InitializeCard(CardData data)
    {
        base.InitializeCard(data);
        productionCardSO = (ProductionCardData)data;
        cardTypeText.text = "Production";

        isProductionActive = false;
        receivedResources = new MultiResource();
    }

    // Called by the connector when a resource reaches the input.
    public void ReceiveResource(Resource resource)
    {
        if (IsValidInput(resource))
        {
            AddResourceToReceived(resource);
            Destroy(resource.gameObject);
            bool allResourcesRecived = CompareMultiResources(receivedResources, productionCardSO.ResourceInput);
            if (!allResourcesRecived)
            {
                Debug.Log("Waiting for all required resources...");
                return;
            }

            if (isProductionActive) return;

            StartCoroutine(ProductionRoutine());
            isProductionActive = true;
        }
        else
        {
            Debug.Log("Invalid resource received: " + resource.rawType);
        }
    }

    private bool IsValidInput(Resource resource)
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

    // Adds received resource to the MultiResource container.
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

    // Checks whether a resource array contains a specific resource.
    private bool ContainsResource<T>(T[] resourceArray, T resourceType)
    {
        foreach (T r in resourceArray)
        {
            if (EqualityComparer<T>.Default.Equals(r, resourceType))
                return true;
        }
        return false;
    }

    // Compares two MultiResource objects to ensure all required resources are present.
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

    // Coroutine that produces output resources at the production rate.
    IEnumerator ProductionRoutine()
    {
        while (true)
        {
            ProduceResource();
            yield return new WaitForSeconds(1f / productionCardSO.ProductionRate);
        }
    }

    void ProduceResource()
    {
        if (outputConnector == null || !inputConnector.IsConnected() || !outputConnector.IsConnected())
            return;

        if (productionCardSO.resourcePrefab == null)
        {
            Debug.LogError("ProductionCardData does not have a resourcePrefab assigned.");
            return;
        }

        // Instantiate output resource at the connector position.
        GameObject outputResObj = Instantiate(productionCardSO.resourcePrefab, outputConnector.transform.position, Quaternion.identity);
        Resource res = outputResObj.GetComponent<Resource>();
        if (res != null)
        {
            res.isRefined = true;
            if (productionCardSO.ResourceOutput.refinedResources.Length > 0)
            {
                res.refinedType = productionCardSO.ResourceOutput.refinedResources[0];
            }
            else if (productionCardSO.ResourceOutput.rawResources.Length > 0)
            {
                res.isRefined = false;
                res.rawType = productionCardSO.ResourceOutput.rawResources[0];
            }
        }

        // If connected to a conveyor, add movement.
        if (outputConnector.conveyor != null)
        {
            ResourceMover mover = outputResObj.GetComponent<ResourceMover>() ?? outputResObj.AddComponent<ResourceMover>();
            mover.Initialize(outputConnector.conveyor.GetPathPoints());
        }
    }
}