using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settle : MonoBehaviour
{
    [SerializeField]
    Material ownedMat;
    [SerializeField]
    Material testingMat; //test
    [SerializeField]
    Material cityMat;


    //should the settle button be displayed
    //e.g. can you settle here
    public bool DisplaySettleButton(Tile tileToCheck)
    {
        TileManager tm = GameObject.FindAnyObjectByType<TileManager>();

        //check all tiles adjacent to clicked tile
        foreach (Tile t in tileToCheck.neighbours)
        {
            if (t.ownedByXempire != null) //if owned
            {
                return false; 
            }
        }
        foreach (Tile t in tileToCheck.neighbours)
        {
            foreach (Tile tn in t.neighbours)
            {
                if (t.ownedByXempire != null) //if owned
                {
                    return false;
                }
            }
        }

        return true; // if not owned
    }

    //function for the settle button
    //establishes city
    public void SettleCity()
    {
        Tile targetTile = GameObject.FindAnyObjectByType<GetClicked>().currentlySeleceted.GetComponent<Tile>(); //the clicked tile that will be settled

        Empire empire = targetTile.ownedByXempire = GameObject.FindAnyObjectByType<Player>().playersEmprie;  //assign empire
        targetTile.hasXcity = new City(targetTile, empire); //create new city
        Debug.Log($"now owned by{empire.empireName}");


        targetTile.gameObject.GetComponent<Renderer>().material = cityMat; //set mat for city

        TileManager tm = GameObject.FindAnyObjectByType<TileManager>();
    }

    Tile GetSelecetedTile()
    {
        Tile clickedTile = Camera.main.GetComponent<GetClicked>()
                            .currentlySeleceted.GetComponent<Tile>();
        return clickedTile;
    }

    void EstablishCity()
    {
        //get empire
        //
    }
}
