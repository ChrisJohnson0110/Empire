using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    // tile display data
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
}
