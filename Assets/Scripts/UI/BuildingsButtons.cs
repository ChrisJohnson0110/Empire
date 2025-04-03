using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// controls for all of the buildings buttons
/// </summary>
public class BuildingsButtons : MonoBehaviour
{
    [SerializeField] private Button _settleButton;
    [SerializeField] private Button _outpostButton;

    private CitySettle _settleReference;
    private OutpostSettle _outpostReference;

    private void Start()
    {
        _settleReference = GameObject.FindAnyObjectByType<CitySettle>();
        _outpostReference = GameObject.FindAnyObjectByType<OutpostSettle>();
    }

    //settle button
    public void CreateACity()
    {
        Tile _targetTile = GameObject.FindAnyObjectByType<MouseClick>().currentlySeleceted.GetComponent<Tile>();
        _settleReference.SettleCity(_targetTile);
        UpdateButtonsToIfCanBuild(_targetTile);
    }

    //outpost button
    public void CreateAnOutpost()
    {
        Tile _targetTile = GameObject.FindAnyObjectByType<MouseClick>().currentlySeleceted.GetComponent<Tile>();
        _outpostReference.SettleOutPost(_targetTile);
        UpdateButtonsToIfCanBuild(_targetTile);
    }

    //farm button
    public void CreateAFarm()
    {
        Debug.Log("farm created");
    }


    //update the bottom left menu
    //are the buttons clickable
    public void UpdateButtonsToIfCanBuild(Tile tile)
    {
        _settleButton.interactable = TileManager.CheckForNearByEmpiresLand(tile);
        _outpostButton.interactable = TileManager.CheckForNearByEmpiresLand(tile);
    }
}
