using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure
{
    public Empire OwnedByEmpire { get; private set; }
    public Tile location { get; private set; }

    public Structure(Empire ownedByEmpire, Tile structureTile)
    {
        //set structure values
        OwnedByEmpire = ownedByEmpire;
        location = structureTile;

        //claim tile that structure is on
        structureTile.ownedByXempire = ownedByEmpire;
        structureTile.gameObject.GetComponent<Renderer>().material = ownedByEmpire.BuildingMaterial; //set mat for tile structure is on
    }
}
