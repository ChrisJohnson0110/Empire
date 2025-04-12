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
        tileImprovement.resourceType = WhatImprovementShouldBeAddedToTile(a_tile);
        tileImprovement.improvementYieldType.Add(new YieldTypes(YieldTypes.yieldTypes.food,5));
        City c = GetNearestCity(a_tile);
        c.workedResources.Add(a_tile);
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

    //return the nearest city within 3 iterations of neighbours
    private City GetNearestCity(Tile a_tile)
    {
        foreach (Tile t in a_tile.neighbours)
        {
            if (t.hasStructure.GetType() == typeof(City))
            {
                return (City)t.hasStructure;
            }
        }

        foreach (Tile t in a_tile.neighbours)
        {
            foreach (Tile tn in t.neighbours)
            {
                if (t.hasStructure.GetType() == typeof(City))
                {
                    return (City)t.hasStructure;
                }
            }
        }

        foreach (Tile t in a_tile.neighbours)
        {
            foreach (Tile tn in t.neighbours)
            {
                foreach (Tile tnn in tn.neighbours)
                {
                    if (t.hasStructure.GetType() == typeof(City))
                    {
                        return (City)t.hasStructure;
                    }
                }
            }
        }

        return null;
    }
}