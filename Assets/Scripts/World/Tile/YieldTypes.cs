using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
