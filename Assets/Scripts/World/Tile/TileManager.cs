using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// has functions relative to interacting with tiles
/// might be worth splitting some functions into other scripts e.g. a script for tile info functions or one for mouse functions
/// </summary>
public class TileManager : MonoBehaviour
{
    private Dictionary<Vector3Int, Tile> _tiles;
    public static TileManager instance; // Singleton instance

    [Header("MouseObjects")]
    [SerializeField] private GameObject _clickPrefab;
    [SerializeField] private GameObject _hoverPrefab;
    private GameObject _clickObject;
    private GameObject _hoverObject;

    [Header("Prefabs")]
    [SerializeField] private Vector3 _objectOffset = new Vector3(5,3.5f,0);
    [SerializeField] private GameObject _treePrefab;
    [SerializeField] private GameObject _cityPrefab;

    [Header("Line Renderer")]
    [SerializeField] private GameObject _lineRendererPrefab;
    [SerializeField] private Vector3 _lineBorderOffset = new Vector3(0,0.5f,0);
    private List<GameObject> _lines = new List<GameObject>();

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
        _clickObject = Instantiate(_clickPrefab, new Vector3(0,-10,0), gameObject.transform.rotation);
        _hoverObject = Instantiate(_hoverPrefab, new Vector3(0, -10, 0), gameObject.transform.rotation);
    }

    public List<Tile> GetAdjacentOfAdjacent(Tile a_tile)
    {
        List<Tile> tilesAdj = new List<Tile>();
        List<Tile> tilesAdjAdj = new List<Tile>();

        foreach (Tile t in a_tile.neighbours) //get adjacent
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

        if (tilesAdjAdj.Contains(a_tile) == true)
        {
            tilesAdjAdj.Remove(a_tile);
        }


        return tilesAdjAdj;
    }

    public void ReplaceTileModel(Tile tile)
    {
        //check for and add city
        if (tile.hasStructure.GetType() == typeof(City))
        {
            if (tile.ObjectOnTileModel)
            {
                RemoveObjectOnTile(tile);
            }
            AddObjectToTile(tile, _cityPrefab);
        }
    }

    public void SetupTileManager()
    {
        _tiles = new Dictionary<Vector3Int, Tile>();
        CreateDictionary();
        CreateTileModels();

        //https://www.youtube.com/watch?v=wxVgIH0j8Wg
        //8:35
        //adding a player to add pathfinding //dont need but want to finish video for potential better optimisation 

    }

    void CreateTileModels()
    {
        foreach (Tile t in _tiles.Values)
        {
            if (t.baseTileType.baseTileType == BaseTile.BaseTileTypes.grassland)
            {
                AddObjectToTile(t, _treePrefab);
            }
        }
    }
    
    void CreateDictionary()
    {
        Tile[] hextiles = gameObject.GetComponentsInChildren<Tile>();

        foreach (Tile tile in hextiles)
        {
            RegisterTile(tile);
        }

        foreach (Tile tile in hextiles)
        {
            List<Tile> neighbours = GetNeighbours(tile);
            tile.neighbours = neighbours;
        }
    }
    //register the tile with cube coord
    void RegisterTile(Tile a_tile)
    {
        _tiles.Add(a_tile.cubeCoord, a_tile);
    }
    //get the neighbours of the tile
    List<Tile> GetNeighbours(Tile a_tile)
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
            Vector3Int tileCoord = a_tile.cubeCoord;

            if (_tiles.TryGetValue(tileCoord + nc, out Tile neighbour))
            {
                neighbours.Add(neighbour);
            }
        }

        return neighbours;
    }
    //when clicking a tile
    public void OnClickTile(HexRenderer a_hexRenderer)
    {
        _clickObject.transform.position = a_hexRenderer.transform.position; //move the hightlight
    }
    //when hovering a tile
    public void OnHoverTile(HexRenderer a_hexRenderer)
    {
        _hoverObject.transform.position = a_hexRenderer.transform.position; //move the select
    }
    //add a model to the tile
    public void AddObjectToTile(Tile a_tile, GameObject a_modelPrefab)
    {
        a_tile.ObjectOnTileModel = Instantiate(a_modelPrefab, a_tile.gameObject.transform.position + _objectOffset, a_tile.gameObject.transform.rotation);
        a_tile.ObjectOnTileModel.transform.SetParent(a_tile.gameObject.transform);
    }
    //remove the model on a tile
    public void RemoveObjectOnTile(Tile a_tile)
    {
        if (a_tile.ObjectOnTileModel)
        {
            Destroy(a_tile.ObjectOnTileModel);
        }
    }
    //draw the borders of all tiles
    public void DrawBorder()
    {
        foreach (GameObject line in _lines) //TODO probably able to reuse lines to avoid destroying all // not sure if it would be more efficient to check them agaisnt/with-- existing
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
                        GameObject g = Instantiate(_lineRendererPrefab, tile.transform.position, tile.transform.rotation);
                        g.layer = LayerMask.NameToLayer("Tiles");
                        g.GetComponent<LineRenderer>().startWidth= 0.1f;
                        g.GetComponent<LineRenderer>().endWidth= 0.1f;


                        g.GetComponent<LineRenderer>().material = p.playersEmprie.ownedMaterial;
                        g.GetComponent<LineRenderer>().SetPosition(0, midpoint + (perpendicular * 0.5f) + _lineBorderOffset);
                        g.GetComponent<LineRenderer>().SetPosition(1, midpoint + (perpendicular * -0.5f) + _lineBorderOffset);
                        _lines.Add(g);
                    }
                }
            }
        }
    }
    List<GameObject> ShowBorderOfSelected(List<Tile> a_tiles)
    {
        List<GameObject> VisonRange = new List<GameObject>();
        foreach (Tile tile in a_tiles)
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
                GameObject g = Instantiate(_lineRendererPrefab, tile.transform.position, tile.transform.rotation);
                g.GetComponent<LineRenderer>().startWidth = 0.1f;
                g.GetComponent<LineRenderer>().endWidth = 0.1f;


                g.GetComponent<LineRenderer>().material = GameObject.FindAnyObjectByType<Player>().playersEmprie.ownedMaterial; // TODO need to get current player


                g.GetComponent<LineRenderer>().SetPosition(0, midpoint + (perpendicular * 0.5f) + _lineBorderOffset);
                g.GetComponent<LineRenderer>().SetPosition(1, midpoint + (perpendicular * -0.5f) + _lineBorderOffset);
                VisonRange.Add(g);
            }
        }
        return VisonRange;
    }
    //check tiles surrounding the given tile
    //if any are owned return false
    public static bool CheckForNearByEmpiresLand(Tile a_tileToCheck)
    {
        //check all tiles adjacent to clicked tile
        foreach (Tile t in a_tileToCheck.neighbours)
        {
            if (t.ownedByXempire != null) //if owned
            {
                return false;
            }
        }

        foreach (Tile t in a_tileToCheck.neighbours)
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
    //check tiles surrounding the given tile
    //if any are owned return false
    public static bool CheckForNearByEmpiresLand()
    {
        MouseClick mouseClickRef = GameObject.FindAnyObjectByType<MouseClick>();

        //check all tiles adjacent to clicked tile
        foreach (Tile t in mouseClickRef.currentlySeleceted.gameObject.GetComponent<Tile>().neighbours)
        {
            if (t.ownedByXempire != null) //if owned
            {
                return false;
            }
        }

        foreach (Tile t in mouseClickRef.currentlySeleceted.gameObject.GetComponent<Tile>().neighbours)
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
    //is the tile owned by the givn empire
    public static bool CheckForOwned(Tile a_tileToCheck, Empire a_EmpireToCheck)
    {
        if (a_tileToCheck.ownedByXempire == a_EmpireToCheck)
        {
            if (a_tileToCheck.hasStructure == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    //is there a resource on the tile
    public static bool CheckForResource(Tile a_tile)
    {
        if (a_tile.resourceOnTile != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //return the nearest city within 3 iterations of neighbours
    public City GetNearestCity(Tile a_tile)
    {
        //TODO
        //get empires list of structures
        //get all cities
        //get distance to each
        //return closest

        foreach (Tile t in a_tile.neighbours)
        {
            if (t.hasStructure == null) { continue; }
            if (t.hasStructure.GetType() == typeof(City))
            {
                return (City)t.hasStructure;
            }
        }

        foreach (Tile t in a_tile.neighbours)
        {
            foreach (Tile tn in t.neighbours)
            {
                if (tn.hasStructure == null) { continue; }
                if (tn.hasStructure.GetType() == typeof(City))
                {
                    return (City)tn.hasStructure;
                }
            }
        }

        foreach (Tile t in a_tile.neighbours)
        {
            foreach (Tile tn in t.neighbours)
            {
                foreach (Tile tnn in tn.neighbours)
                {
                    if (tnn.hasStructure == null) { continue; }
                    if (tnn.hasStructure.GetType() == typeof(City))
                    {
                        return (City)tnn.hasStructure;
                    }
                }
            }
        }

        return null;
    }
}
