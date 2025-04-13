using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// format for the yield types storing the type of yield and the amount
/// </summary>
[Serializable]
public class YieldTypes
{
    public YieldTypes(yieldTypes a_yieldType, int a_yeildAmount)
    {
        yieldType = a_yieldType;
        yieldAmount = a_yeildAmount;
    }
    public enum yieldTypes
    {
        Wood,
        Clay,
        Stone,
        Wheat,
        Sheep,
        Fish,
        Gold
    }

    public yieldTypes yieldType;
    public int yieldAmount;

}
