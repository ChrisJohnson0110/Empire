using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outpost : Structure
{
    public Outpost(Tile tile, Empire a_ownedByEmpire) : base(a_ownedByEmpire, tile)
    {
        a_ownedByEmpire.structures.Add(this);
    }
}
