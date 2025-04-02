using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// this script has all of the needed functions for the bottom left menu within the UI
/// holding functions for all of the buttons 
/// </summary>
public class BottomLeftMenu : MonoBehaviour
{
    [SerializeField] private Button _settleButton; 
    [SerializeField] private Button _outpostButton;

    private CitySettle _settleReference;
    private OutpostSettle _outpostReference;

    private void Start()
    {
        _settleReference = GameObject.FindAnyObjectByType<CitySettle>();
        _outpostReference = GameObject.FindAnyObjectByType<OutpostSettle>();
    }

    //toggle the display for dev text on the tiles
    public void ToggleDevText()
    {
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

    //settle button
    public void Settle()
    {
        Tile _targetTile = GameObject.FindAnyObjectByType<MouseClick>().currentlySeleceted.GetComponent<Tile>();
        _settleReference.SettleCity(_targetTile);
        UpdateMenu(_targetTile);
    }
    //outpost button
    public void Outpost()
    {
        Tile _targetTile = GameObject.FindAnyObjectByType<MouseClick>().currentlySeleceted.GetComponent<Tile>();
        _outpostReference.SettleOutPost(_targetTile);
        UpdateMenu(_targetTile);
    }
    //update the bottom left menu
    //are the buttons clickable
    public void UpdateMenu(Tile tile)
    {
        _settleButton.interactable = CheckForNearByCiv(tile);
        _outpostButton.interactable = CheckForNearByCiv(tile);
    }

    //check tiles surrounding the given tile
    //if any are owned return false
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
