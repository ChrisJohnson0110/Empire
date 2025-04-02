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
    public enum yieldTypes
    {
        food,
        production,
        gold,
        faith,
        science,
        culture
    }

    public yieldTypes yieldType;
    public int yieldAmount;

}
