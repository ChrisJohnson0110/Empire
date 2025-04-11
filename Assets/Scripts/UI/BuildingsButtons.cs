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

    private CitySettle _citySettleReference;
    private OutpostSettle _outpostSettleReference;
    private FarmSettle _farmSettleReference;

    Empire PlayersEmpire;

    private void Awake()
    {
        _citySettleReference = GameObject.FindAnyObjectByType<CitySettle>();
        _outpostSettleReference = GameObject.FindAnyObjectByType<OutpostSettle>();
        _farmSettleReference = GameObject.FindAnyObjectByType<FarmSettle>();
        PlayersEmpire = GameObject.FindAnyObjectByType<Player>().playersEmprie; //TODO with more players will need to change how we get this value
    }

    //settle button
    public void CreateACity()
    {
        Tile _targetTile = GameObject.FindAnyObjectByType<MouseClick>().currentlySeleceted.GetComponent<Tile>();
        _citySettleReference.SettleCity(_targetTile, PlayersEmpire);
        AfterBuildUpdate(_targetTile);
    }

    //outpost button
    public void CreateAnOutpost()
    {
        Tile _targetTile = GameObject.FindAnyObjectByType<MouseClick>().currentlySeleceted.GetComponent<Tile>();
        _outpostSettleReference.SettleOutPost(_targetTile, PlayersEmpire);
        AfterBuildUpdate(_targetTile);
    }

    //farm button
    public void CreateAFarm()
    {
        Tile _targetTile = GameObject.FindAnyObjectByType<MouseClick>().currentlySeleceted.GetComponent<Tile>();
        _farmSettleReference.SettleFarm(_targetTile, PlayersEmpire);
        AfterBuildUpdate(_targetTile);
    }

    private void AfterBuildUpdate(Tile a_targetTile)
    {
        //update model
        TileManager _tileManagerReference = GameObject.FindAnyObjectByType<TileManager>();
        _tileManagerReference.ReplaceTileModel(a_targetTile);
        //updateButton
        UpdateButtonsToIfCanBuild(a_targetTile);
    }

    //update the bottom left menu
    //are the buttons clickable
    public void UpdateButtonsToIfCanBuild(Tile tile)
    {
        _settleButton.interactable = TileManager.CheckForNearByEmpiresLand(tile);
        _outpostButton.interactable = TileManager.CheckForNearByEmpiresLand(tile);
    }
}
