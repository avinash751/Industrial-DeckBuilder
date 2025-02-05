using UnityEngine;

public class Resource : MonoBehaviour
{
    public bool isRefined;        // false for raw resource, true for refined.
    public RawResource rawType;   // Set when resource is raw.
    public RefinedResource refinedType;  // Set when resource is refined.
    
    public float moveSpeed = 2f;  // Speed at which this resource moves.
}
