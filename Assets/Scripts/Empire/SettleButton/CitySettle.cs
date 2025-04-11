using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is responsible for the function of creating a city
/// it creates the city and provideds in the needed perameters
/// it also assigns it to the needed empire
/// 
/// this script and the other buildings could inherit from a general building script
/// the only changes between scripts is the type of building that is created
/// has to be some optimiseation made
/// 
/// might be better to move all of this to the bottom left menu script
/// </summary>
public class CitySettle : MonoBehaviour
{
    public void SettleCity(Tile a_targetTile, Empire a_empire)
    {
        a_targetTile.ownedByXempire = a_empire; 
        
        a_targetTile.hasStructure = new City(a_targetTile, a_empire);

        TileManager _tileManagerReference = GameObject.FindAnyObjectByType<TileManager>();
        _tileManagerReference.DrawBorder();

        Debug.Log($"new city settled by {a_empire.empireName}");
    }
}
