using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City
{
    List<Tile> claimedTiles = new List<Tile>(); //claimed tiles that belong to the city
    List<Resource> workedResources = new List<Resource>(); //resources within the city that are being worked
}
