using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// empire script stores infomation about the entire empire
/// all of the things this empire owns and infomation about the empire itself
/// </summary>
public class Empire
{
    public string empireName;
    public Material ownedMaterial; //material for land owned by this empire
    public Material buildingMaterial; //material for building owned by this empire
    public List<Structure> structures = new List<Structure>();
    public List<Tile> ownedTiles = new List<Tile>();

    // score ?

    // info text?

    //values from owned
    //number of worked resources

    public Empire(string a_empireName, Material a_material)
    {
        empireName = a_empireName;
        ownedMaterial = a_material;
    }

    

}
