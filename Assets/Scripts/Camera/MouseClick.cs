using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// script for clicking a tile on the grid
/// handles mouse clicks and updates the tiles
/// </summary>
public class MouseClick : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _clickedInfoBox;
    [HideInInspector] public GameObject currentlySeleceted { get; private set; }

    private CitySettle _settleRef;

    

    private void Start()
    {
        currentlySeleceted = new GameObject();
        _clickedInfoBox.text = "";

        _settleRef = GameObject.FindFirstObjectByType<CitySettle>();
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
                    GameObject.FindAnyObjectByType<BuildingsButtons>().UpdateButtonsToIfCanBuild(hitObject.GetComponent<Tile>());
                    GameObject.FindAnyObjectByType<BottomLeftMenu>().HideMenus(true);

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


    // TODO need to refine this info box display


    //info of the clicked tile
    void SetTileOptions(Tile tile)
    {
        string stringOne = "";

        //base tile info

        stringOne += "BT: ";
        stringOne += tile.baseTileType.baseTileType.ToString();

        //tile yield info

        stringOne += "YT: ";
        foreach (YieldTypes yt in tile.baseTileType.tileYield)
        {
            stringOne += yt.yieldType;
            stringOne += yt.yieldAmount;
            stringOne += ". ";
        }

        //resource on tile info

        stringOne += "RT: ";
        if (tile.resourceOnTile != null)
        {
            stringOne += tile.resourceOnTile.resourceType.ToString();
            foreach (YieldTypes rt in tile.resourceOnTile.tileYieldType)
            {
                stringOne += rt.yieldType;
                stringOne += rt.yieldAmount;
                stringOne += ". ";
            }
        }
        else
        {
            stringOne += "none";
        }

        _clickedInfoBox.text = stringOne;
    }
}
