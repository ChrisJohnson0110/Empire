using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class GetClicked : MonoBehaviour
{
    [SerializeField]
    GameObject clickedInfoBox;

    public GameObject currentlySeleceted;

    CitySettle settleRef; //


    private void Start()
    {
        currentlySeleceted = new GameObject();
        clickedInfoBox.SetActive(false);

        settleRef = GameObject.FindFirstObjectByType<CitySettle>();
    }

    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // Create a ray from the camera through the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Input.GetMouseButtonDown(0))
            {
                // Perform the raycast
                if (Physics.Raycast(ray, out hit))
                {
                    // Get the object that was hit
                    GameObject hitObject = hit.collider.gameObject;

                    if (hitObject.TryGetComponent<Tile>(out Tile tile))
                    {
                        currentlySeleceted = hitObject;
                    }

                    //update the menu options for the tile
                    GameObject.FindAnyObjectByType<BLmenu>().UpdateMenu(hitObject.GetComponent<Tile>());

                    //highlight the clicked tile
                    if (hitObject.TryGetComponent<HexRenderer>(out HexRenderer target))
                    {
                        target.OnClickTile();

                        SetTileOptions(target.gameObject.GetComponent<Tile>()); //move this func to tile manager //IMPORTANT
                    }

                }
            }

            //highlight the hovered tile
            if (Physics.Raycast(ray, out hit))
            {
                //alternative highlight
                if (hit.collider.gameObject.TryGetComponent<HexRenderer>(out HexRenderer target))
                {
                    target.OnHoverTile();
                }
            }
        }
    }


    //need to refine this info box display


    //options for the clicked tile
    void SetTileOptions(Tile tile)
    {
        Tile selectedTile = tile.GetComponent<Tile>();

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
