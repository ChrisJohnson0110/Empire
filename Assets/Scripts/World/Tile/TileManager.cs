using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    Dictionary<Vector3Int, Tile> tiles;

    CitySettle settleReference; // settle reference

    public static TileManager instance; // Singleton instance

    [Header("MouseObjects")]
    [SerializeField] GameObject clickPrefab;
    [SerializeField] GameObject hoverPrefab;
    GameObject clickObject;
    GameObject hoverObject;

    [Header("Prefabs")]
    [SerializeField] GameObject treePrefab;

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
        settleReference = GameObject.FindAnyObjectByType<CitySettle>(); //set settle reference
        GameObject.FindAnyObjectByType<BLmenu>().ToggleDevText();

        clickObject = Instantiate(clickPrefab, new Vector3(-5,-5,-5), gameObject.transform.rotation);
        hoverObject = Instantiate(hoverPrefab, new Vector3(-5,-5,-5), gameObject.transform.rotation);
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

    public void SetupTileManager()
    {
        tiles = new Dictionary<Vector3Int, Tile>();
        CreateDictionary();
        CreateTileModels();


        //https://www.youtube.com/watch?v=wxVgIH0j8Wg
        //8:35
        //adding a player to add pathfinding //dont need but want to finish video for potential better optimisation 

    }

    void CreateTileModels()
    {
        foreach (Tile t in tiles.Values)
        {
            if (t.baseTileType.baseTileType == BaseTile.BaseTileTypes.grassland)
            {
                AddObjectToTile(t, treePrefab);
            }
        }
    }

    void CreateDictionary()
    {
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
            new Vector3Int(0,-1,1),
            new Vector3Int(1,-1,0),
            new Vector3Int(1,0,-1),
            new Vector3Int(0,1,-1),
            new Vector3Int(-1,1,0),
            new Vector3Int(-1,0,1),
        };


        //check dic for if cube coord exists
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
        clickObject.transform.position = hr.transform.position; //move the hightlight
    }
    
    public void OnHoverTile(HexRenderer hr)
    {
        hoverObject.transform.position = hr.transform.position; //move the select
    }

    public void AddObjectToTile(Tile tile, GameObject modelPrefab)
    {
        tile.ObjectOnTileModel = Instantiate(modelPrefab, tile.gameObject.transform.position + new Vector3(-0.2f, 0.7f, 0.5f), tile.gameObject.transform.rotation);
    }

    public void RemoveObjectOnTile(Tile tile)
    {
        if (tile.ObjectOnTileModel)
        {
            Destroy(tile.ObjectOnTileModel);
        }
    }
}
