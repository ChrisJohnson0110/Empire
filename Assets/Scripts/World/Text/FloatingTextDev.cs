using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingTextDev : MonoBehaviour
{
    private void Start()
    {
        foreach (Tile tile in GameObject.FindObjectsOfType<Tile>()) // cycle though each tile in grid
        {
            //add text above tile to help debugging
            CreateFloatingText(tile.gameObject, $"{tile.gameObject.name}\n<color=red>Type:</color> {tile.baseTileType.baseTileType}");
        }
    }

    public void CreateFloatingText(GameObject parent, string textContent)
    {
        GameObject textObject = new GameObject("FloatingText");
        textObject.transform.SetParent(parent.transform);
        textObject.transform.localPosition = new Vector3(0, 0.5f, 0); // Position above hex
        textObject.transform.rotation = Quaternion.Euler(90, 0, 0); // Align text upright in world space

        TextMeshPro textMesh = textObject.AddComponent<TextMeshPro>();
        textMesh.text = textContent;
        textMesh.fontSize = 3;
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.color = Color.white;
        textMesh.richText = true; // Enables color formatting
    }
}




