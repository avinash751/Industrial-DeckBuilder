using UnityEngine;
using CustomInspector;

[CreateAssetMenu(fileName = "ProductionCardData", menuName = "Scriptable Objects/ProductionCardData")]
public class ProductionCardData : CardData
{
    [HorizontalLine("Production Card Data Settings", 3, FixedColor.DustyBlue)]
    public MultiResource ResourceInput;
    public MultiResource ResourceOutput;
    public int ProductionRate;
}

[System.Serializable]
public class MultiResource
{
    public RawResource[] rawResources;
    public RefinedResource[] refinedResources;
}

