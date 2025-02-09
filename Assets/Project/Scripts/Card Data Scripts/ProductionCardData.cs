using UnityEngine;
using CustomInspector;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ProductionCardData", menuName = "Scriptable Objects/ProductionCardData")]
public class ProductionCardData : CardData
{
    [HorizontalLine("Production Card Data Settings", 3, FixedColor.DustyBlue)]
    public MultiResource ResourceInput;
    public MultiResource ResourceOutput; // Still MultiResource for output, but we'll handle it to distribute uniquely
    public int ProductionRate;

    [Preview]
    [SerializeField]
    public GameObject resourcePrefab; // Prefab for the resource to spawn
}


[System.Serializable]
public class MultiResource
{
    public RawResource[ ] rawResources;
    public RefinedResource[ ] refinedResources;

    // Helper function to get all resources in a single list (useful for iteration)
    public List<ResourceInfo> GetAllResources()
    {
        List<ResourceInfo> resources = new List<ResourceInfo>();
        if (rawResources != null)
        {
            foreach (var rawRes in rawResources)
            {
                resources.Add(new ResourceInfo { isRefined = false, rawType = rawRes });
            }
        }
        if (refinedResources != null)
        {
            foreach (var refinedRes in refinedResources)
            {
                resources.Add(new ResourceInfo { isRefined = true, refinedType = refinedRes });
            }
        }
        return resources;
    }
}

// Simple struct to hold resource info - moved outside class for general use if needed
[System.Serializable]
public struct ResourceInfo
{
    public bool isRefined;
    public RawResource rawType;
    public RefinedResource refinedType;
}