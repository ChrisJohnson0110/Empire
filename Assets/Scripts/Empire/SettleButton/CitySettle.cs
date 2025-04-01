using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySettle : MonoBehaviour
{
    //function for the settle button
    //establishes city
    public void SettleCity()
    {
        Tile targetTile = GameObject.FindAnyObjectByType<GetClicked>().currentlySeleceted.GetComponent<Tile>(); //the clicked tile that will be settled

        Empire empire = targetTile.ownedByXempire = GameObject.FindAnyObjectByType<Player>().playersEmprie;  //assign empire
        
        targetTile.hasStructure = new City(targetTile, empire); //create new city

        TileManager tm = GameObject.FindAnyObjectByType<TileManager>();
        tm.UpdateTile(targetTile);

        Debug.Log($"now owned by{empire.empireName}");
    }
}
