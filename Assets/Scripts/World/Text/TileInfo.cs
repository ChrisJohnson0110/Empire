using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// floating text display
/// used to display tile info within the game such as:
/// yeild tpyes and amount, resources the tile has
/// </summary>
public class TileInfo : MonoBehaviour
{
    [Header("Offsets")]
    [SerializeField] private Vector3 _offsetYields = new Vector3(0, 0.3f, 0);
    [SerializeField] private Vector3 _offsetResources = new Vector3(0, 0.3f, 0.3f);

    private void Start()
    {
        Invoke(nameof(CreateTileInfo), 0.01f); // Delayed execution to ensure runs after terrain generation
    }

    //generate the tile infomation needed for the display box
    void CreateTileInfo()
    {
        foreach (Tile tile in FindObjectsOfType<Tile>())
        {

            // Create a Parent Object
            GameObject yo = new GameObject("r");
            yo.transform.SetParent(tile.transform);
            yo.transform.localPosition = _offsetResources;
            yo.transform.localRotation = Quaternion.Euler(90, 0, 0); // Rotate for proper world alignment

            // Add RectTransform for Correct Alignment
            RectTransform rt = yo.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(2f, 0.5f); // Adjust size to fit text
            rt.localScale = Vector3.one * 0.1f; // Scale down the text

            // Display Resource Text
            if (tile.resourceOnTile != null)
            {
                FloatingText.instance.CreateFloatingText(yo, $" {tile.resourceOnTile.resourceType} ", new Vector3(0,0,0), Color.black, 3);
            }



            // Create a Parent Object
            GameObject yieldObject = new GameObject("TileInfo");
            yieldObject.transform.SetParent(tile.transform);
            yieldObject.transform.localPosition = _offsetYields;
            yieldObject.transform.localRotation = Quaternion.Euler(90, 0, 0); // Rotate for proper world alignment

            // Add RectTransform for Correct Alignment
            RectTransform rectTransform = yieldObject.AddComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(2f, 0.5f); // Adjust size to fit text
            rectTransform.localScale = Vector3.one * 0.1f; // Scale down the text

            

            // Add GridLayoutGroup for Horizontal Layout
            GridLayoutGroup gridLayout = yieldObject.AddComponent<GridLayoutGroup>();
            gridLayout.cellSize = new Vector2(1.29f, 0f); // Adjust size for text elements
            gridLayout.spacing = new Vector2(0.1f, 3); // Space text elements apart
            gridLayout.startAxis = GridLayoutGroup.Axis.Horizontal;
            gridLayout.childAlignment = TextAnchor.MiddleCenter;

            // Gather and Display Yield Types
            List<YieldTypes> yields = new List<YieldTypes>();
            foreach (YieldTypes yt in tile.baseTileType.tileYield) yields.Add(yt);
            if (tile.resourceOnTile != null)
            {
                foreach (YieldTypes yt in tile.resourceOnTile.tileYieldType) yields.Add(yt);
            }

            Color textColor;

            foreach (YieldTypes yt in yields)
            {
                if (yt.yieldType == YieldTypes.yieldTypes.food)
                {
                    textColor = Color.green;
                }
                else if (yt.yieldType == YieldTypes.yieldTypes.production)
                {
                    textColor = Color.red;
                }
                else if (yt.yieldType == YieldTypes.yieldTypes.science)
                {
                    textColor = Color.blue;
                }
                else if (yt.yieldType == YieldTypes.yieldTypes.faith)
                {
                    textColor = Color.white;
                }
                else if (yt.yieldType == YieldTypes.yieldTypes.culture)
                {
                    textColor = Color.magenta;
                }
                else if (yt.yieldType == YieldTypes.yieldTypes.gold)
                {
                    textColor = Color.yellow;
                }
                else
                {
                    textColor = Color.black;
                }
                FloatingText.instance.CreateFloatingText(yieldObject, $" {yt.yieldAmount} ", _offsetYields, textColor, 5);
            }
        }
    }
}