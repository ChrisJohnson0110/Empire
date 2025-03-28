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
        BLmenu bl = GameObject.FindAnyObjectByType<BLmenu>();

        //check all tiles adjacent to clicked tile
        foreach (Tile t in tm.GetAdjacentOfAdjacent(tileToCheck))
        {
            if (t.ownedByXempire != null) //if owned
            {
                //cant settle // hide button
                bl.ToggleSettleButton(false);
                return false; //early out
            }
        }

        //can settle //show button
        bl.ToggleSettleButton(true);
        return true;
    }

    //function for the settle button
    //establishes city
    public void SettleCity()
    {
        Tile targetTile = GameObject.FindAnyObjectByType<GetClicked>().currentlySeleceted.GetComponent<Tile>();

        Empire empire = targetTile.ownedByXempire = new Empire(); //this needs to be the empire that is playing
        empire.empireName = "new"; //not needed here

        City c = targetTile.hasXcity = new City(targetTile); //create new city

        targetTile.gameObject.GetComponent<Renderer>().material = cityMat; //set mat for city
        TileManager tm = GameObject.FindAnyObjectByType<TileManager>();

        
        //test
        foreach (Tile ta in tm.GetAdjacentOfAdjacent(targetTile)) //color all of the onwed tiles of this city
        {
            ta.gameObject.GetComponent<Renderer>().material = testingMat;
        }

        //color allm owned tiles
        foreach (Tile t in tm.GetAdjacent(targetTile)) //color all of the onwed tiles of this city
        {
            t.ownedByXempire = empire;
            t.gameObject.GetComponent<Renderer>().material = ownedMat;
        }


        
        Debug.Log("now owned by new empire");
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
