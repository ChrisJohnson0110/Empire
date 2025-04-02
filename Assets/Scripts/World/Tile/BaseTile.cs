using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// store the base tile infomation
/// </summary>
[CreateAssetMenu(fileName = "Types", menuName = "ScriptableObjects/Tiles/BaseTile", order = 1)]
public class BaseTile : ScriptableObject
{
    public BaseTileTypes baseTileType;
    public List<YieldTypes> tileYield = new List<YieldTypes>();
    public bool isHilly;
    public bool cannotTraverse;

    public enum BaseTileTypes
    {
        grassland,
        desert,
        plains,

        tundra,
        mountain,

        coast,
        ocean,
        ice,

        oasis,

        naturalwonder
    }
}
