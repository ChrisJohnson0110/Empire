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

    private void OnEnable()
    {
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
                tile.AddComponent<Tile>();

                Tile t = tile.GetComponent<Tile>();

                t.offSetCoord = new Vector2Int(x,y);
                t.cubeCoord = OffsetCube(t.offSetCoord);


                RandomBaseTile.instance.RandomiseBaseTile(t, hr);
                RandomResource.instance.RandomiseResource(t);
            }
        }
    }

    //generate the visual hex
    private HexRenderer GenerateHex(GameObject a_tile)
    {
        //hex settings
        HexRenderer hexRenderer = a_tile.GetComponent<HexRenderer>();
        hexRenderer.outerSize = _outerSize;
        hexRenderer.innerSize = _innerSize;
        hexRenderer.height = _height;
        hexRenderer.DrawMesh();

        return hexRenderer;
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

    private static Vector3Int OffsetCube(Vector2Int offsSet)
    {
        var r = offsSet.y - (offsSet.x - (offsSet.x % 2)) / 2;
        var q = offsSet.x;
        return new Vector3Int(q, r, -q-r);
    }
}
