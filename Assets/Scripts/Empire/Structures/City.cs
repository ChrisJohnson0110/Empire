using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : Structure
{
    public string CityName;
    public List<Tile> claimedTiles = new List<Tile>(); //claimed tiles that belong to the city
    public List<Resource> workedResources = new List<Resource>(); //resources within the city that are being worked

    private static string[] cityNames = { "Metropolis", "New Haven", "Skyview", "Rivertown", "Evergreen", "Sunset Bay", "Stormhold" };

    public City(Tile tile, Empire a_ownedByEmpire) : base(a_ownedByEmpire, tile)
    {
        a_ownedByEmpire.structures.Add(this); //add this structure to the empire

        ClaimNeigbours(tile, a_ownedByEmpire); 

        tile.gameObject.GetComponent<Renderer>().material = a_ownedByEmpire.BuildingMaterial; //set mat for city
        CityName = cityNames[Random.Range(0, cityNames.Length)]; //assign random name
    }

    void ClaimNeigbours(Tile tile, Empire a_ownedByEmpire)
    {
        foreach (Tile t in tile.neighbours)
        {
            //add tile to empire and city
            claimedTiles.Add(t);
            a_ownedByEmpire.ownedTiles.Add(t);
            //add claim info to tile
            t.ownedByXempire = OwnedByEmpire;
            


            // TODO needs to be add highlight not replace material

            //t.gameObject.GetComponent<Renderer>().material = OwnedByEmpire.OwnedMaterial; //change materials
        }
    }
}
