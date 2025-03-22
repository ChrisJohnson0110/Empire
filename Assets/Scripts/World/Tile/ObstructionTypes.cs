using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
