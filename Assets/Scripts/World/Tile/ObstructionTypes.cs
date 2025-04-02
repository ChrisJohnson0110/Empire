using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// script to store the obstructions that exist on a tile
/// </summary>
[CreateAssetMenu(fileName = "Types", menuName = "ScriptableObjects/Tiles/RoughTypes", order = 1)]
public class ObstructionTypes : ScriptableObject
{
    [SerializeField]
    roughTypes roughType;
    [SerializeField]
    int foodModifier;
    [SerializeField]
    int productionModifier;

    public enum roughTypes
    {
        forest,
        jungle,
        marsh
    }
}
