using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is for the cities themselves
/// storing any infomation related to then city, setting up the city, and any general city functions
/// will need seperate scripts to handle the citys resources
/// </summary>
public class City : Structure
{
    public string cityName;
    public List<Tile> claimedTiles = new List<Tile>(); //claimed tiles that belong to the city
    public List<Tile> workedResources = new List<Tile>(); //resources within the city that are being worked

    private static string[] _cityNames = { "Metropolis", "New Haven", "Skyview", "Rivertown", "Evergreen", "Sunset Bay", "Stormhold" };

    public City(Tile a_tile, Empire a_ownedByEmpire) : base(a_ownedByEmpire, a_tile)
    {
        a_ownedByEmpire.structures.Add(this); //add this structure to the empire

        ClaimNeigbours(a_tile, a_ownedByEmpire); 
        cityName = _cityNames[Random.Range(0, _cityNames.Length)]; //assign random name


        FloatingText.instance.CreateFloatingText(a_tile.gameObject, cityName, new Vector3(0, 0.5f, 0), Color.black, 4);
    }

    void ClaimNeigbours(Tile a_tile, Empire a_ownedByEmpire)
    {
        foreach (Tile tile in a_tile.neighbours)
        {
            //add tile to empire and city
            claimedTiles.Add(tile);
            a_ownedByEmpire.ownedTiles.Add(tile);
            //add claim info to tile
            tile.ownedByXempire = ownedByEmpire;
        }
    }

    public void GetWorkedResources()
    {
        //combine
        
        //return all owned tiles yeild

        //return all worked resources yeild
    }
}
