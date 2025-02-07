using UnityEngine;
using System.Collections.Generic;
using CustomInspector;

public class ExportZone : MonoBehaviour, IResourceReceiver
{
    [Header("Resource Tracking")]
    [SerializeField] private Dictionary<RawResource, int> rawResourcesInventory = new Dictionary<RawResource, int>();
    [SerializeField] private Dictionary<RefinedResource, int> refinedResourcesInventory = new Dictionary<RefinedResource, int>();

    [Header("Resource Values")]
    [SerializeField] private ResourceValueSO resourceValueTable; // Assign ResourceValueTableSO in Inspector

    [Header("Selling Settings")]
    [SerializeField] private float sellInterval = 5f;
    [SerializeField][ReadOnly] private float sellTimer = 0f;

    private void Update()
    {
        sellTimer += Time.deltaTime;
        if (sellTimer >= sellInterval)
        {
            SellAllResources();
            sellTimer = 0f;
        }
    }

    public void ReceiveResource(Resource resource)
    {
        Debug.Log($"Export Zone received: {resource.rawType} or {resource.refinedType}");

        if (!resource.isRefined)
        {
            if (rawResourcesInventory.ContainsKey(resource.rawType))
            {
                rawResourcesInventory[resource.rawType]++;
            }
            else
            {
                rawResourcesInventory.Add(resource.rawType, 1);
            }
            Debug.Log($"Raw Resources in Export Zone: {ResourceInventoryToString(rawResourcesInventory)}");
        }
        else
        {
            if (refinedResourcesInventory.ContainsKey(resource.refinedType))
            {
                refinedResourcesInventory[resource.refinedType]++;
            }
            else
            {
                refinedResourcesInventory.Add(resource.refinedType, 1);
            }
            Debug.Log($"Refined Resources in Export Zone: {ResourceInventoryToString(refinedResourcesInventory)}");
        }

        Destroy(resource.gameObject);
    }

    void SellAllResources()
    {
        float totalValue = 0f;
        totalValue = GetValueOfTotalResourcesSold();

        if (totalValue > 0)
        {
            MoneyManager.Instance.AddMoney(totalValue);
            Debug.Log($"Export Zone sold resources for ${totalValue:F2}");
        }
        else
        {
            Debug.Log("Export Zone inventory empty, nothing to sell.");
        }

        rawResourcesInventory.Clear();
        refinedResourcesInventory.Clear();
    }

    private float GetValueOfTotalResourcesSold()
    {
        float totalValue = 0f;
        foreach (var pair in rawResourcesInventory)
        {
            float resourceValue = GetRawResourceValue(pair.Key);
            totalValue += resourceValue * pair.Value;
        }

        foreach (var pair in refinedResourcesInventory)
        {
            float resourceValue = GetRefinedResourceValue(pair.Key);
            totalValue += resourceValue * pair.Value;
        }

        return totalValue;
    }

    private float GetRawResourceValue(RawResource resourceType)
    {
        if (resourceValueTable == null || resourceValueTable.rawResourceValues == null) return 0f; // Safety checks

        foreach (var resourceValue in resourceValueTable.rawResourceValues)
        {
            if (resourceValue.rawResourceType == resourceType)
            {
                return resourceValue.neutralDemandValue;
            }
        }
        Debug.LogWarning($"No value found in ResourceValueTable for RawResource: {resourceType}");
        return 0f;
    }

    private float GetRefinedResourceValue(RefinedResource resourceType)
    {
        if (resourceValueTable == null || resourceValueTable.refinedResourceValues == null) return 0f; // Safety checks

        foreach (var resourceValue in resourceValueTable.refinedResourceValues)
        {
            if (resourceValue.refinedResourceType == resourceType)
            {
                return resourceValue.neutralDemandValue;
            }
        }
        Debug.LogWarning($"No value found in ResourceValueTable for RefinedResource: {resourceType}");
        return 0f;
    }

    // Helper method for debugging inventory (same as before)
    private string ResourceInventoryToString<T>(Dictionary<T, int> inventory)
    {
        string inventoryString = "";
        foreach (var pair in inventory)
        {
            inventoryString += $"{pair.Key}: {pair.Value}, ";
        }
        return inventoryString.TrimEnd(',', ' ');
    }
}