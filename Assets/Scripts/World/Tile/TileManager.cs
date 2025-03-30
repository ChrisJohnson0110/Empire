using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    Dictionary<Vector3Int, Tile> tiles;

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

    public List<Tile> GetAdjacentOfAdjacent(Tile tile)
    {
        List<Tile> tilesAdj = new List<Tile>();
        List<Tile> tilesAdjAdj = new List<Tile>();

        foreach (Tile t in tile.neighbours) //get adjacent
        {
            tilesAdj.Add(t);
            
        }

        foreach (Tile ta in tilesAdj) //all adj
        {
            foreach (Tile tileAdjAdj in ta.neighbours)
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
