using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// create the hexagon grid that is the map
/// NOTE: Reading material for hexagon grids : https://www.redblobgames.com/grids/hexagons/
/// </summary>
public class HexGridLayout : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private Vector2Int _gridSize; //size of map to generate

    [Header("Tile Settings")]
    private float _outerSize = 1; // boder size for hex
    private float _innerSize = 0; // inner border e.g. hole
    private float _height = 0.5f; // height

    //Tile material generation
    [Header("Tile Material Generation")]
    [SerializeField] private List<BaseTile> _baseTiless = new List<BaseTile>(); //list of base tiles for random hex assignment
    private TileMaterials _tileMaterials; //reference to script holding materials for use

    [Header("Perlin Noise Settings")]
    [SerializeField] private float _noiseSeed = -1;
    [SerializeField] private float _noiseFrequency = 100f;
    [SerializeField] private float _noiseThreshhold = 0.5f;

    private void OnEnable()
    {
        _tileMaterials = GameObject.FindObjectOfType<TileMaterials>();

        LayoutGrid(); //create grid
        GameObject.FindObjectOfType<TileManager>().SetupTileManager(); //create dic of tiles
    }

    //grid creation
    private void LayoutGrid()
    {
        for (int y = 0; y < _gridSize.y; y++)
        {
            for (int x = 0; x < _gridSize.x; x++)
            {
                GameObject tile = new GameObject($"Hex {x},{y}", typeof(HexRenderer));
                tile.transform.SetParent(transform, true);
                tile.layer = LayerMask.NameToLayer("Tiles");

                tile.transform.position = GetPositionForHexCoordinate(new Vector2Int(x, y));
                HexRenderer hr = GenerateHex(tile);
       
                //add collider - for raycast
                tile.AddComponent<MeshCollider>();

                //tile for tile details
                Tile t = tile.AddComponent<Tile>();
                t.offSetCoord = new Vector2Int(x,y);
                t.cubeCoord = OffsetCube(t.offSetCoord);

                //visuals
                RandomiseMaterials(t, hr);
            }
        }
    }

    //generate the visual hex
    HexRenderer GenerateHex(GameObject tile)
    {
        //hex settings
        HexRenderer hexRenderer = tile.GetComponent<HexRenderer>();
        hexRenderer.outerSize = _outerSize;
        hexRenderer.innerSize = _innerSize;
        hexRenderer.height = _height;
        hexRenderer.SetMaterial(_tileMaterials.unAssigned);
        hexRenderer.DrawMesh();

        return hexRenderer;
    }

    //assign tile types and materials to all of the tiles
    void RandomiseMaterials(Tile tile, HexRenderer hexRenderer)
    {
        if (_noiseSeed == -1)
        {
            _noiseSeed = Random.Range(0, 1000000);
        }

        float waterValue = Mathf.PerlinNoise((tile.offSetCoord.x + _noiseSeed) / _noiseFrequency, (tile.offSetCoord.y + _noiseSeed) / _noiseFrequency);

        bool isWater = waterValue < _noiseThreshhold;
        if (isWater)
        {
            tile.baseTileType = _baseTiless[2];
            hexRenderer.SetMaterial(_tileMaterials.ocean);
        }
        else
        {
            tile.baseTileType = _baseTiless[Random.Range(0, _baseTiless.Count - 1)]; //random for now

            //change material based on random basetile given
            if (tile.baseTileType.baseTileType == BaseTile.BaseTileTypes.grassland)
            {
                hexRenderer.SetMaterial(_tileMaterials.grass);
            }
            else if (tile.baseTileType.baseTileType == BaseTile.BaseTileTypes.plains)
            {
                hexRenderer.SetMaterial(_tileMaterials.plains);
            }
            else
            {
                hexRenderer.SetMaterial(_tileMaterials.unAssigned);
            }

        }
    }

    //get hex position
    private Vector3 GetPositionForHexCoordinate(Vector2Int coordinate)
    {
        int collumn = coordinate.x;
        int row = coordinate.y;

        float width;
        float height;
        float xPosition;
        float yPosition;

        bool shouldOffset;

        float horizontalDistance;
        float verticalDistance;
        float offset;
        float size = _outerSize;


            shouldOffset = (collumn % 2) == 0;
            width = 2f * size;
            height = Mathf.Sqrt(3) * size;

            horizontalDistance = width * (3f/4f);
            verticalDistance = height;

            offset = (shouldOffset) ? height / 2 : 0;

            xPosition = (collumn * horizontalDistance);
            yPosition = (row * verticalDistance) - offset;
 

        return new Vector3(xPosition, 0, -yPosition);
    }

    static Vector3Int OffsetCube(Vector2Int offsSet)
    {
        var r = offsSet.y - (offsSet.x - (offsSet.x % 2)) / 2;
        var q = offsSet.x;
        return new Vector3Int(q, r, -q-r);
    }
}
