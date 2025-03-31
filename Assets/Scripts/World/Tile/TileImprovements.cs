using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
