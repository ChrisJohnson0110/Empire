using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// store the improvements on the tile and what increase the improvement gets
/// along with any other data todo with the improvement 
/// </summary>
public class TileImprovements
{
    public enum improvements
    {
        wheatFarm, //wheat
        clayQuarry, //clay
        stoneQuarry, //stone
        sheepPen, //sheep
        lumberMill, //wood
        fishingBoat, //fish
        unassigned //forced to return a type, special case
    }

    public improvements resourceType;

    public List<YieldTypes> improvementYieldType = new List<YieldTypes>();

}
