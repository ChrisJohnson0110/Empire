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
    [SerializeField] GameObject cityPrefab;

    [Header("Line Renderer")]
    [SerializeField] GameObject lineRendererPrefab;
    List<GameObject> lines = new List<GameObject>();
    [SerializeField] Vector3 lineBorderOffset = new Vector3(0,0.5f,0);
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
        //check for and add city
        if (tile.hasStructure.GetType() == typeof(City))
        {
            if (tile.ObjectOnTileModel)
            {
                RemoveObjectOnTile(tile);
            }
            AddObjectToTile(tile, cityPrefab);
        }

        DrawBorder();
        ShowBorderOfSelected(GetTilesXDistanceAway(tile, 5));
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

    List<Tile> GetTilesXDistanceAway(Tile tile, int distance)
    {
        List<Tile> tilesToReturn = new List<Tile>();
        List<Tile> tilesToIgnore = new List<Tile>();

        tilesToIgnore.Add(tile);

        if (distance == 0)
        {
            return tilesToReturn;
        }
        
        for (int i = 0; i < distance; i++)
        {
            foreach (Tile tileToIgnore in tilesToIgnore)
            {
                foreach (Tile t in tileToIgnore.neighbours)
                {
                    if (tilesToIgnore.Contains(t) == false)
                    {
                        tilesToIgnore.Add(t);
                    }
                }
            }
        }

        foreach (Tile t in tilesToIgnore)
        {
            foreach (Tile tn in t.neighbours)
            {
                if (tilesToIgnore.Contains(tn) == false)
                {
                    tilesToReturn.Add(tn);
                }
            }
        }

        return tilesToReturn;
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
        tile.ObjectOnTileModel.transform.SetParent(this.transform);
    }

    public void RemoveObjectOnTile(Tile tile)
    {
        if (tile.ObjectOnTileModel)
        {
            Destroy(tile.ObjectOnTileModel);
        }
    }


    void DrawBorder()
    {
        foreach (GameObject line in lines) //TODO probably able to reuse lines to avoid destroying all // not sure if it would be more efficient to check them agaisnt/with-- existing
        {
            Destroy(line);
        }

        foreach (Player p in GameObject.FindObjectsOfType<Player>())
        {
            foreach (Tile tile in p.playersEmprie.ownedTiles)
            {
                foreach (Tile t in tile.neighbours)
                {
                    if (t.ownedByXempire == null)
                    {
                        Vector3 point1 = tile.transform.position;
                        Vector3 point2 = t.transform.position;

                        // Step 1: Midpoint between point1 and point2
                        Vector3 midpoint = (point1 + point2) / 2f;

                        // Step 2: Direction vector of the line
                        Vector3 direction = (point2 - point1).normalized;

                        // Step 3: Find a perpendicular vector in 3D
                        Vector3 arbitraryVector = Vector3.up; // Choose an arbitrary vector
                        if (Vector3.Dot(arbitraryVector, direction) > 0.99f)
                        {
                            arbitraryVector = Vector3.right; // Change if too aligned
                        }

                        // Step 4: Get a perpendicular vector using cross product
                        Vector3 perpendicular = Vector3.Cross(direction, arbitraryVector).normalized;

                        // Step 5: Offset midpoint by the perpendicular vector scaled to the given distance and add to line renderer
                        GameObject g = Instantiate(lineRendererPrefab, tile.transform.position, tile.transform.rotation);
                        g.GetComponent<LineRenderer>().SetWidth(0.1f, 0.1f);
                        g.GetComponent<LineRenderer>().material = p.playersEmprie.OwnedMaterial;
                        g.GetComponent<LineRenderer>().SetPosition(0, midpoint + (perpendicular * 0.5f) + lineBorderOffset);
                        g.GetComponent<LineRenderer>().SetPosition(1, midpoint + (perpendicular * -0.5f) + lineBorderOffset);
                        lines.Add(g);
                    }
                }
            }
        }
    }

    List<GameObject> ShowBorderOfSelected(List<Tile> tiles)
    {
        List<GameObject> VisonRange = new List<GameObject>();
        foreach (Tile tile in tiles)
        {
            foreach (Tile t in tile.neighbours)
            {
                Vector3 point1 = tile.transform.position;
                Vector3 point2 = t.transform.position;

                // Step 1: Midpoint between point1 and point2
                Vector3 midpoint = (point1 + point2) / 2f;

                // Step 2: Direction vector of the line
                Vector3 direction = (point2 - point1).normalized;

                // Step 3: Find a perpendicular vector in 3D
                Vector3 arbitraryVector = Vector3.up; // Choose an arbitrary vector
                if (Vector3.Dot(arbitraryVector, direction) > 0.99f)
                {
                    arbitraryVector = Vector3.right; // Change if too aligned
                }

                // Step 4: Get a perpendicular vector using cross product
                Vector3 perpendicular = Vector3.Cross(direction, arbitraryVector).normalized;

                // Step 5: Offset midpoint by the perpendicular vector scaled to the given distance and add to line renderer
                GameObject g = Instantiate(lineRendererPrefab, tile.transform.position, tile.transform.rotation);
                g.GetComponent<LineRenderer>().SetWidth(0.1f, 0.1f);


                g.GetComponent<LineRenderer>().material = GameObject.FindAnyObjectByType<Player>().playersEmprie.OwnedMaterial; // TODO need to get current player


                g.GetComponent<LineRenderer>().SetPosition(0, midpoint + (perpendicular * 0.5f) + lineBorderOffset);
                g.GetComponent<LineRenderer>().SetPosition(1, midpoint + (perpendicular * -0.5f) + lineBorderOffset);
                VisonRange.Add(g);
            }
        }
        return VisonRange;
    }
}
