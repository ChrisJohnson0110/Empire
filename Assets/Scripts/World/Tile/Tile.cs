using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // tile display data

    List<YieldTypes> yieldTypes;

    // tile data

    //type of base tile
    public BaseTile baseTileType;

    //is the tile obstructed
    public ObstructionTypes.roughTypes roughType;

    //is there a resource on the tile
    public Resource resourceOnTile;

    public Empire ownedByXempire; //empire tile is owned by
    public City hasXcity = null; //if this tile is a city

    //types of terrain //classification of the above values
    //will have to calculate all of these
    //enum terrainTypes
    //{
    //    open_terrain, //flat / level terrain (marsh counts)
    //    rough_terrain, //hilly, forst or jungle
    //    lake, //small collection of in land water 
    //    coast, //tiles that border land
    //    ocean, //deep sea tiles
    //    fresh_water //tiles around lakes/oasis
    //}

    //CalculateTerrainTypes.calc();

    TileImprovements.improvements improvement;


    //bool isTouchingRiver; //on creation

    bool hasBarbarianCamp; //barbs on spawn edit tile value // bard on destroy edit value
    bool isPillaged; //on pillage edit value




    // base modifier list

    // modifiers from buildings

    // modifiers from religion

    //from policy


    public void Start()
    {
        if (baseTileType.baseTileType != BaseTile.BaseTileTypes.ocean)
        {
            if (Random.Range(0, 100) > 80)
            {
                YieldTypes.yieldTypes[] yields = (YieldTypes.yieldTypes[])System.Enum.GetValues(typeof(YieldTypes.yieldTypes)); //all yield tpyes as array
                Resource.type[] resources = (Resource.type[])System.Enum.GetValues(typeof(Resource.type)); //all yield tpyes as array

                YieldTypes randomYield = new YieldTypes();
                randomYield.yieldType = yields[Random.Range(0, yields.Length)];
                randomYield.yieldAmount = Random.Range(1, 3);


                resourceOnTile = new Resource();
                resourceOnTile.resourceType = resources[Random.Range(0, resources.Length)];
                resourceOnTile.tileYieldType.Add(randomYield);
            }
        }
        
    }
}
