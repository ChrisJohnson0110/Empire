using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HexGridLayout : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField]
    Vector2Int gridSize;

    [Header("Tile Settings")]
    public float outerSize = 1;
    public float innerSize = 0;
    public float height = 0.5f;
    public bool isFlatTopped;

    //temp
    [SerializeField]
    List<BaseTile> baseTiless = new List<BaseTile>();

    [SerializeField]
    Camera mainCamera;

    TileMaterials tileMaterials;

    private void OnEnable()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        tileMaterials = GameObject.FindObjectOfType<TileMaterials>();
        LayoutGrid();
    }

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
                tile.transform.position = GetPositionForHexCoordinate(new Vector2Int(x, y));

                HexRenderer hexRenderer = tile.GetComponent<HexRenderer>();
                hexRenderer.isFlatTopped = isFlatTopped;
                hexRenderer.outerSize = outerSize;
                hexRenderer.innerSize = innerSize;
                hexRenderer.height = height;
                hexRenderer.SetMaterial(tileMaterials.unAssigned);
                hexRenderer.DrawMesh();

                tile.transform.SetParent(transform, true);

                tile.AddComponent<MeshCollider>();

                Tile t = tile.AddComponent<Tile>();
                t.baseTileType = baseTiless[Random.Range(0, baseTiless.Count)];

                

                if (t.baseTileType.baseTileType == BaseTile.BaseTileTypes.grassland)
                {
                    hexRenderer.SetMaterial(tileMaterials.grass);
                }
                else if (t.baseTileType.baseTileType == BaseTile.BaseTileTypes.desert)
                {
                    hexRenderer.SetMaterial(tileMaterials.desert);
                }
                else if (t.baseTileType.baseTileType == BaseTile.BaseTileTypes.ocean)
                {
                    hexRenderer.SetMaterial(tileMaterials.ocean);
                }

                CreateFloatingText(tile, $"Hex {x},{y}\n<color=red>Type:</color> {t.baseTileType.baseTileType}");
            }
        }

    }
    public void CreateFloatingText(GameObject parent, string textContent)
    {
        GameObject textObject = new GameObject("FloatingText");
        textObject.transform.SetParent(parent.transform);
        textObject.transform.localPosition = new Vector3(0, 0.5f, 0); // Position above hex
        textObject.transform.rotation = Quaternion.Euler(90, 90, 0); // Align text upright in world space

        TextMeshPro textMesh = textObject.AddComponent<TextMeshPro>();
        textMesh.text = textContent;
        textMesh.fontSize = 3;
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.color = Color.white;
        textMesh.richText = true; // Enables color formatting
    }

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

        if (!isFlatTopped)
        {
            shouldOffset = (row % 2) == 0;
            width = Mathf.Sqrt(3) * size;
            height = 2f * size;

            horizontalDistance = width;
            verticalDistance = height * (3f/4f);

            offset = (shouldOffset) ? width / 2 : 0;

            xPosition = (collumn * horizontalDistance) + offset;
            yPosition = (row * verticalDistance);
        }
        else
        {
            shouldOffset = (collumn % 2) == 0;
            width = 2f * size;
            height = Mathf.Sqrt(3) * size;

            horizontalDistance = width * (3f/4f);
            verticalDistance = height;

            offset = (shouldOffset) ? height / 2 : 0;

            xPosition = (collumn * horizontalDistance);
            yPosition = (row * verticalDistance) - offset;
        }

        return new Vector3(xPosition, 0, -yPosition);
    }
}
