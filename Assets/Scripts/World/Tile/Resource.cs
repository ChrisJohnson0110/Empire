using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// data tpye to store resource types and yield
/// along with any other specific infomation to do with a resource
/// </summary>
public class Resource
{
    //bool isHidden; //is the resource shown //client side later maybe
    //Tech techRequiredToSee = new Tech();
    //Tech techRequiredToWork = new Tech();

    //yield
    public enum type
    {
        Wood,
        Clay,
        Stone,
        Wheat,
        Sheep,
        Fish
    }

    public type resourceType;

    public List<YieldTypes> tileYieldType = new List<YieldTypes>();
}
