using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Types", menuName = "ScriptableObjects/Tiles/BaseTile", order = 1)]
public class BaseTile : ScriptableObject
{
    [SerializeField]
    public BaseTileTypes baseTileType;
    [SerializeField]
    bool isHilly;
    [SerializeField]
    public List<YieldTypes> tileYield = new List<YieldTypes>();
    [SerializeField]
    bool cannotTraverse;
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
