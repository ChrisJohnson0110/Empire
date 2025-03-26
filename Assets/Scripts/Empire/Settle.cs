using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settle : MonoBehaviour
{
    //should the settle button be displayed
    //e.g. can you settle here
    public void DisplaySettleButton()
    {
        //is tile owned by city


        if (1 == 1)
        {
            //display
        }
        else
        {
            //dont
        }
    }

    //function for the settle button
    //establishes city
    public void SettleCity()
    {
        //get empire
        //
        //GetSelecetedTile().ownedByXempire = ;
        //GetSelecetedTile().hasXcity = ;
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
