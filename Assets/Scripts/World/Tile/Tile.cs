using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    // tile display data

    List<YieldTypes> yieldTypes;


    public BaseTile baseTileType; //water, grass desert
    public ObstructionTypes.roughTypes roughType;  //hills, marsh, jungle
    public Resource resourceOnTile; //is there a workable resource

    public Empire ownedByXempire; //empire tile is owned by
    public Structure hasStructure; //if this tile is a structure

    public Vector2Int offSetCoord; //hex grid pos
    public Vector3Int cubeCoord; //cube pos
    public List<Tile> neighbours; //hex that are bordering

    TerrainTypes terrainType;

    TileImprovements.improvements improvement;


    //bool isTouchingRiver; //on creation

    bool hasBarbarianCamp; //barbs on spawn edit tile value // bard on destroy edit value
    bool isPillaged; //on pillage edit value


    public GameObject ObjectOnTileModel;


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
