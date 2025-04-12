using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this is the general script for any structure
/// it ensures that structures store the empire they are assigned to
/// and also the location of the structuire
/// allows tile to store a structure type that could be any type of structure
/// </summary>
public class Structure
{
    public Empire ownedByEmpire { get; private set; }
    public Tile location { get; private set; }

    public Structure(Empire a_ownedByEmpire, Tile a_structureTile)
    {
        //set structure values
        ownedByEmpire = a_ownedByEmpire;
        location = a_structureTile;

        //claim tile that structure is on
        a_structureTile.ownedByXempire = a_ownedByEmpire;
        //a_structureTile.gameObject.GetComponent<Renderer>().material = a_ownedByEmpire.buildingMaterial; //set mat for tile structure is on
    }
}
