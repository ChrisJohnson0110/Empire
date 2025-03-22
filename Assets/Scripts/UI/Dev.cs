using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dev : MonoBehaviour
{
    private void Start()
    {
        ToggleText();
    }

    public void ToggleText()
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
}
