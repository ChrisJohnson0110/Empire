using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City
{
    public string CityName;
    public Tile CityTile; //tile that the city is on
    public List<Tile> claimedTiles = new List<Tile>(); //claimed tiles that belong to the city
    public List<Resource> workedResources = new List<Resource>(); //resources within the city that are being worked

    private static string[] cityNames = { "Metropolis", "New Haven", "Skyview", "Rivertown", "Evergreen", "Sunset Bay", "Stormhold" };


    public City(Tile tile)
    {
        CityTile = tile;
        TileManager tm = GameObject.FindAnyObjectByType<TileManager>();
        foreach (Tile t in tm.GetAdjacent(tile))
        {
            claimedTiles.Add(t);
        }

        CityName = cityNames[Random.Range(0, cityNames.Length)];
    }
}
