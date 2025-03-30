using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//create the hexgrid for the map
//great reading material on hex grids https://www.redblobgames.com/grids/hexagons/
public class HexGridLayout : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField]
    Vector2Int gridSize; //size of map to generate

    [Header("Tile Settings")]
    public float outerSize = 1; // boder size for hex
    public float innerSize = 0; // inner border e.g. hole
    public float height = 0.5f; // height

    //temp
    [SerializeField]
    List<BaseTile> baseTiless = new List<BaseTile>(); //list of base tiles for random hex assignment

    TileMaterials tileMaterials; //reference to script holding materials for use

    TileManager tm;

    private void OnEnable()
    {
        tileMaterials = GameObject.FindObjectOfType<TileMaterials>();
        tm = GameObject.FindObjectOfType<TileManager>();

        LayoutGrid(); //create grid
        tm.CreateHexDic();
    }

    //grid creation
    private void LayoutGrid()
    {
        foreach (Transform child in this.gameObject.transform)
        {
            Destroy(child.gameObject);
        }

        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                GameObject tile = new GameObject($"Hex {x},{y}", typeof(HexRenderer));
                tile.transform.SetParent(transform, true);

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
        hexRenderer.outerSize = outerSize;
        hexRenderer.innerSize = innerSize;
        hexRenderer.height = height;
        hexRenderer.SetMaterial(tileMaterials.unAssigned);
        hexRenderer.DrawMesh();

        return hexRenderer;
    }

    void RandomiseMaterials(Tile tile, HexRenderer hexRenderer)
    {
        tile.baseTileType = baseTiless[Random.Range(0, baseTiless.Count)]; //random for now

        //change material based on random basetile given
        if (tile.baseTileType.baseTileType == BaseTile.BaseTileTypes.grassland)
        {
            hexRenderer.SetMaterial(tileMaterials.grass);
        }
        else if (tile.baseTileType.baseTileType == BaseTile.BaseTileTypes.desert)
        {
            hexRenderer.SetMaterial(tileMaterials.desert);
        }
        else if (tile.baseTileType.baseTileType == BaseTile.BaseTileTypes.ocean)
        {
            hexRenderer.SetMaterial(tileMaterials.ocean);
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
        float size = outerSize;


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
