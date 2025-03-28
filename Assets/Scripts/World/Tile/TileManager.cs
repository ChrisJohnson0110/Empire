using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    public List<Tile> allTiles = new List<Tile>(); //all tiles generated
    Settle settleReference; // settle reference

    private void Start()
    {
        settleReference = GameObject.FindAnyObjectByType<Settle>(); //set settle reference
    }

    public List<Tile> GetAdjacent(Tile tile)
    {
        List<Tile> tilesAdjacent = new List<Tile>();

        // remove "Hex " and split at ','
        string[] parts = tile.gameObject.name.Substring(4).Split(',');

        // Parse the coords
        int x = int.Parse(parts[0]);
        int y = int.Parse(parts[1]);

        //ADD ALL COORDS
        List<Vector2> tiles = new List<Vector2>();
        tiles.Add(new Vector2(x, y - 1)); //below
        tiles.Add(new Vector2(x, y + 1)); //above

        if (x % 2 != 0)
        {
            tiles.Add(new Vector2(x - 1, y));
            tiles.Add(new Vector2(x - 1, y + 1));
            tiles.Add(new Vector2(x + 1, y));
            tiles.Add(new Vector2(x + 1, y + 1));
        }
        else
        {
            tiles.Add(new Vector2(x - 1, y));
            tiles.Add(new Vector2(x - 1, y - 1));
            tiles.Add(new Vector2(x + 1, y));
            tiles.Add(new Vector2(x + 1, y - 1));
        }
        
        foreach (Tile t in allTiles)
        {
            foreach (Vector2 coord in tiles)
            {
                if (t.gameObject.name == $"Hex {coord.x.ToString()},{coord.y.ToString()}")
                {
                    tilesAdjacent.Add(t);
                    if (tilesAdjacent.Count == 6)
                    {
                        break;
                    }
                }
            }
            
        }

        return tilesAdjacent;
    }

    public List<Tile> GetAdjacentOfAdjacent(Tile tile)
    {
        List<Tile> tilesAdj = new List<Tile>();
        List<Tile> tilesAdjAdj = new List<Tile>();

        foreach (Tile t in GetAdjacent(tile)) //get adjacent
        {
            tilesAdj.Add(t);
            
        }

        foreach (Tile ta in tilesAdj) //all adj
        {
            foreach (Tile tileAdjAdj in GetAdjacent(ta))
            {
                if (tilesAdjAdj.Contains(tileAdjAdj) == false)
                {
                    tilesAdjAdj.Add(tileAdjAdj);
                }
            }
        }

        if (tilesAdjAdj.Contains(tile) == true)
        {
            tilesAdjAdj.Remove(tile);
        }


        return tilesAdjAdj;
    }

    public void UpdateTile(Tile tile)
    {
        //should maybe call function on the button scipt that handles all button checks ?
        //need to clean up this whole interaction
        
        settleReference.DisplaySettleButton(tile);
    }
}
