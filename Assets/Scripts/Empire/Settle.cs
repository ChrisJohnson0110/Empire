using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settle : MonoBehaviour
{
    //should the settle button be displayed
    //e.g. can you settle here
    public bool DisplaySettleButton(Tile tileToCheck)
    {
        TileManager tm = GameObject.FindAnyObjectByType<TileManager>();
        BLmenu bl = GameObject.FindAnyObjectByType<BLmenu>();

        //check all tiles adjacent to clicked tile
        foreach (Tile t in tm.GetAdjacent(tileToCheck))
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
        //get empire
        //
        //GetSelecetedTile().ownedByXempire = ;
        //GetSelecetedTile().hasXcity = ;
        GetClicked gc = GameObject.FindAnyObjectByType<GetClicked>();
        Empire e = gc.currentlySeleceted.GetComponent<Tile>().ownedByXempire = new Empire();
        e.empireName = "new";
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
