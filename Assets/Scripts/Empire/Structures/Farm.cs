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
    TileImprovements tileImprovement = new TileImprovements(); //stores type of farm and yeild

    public Farm(Tile a_tile, Empire a_ownedByEmpire) : base(a_ownedByEmpire, a_tile)
    {
        a_ownedByEmpire.structures.Add(this);

        //TODO tile improvements need to be refined
        tileImprovement.resourceType = WhatImprovementShouldBeAddedToTile(a_tile);
        tileImprovement.improvementYieldType.Add(new YieldTypes(YieldTypes.yieldTypes.Wood,5));

        Debug.Log($"nearest city : {TileManager.instance.GetNearestCity(a_tile).cityName}");

        FloatingText.instance.CreateFloatingText(a_tile.gameObject, tileImprovement.resourceType.ToString(), new Vector3(0, 0.5f, 0), Color.black, 3);
    }

    //the type of farm that a tile needs
    private TileImprovements.improvements WhatImprovementShouldBeAddedToTile(Tile a_tile)
    {
        if (a_tile.resourceOnTile.resourceType == Resource.type.Clay)
        {
            return TileImprovements.improvements.clayQuarry;
        }
        else if (a_tile.resourceOnTile.resourceType == Resource.type.Stone)
        {
            return TileImprovements.improvements.stoneQuarry;
        }
        else if (a_tile.resourceOnTile.resourceType == Resource.type.Wheat)
        {
            return TileImprovements.improvements.wheatFarm;
        }
        else if (a_tile.resourceOnTile.resourceType == Resource.type.Wood)
        {
            return TileImprovements.improvements.lumberMill;
        }
        else if (a_tile.resourceOnTile.resourceType == Resource.type.Sheep)
        {
            return TileImprovements.improvements.sheepPen;
        }
        else if (a_tile.resourceOnTile.resourceType == Resource.type.Fish)
        {
            return TileImprovements.improvements.fishingBoat;
        }

        return TileImprovements.improvements.unassigned;
    }
}