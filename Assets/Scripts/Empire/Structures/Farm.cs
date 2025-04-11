using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is for the farm improvement
/// am farm improvement is an improvement to a tiles yield
/// this is a class to store all of the data relating to the tiles improvement
/// </summary>
public class Farm : Structure
{
    TileImprovements tileImprovement = new TileImprovements();

    public Farm(Tile a_tile, Empire a_ownedByEmpire) : base(a_ownedByEmpire, a_tile)
    {
        a_ownedByEmpire.structures.Add(this);
        tileImprovement.resourceType = TileImprovements.improvements.farm;
        tileImprovement.improvementYieldType.Add(new YieldTypes(YieldTypes.yieldTypes.food,5));
    }
}