using UnityEngine;

[CreateAssetMenu(fileName = "ResourceValue", menuName = "Scriptable Objects/ResourceValue")]
public class ResourceValueSO : ScriptableObject
{
    public RawResource rawResourceType;
    public RefinedResource refinedResourceType;
    public float neutralDemandValue; // Value when demand is neutral
    // We can add lowDemandValue and highDemandValue later!
}