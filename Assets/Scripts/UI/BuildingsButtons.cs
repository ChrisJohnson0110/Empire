using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public void Settle()
    {
        Tile _targetTile = GameObject.FindAnyObjectByType<MouseClick>().currentlySeleceted.GetComponent<Tile>();
        _settleReference.SettleCity(_targetTile);
        UpdateMenu(_targetTile);
    }
    //outpost button
    public void Outpost()
    {
        Tile _targetTile = GameObject.FindAnyObjectByType<MouseClick>().currentlySeleceted.GetComponent<Tile>();
        _outpostReference.SettleOutPost(_targetTile);
        UpdateMenu(_targetTile);
    }
    //update the bottom left menu
    //are the buttons clickable
    public void UpdateMenu(Tile tile)
    {
        _settleButton.interactable = TileManager.CheckForNearByEmpiresLand(tile);
        _outpostButton.interactable = TileManager.CheckForNearByEmpiresLand(tile);
    }
}
