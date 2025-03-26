using UnityEngine;

public enum RawResource
{
    None,
    Stone,
    Wood,
    Water,
    MetalOre,

}

public enum RefinedResource
{
    None,
    StoneBricks, 
    WoodPlanks,
    Cement,
    MetalIngot, // (metal ore)
    Steel,  // (metal Ingot and water)
    Plywood, // (wood planks and water)
    ConcreteBlock,// (cement and stone bricks)
    HandTools, // (Steel and wood planks)
}
