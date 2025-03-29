using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    Dictionary<Vector3Int, Tile> tiles;

    public List<Tile> allTiles = new List<Tile>(); //all tiles generated
    Settle settleReference; // settle reference

    public static TileManager instance; // Singleton instance

    [SerializeField]
    GameObject highlightObject;

    [SerializeField]
    GameObject selectorObject;

    public GameObject currentlySelectedTile;


    private void Awake()
    {
        // Ensure only one instance exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy extra instances
            return;
        }

        DontDestroyOnLoad(gameObject); // Optional: persist across scenes
    }


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

    public void CreateHexDic()
    {
        tiles = new Dictionary<Vector3Int, Tile>();

        Tile[] hextiles = gameObject.GetComponentsInChildren<Tile>();

        foreach (Tile t in hextiles)
        {
            RegisterTile(t);
        }

        foreach (Tile t in hextiles)
        {
            List<Tile> neighbours = GetNeighbours(t);
            t.neighbours = neighbours;
        }

        //
        //https://www.youtube.com/watch?v=wxVgIH0j8Wg
        //8:35
        //adding a player to add pathfinding //dont need but want to finish video for better optimisation

    }

    void RegisterTile(Tile tile)
    {
        tiles.Add(tile.cubeCoord, tile);
    }

    List<Tile> GetNeighbours(Tile tile)
    {
        List<Tile> neighbours = new List<Tile>();

        Vector3Int[] neighbourCoords = new Vector3Int[]
        {
            new Vector3Int(1,-1,0),
            new Vector3Int(1,0,-1),
            new Vector3Int(0,1,-1),
            new Vector3Int(-1,1,0),
            new Vector3Int(-1,0,1),
            new Vector3Int(0,-1,1),
        };

        foreach (Vector3Int nc in neighbourCoords)
        {
            Vector3Int tileCoord = tile.cubeCoord;

            if (tiles.TryGetValue(tileCoord + nc, out Tile neighbour))
            {
                neighbours.Add(neighbour);
            }
        }

        return neighbours;
    }

    public void OnClickTile(HexRenderer hr)
    {
        highlightObject.transform.position = hr.transform.position; //move the hightlight

        currentlySelectedTile = hr.gameObject; //store the clicked object
    }
    
    public void OnHoverTile(HexRenderer hr)
    {
        selectorObject.transform.position = hr.transform.position; //move the select
    }
}
