using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class GetClicked : MonoBehaviour
{
    [SerializeField]
    Material highlightMaterial;
    [SerializeField]
    GameObject clickedInfoBox;

    public GameObject currentlySeleceted;
    HexGridLayout hexGridLayout; //reference for the hex settings
    GameObject clicked; //clicked tile outline
    int layerMask; // ui layer

    Settle settleRef; //


    private void Start()
    {
        hexGridLayout = GameObject.FindObjectOfType<HexGridLayout>();
        clicked = new GameObject("Clicked", typeof(HexRenderer));
        clicked.SetActive(false);
        clickedInfoBox.SetActive(false);
        layerMask = ~LayerMask.GetMask("UI"); //get ui layer

        settleRef = GameObject.FindFirstObjectByType<Settle>();

        //create the tile that will be used for highlighting
        HexRenderer hexRenderer = clicked.GetComponent<HexRenderer>();
        hexRenderer.isFlatTopped = hexGridLayout.isFlatTopped;
        hexRenderer.outerSize = hexGridLayout.outerSize;
        hexRenderer.innerSize = hexGridLayout.innerSize;
        hexRenderer.height = hexGridLayout.height;
        hexRenderer.SetMaterial(highlightMaterial);
        hexRenderer.DrawMesh();

    }

    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {

                // Create a ray from the camera through the mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Perform the raycast
                if (Physics.Raycast(ray, out hit))
                {
                    // Get the object that was hit
                    GameObject hitObject = hit.collider.gameObject;

                    if (currentlySeleceted == hitObject.gameObject) //if clicked currently selected
                    {
                        //remove selection
                        clicked.SetActive(false);
                        currentlySeleceted = null;
                        clickedInfoBox.SetActive(false);

                    }
                    else
                    {
                        //show selection
                        currentlySeleceted = hitObject.gameObject;
                        clicked.transform.position = hitObject.transform.position;
                        clicked.SetActive(true);
                        SetTileOptions();
                        clickedInfoBox.SetActive(true);

                        GameObject.FindAnyObjectByType<TileManager>().UpdateTile(currentlySeleceted.GetComponent<Tile>()); //update the clicked tile
                    }


                }
            }
        }
    }

    //options for the clicked tile
    void SetTileOptions()
    {
        Tile selectedTile = currentlySeleceted.GetComponent<Tile>();

        TextMeshProUGUI InfoBoxOne = clickedInfoBox.transform.Find("1").gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI InfoBoxTwo = clickedInfoBox.transform.Find("2").gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI InfoBoxThree = clickedInfoBox.transform.Find("3").gameObject.GetComponent<TextMeshProUGUI>();

        string stringOne = "";
        string stringTwo = "";
        string stringThree = "";

        //base tile info
        stringOne += "BT: ";
        stringOne += selectedTile.baseTileType.baseTileType.ToString(); //BASE TILE TYPE

        //tile yield info
        stringTwo += "YT: ";
        foreach (YieldTypes yt in selectedTile.baseTileType.tileYield) //yiled types
        {
            stringTwo += yt.yieldType;
            stringTwo += yt.yieldAmount;
            stringTwo += ". ";
        }

        //resource on tile info
        stringThree += "RT: ";
        if (selectedTile.resourceOnTile != null)
        {
            stringThree += selectedTile.resourceOnTile.resourceType.ToString();
            foreach (YieldTypes rt in selectedTile.resourceOnTile.tileYieldType)
            {
                stringThree += rt.yieldType;
                stringThree += rt.yieldAmount;
                stringThree += ". ";
            }
        }
        else
        {
            stringThree += "none";
        }

        InfoBoxOne.text = stringOne;
        InfoBoxTwo.text = stringTwo;
        InfoBoxThree.text = stringThree;
    }
}
