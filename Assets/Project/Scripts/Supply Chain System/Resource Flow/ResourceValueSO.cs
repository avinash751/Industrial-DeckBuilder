using UnityEngine;
using System.Collections.Generic; // Make sure to include this for List
using System;

[CreateAssetMenu(fileName = "ResourceValueTable", menuName = "Scriptable Objects/ResourceValueTable")] // Renamed menu item
public class ResourceValueSO : ScriptableObject
{
    public List<RawResourceValue> rawResourceValues;
    public List<RefinedResourceValue> refinedResourceValues;
}

[Serializable] 
public class RawResourceValue
{
    public RawResource rawResourceType;
    public float neutralDemandValue;
}

[Serializable] 
public class RefinedResourceValue
{
    public RefinedResource refinedResourceType;
    public float neutralDemandValue;
}