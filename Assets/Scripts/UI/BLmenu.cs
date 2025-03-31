using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BLmenu : MonoBehaviour
{
    [SerializeField]
    Button settleButton;
    CitySettle settleReference;
    [SerializeField]
    Button outpostButton;
    OutpostSettle outpostReference;

    private void Start()
    {
        settleReference = GameObject.FindAnyObjectByType<CitySettle>();
        outpostReference = GameObject.FindAnyObjectByType<OutpostSettle>();
    }

    public void ToggleDevText()
    {
        //get all tiles children
        //set active/opo
        List<Tile> tiles = new List<Tile>();
        tiles.AddRange(FindObjectsOfType<Tile>());

        foreach (Tile tile in tiles)
        {
            if (tile.transform.childCount > 0)
            {
                Transform child = tile.transform.GetChild(0); // Assuming we toggle the first child
                child.gameObject.SetActive(!child.gameObject.activeSelf);
            }
        }
    }

    public void Settle()
    {
        settleReference.SettleCity();
    }
    public void Outpost()
    {
        outpostReference.SettleOutPost();
    }

    public void UpdateMenu(Tile tile)
    {
        settleButton.interactable = CheckForNearByCiv(tile);
        outpostButton.interactable = CheckForNearByCiv(tile);
    }

    //should the settle button be displayed
    //e.g. can you settle here
    public bool CheckForNearByCiv(Tile tileToCheck)
    {
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
            if (t.ownedByXempire != null) //if owned
            {
                return false;
            }
            foreach (Tile tn in t.neighbours)
            {
                if (tn.ownedByXempire != null) //if owned
                {
                    return false;
                }
            }
        }

        return true; // if not owned
    }

}
