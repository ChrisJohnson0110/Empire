using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City
{
    public Empire OwnedByEmpire;
    public string CityName;
    public Tile CityTile; //tile that the city is on
    public List<Tile> claimedTiles = new List<Tile>(); //claimed tiles that belong to the city
    public List<Resource> workedResources = new List<Resource>(); //resources within the city that are being worked

    private static string[] cityNames = { "Metropolis", "New Haven", "Skyview", "Rivertown", "Evergreen", "Sunset Bay", "Stormhold" };


    public City(Tile tile, Empire a_ownedByEmpire)
    {
        OwnedByEmpire = a_ownedByEmpire;
        a_ownedByEmpire.cities.Add(this);
        CityTile = tile;
        ClaimNeigbours(tile);
        CityName = cityNames[Random.Range(0, cityNames.Length)];
    }

    void ClaimNeigbours(Tile tile)
    {
        foreach (Tile t in tile.neighbours)
        {
            claimedTiles.Add(t);
            t.ownedByXempire = OwnedByEmpire;

            HighlightTile(tile);
        }
    }

    void HighlightTile(Tile tile)
    {

        foreach (Tile ta in tile.neighbours) //color all of the onwed tiles of this city
        {
            ta.gameObject.GetComponent<Renderer>().material = OwnedByEmpire.OwnedMaterial;
        }
    }
}
