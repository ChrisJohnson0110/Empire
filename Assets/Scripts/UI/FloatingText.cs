using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public static FloatingText instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    //create the tile info box above the tile
    public void CreateFloatingText(GameObject a_parent, string a_textContent, Vector3 a_offset, Color a_color, float a_fontSize)
    {
        GameObject textObject = new GameObject("TileInfoText");
        textObject.transform.SetParent(a_parent.transform);
        textObject.transform.localPosition = a_offset;
        textObject.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));

        TextMeshPro textMesh = textObject.AddComponent<TextMeshPro>();
        textMesh.text = a_textContent;

        textMesh.fontSize = a_fontSize;
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.color = a_color;
        textMesh.enableAutoSizing = true;
        textMesh.fontSizeMin = 2f;
        textMesh.fontSizeMax = a_fontSize;
    }
}
