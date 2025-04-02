using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// store the improvements on the tile and what increase the improvement gets
/// along with any other data todo with the improvement 
/// </summary>
public class TileImprovements : MonoBehaviour
{
    public enum improvements
    {
        farm,
        quarry,

    }

    public improvements resourceType;

    public List<YieldTypes> improvementYieldType = new List<YieldTypes>();

}
