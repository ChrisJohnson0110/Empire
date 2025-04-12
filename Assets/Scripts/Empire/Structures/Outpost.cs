using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is for the outpost itself
/// it has a constructor for creating the outpost
/// it will have an functions related to the outpost
/// </summary>
public class Outpost : Structure
{
    //public string state = "Destoryed";

    public Outpost(Tile a_tile, Empire a_ownedByEmpire) : base(a_ownedByEmpire, a_tile)
    {
        a_ownedByEmpire.structures.Add(this);
    }
}
