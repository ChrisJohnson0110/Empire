using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

/// <summary>
/// script that is responsible for the resources owned
/// and for displayig the infomation
/// </summary>
public class ResourceManager : MonoBehaviour
{
    //TODO
    //make sure infomation is somwhow tied to correct player

    //display
    [SerializeField] private TextMeshProUGUI _woodDisplay;
    [SerializeField] private TextMeshProUGUI _stoneDisplay;
    [SerializeField] private TextMeshProUGUI _clayDisplay;
    [SerializeField] private TextMeshProUGUI _sheepDisplay;
    [SerializeField] private TextMeshProUGUI _wheatDisplay;
    [SerializeField] private TextMeshProUGUI _goldDisplay;

    //owned resources
    //yeild type allows storeage of type and amount
    public YieldTypes _wood;
    public YieldTypes _stone; 
    public YieldTypes _clay; 
    public YieldTypes _sheep; 
    public YieldTypes _wheat; 
    public YieldTypes _gold;

    // Define delegate and event
    public delegate void OnChanged(int value);

    //events
    public event OnChanged woodChanged;
    public event OnChanged stoneChanged;
    public event OnChanged clayChanged;
    public event OnChanged sheepChanged;
    public event OnChanged wheatChanged;
    public event OnChanged goldChanged;

    //public variables
    public int woodOwned
    {
        get { return _wood.yieldAmount; }
        set
        {
            if (_wood.yieldAmount != value) // Only trigger if value actually changes
            {
                _wood.yieldAmount = value;
                woodChanged?.Invoke(_wood.yieldAmount); // Trigger event
            }
        }
    }
    public int stoneOwned
    {
        get { return _stone.yieldAmount; }
        set
        {
            if (_stone.yieldAmount != value) // Only trigger if value actually changes
            {
                _stone.yieldAmount = value;
                stoneChanged?.Invoke(_stone.yieldAmount); // Trigger event
            }
        }
    }
    public int clayOwned
    {
        get { return _clay.yieldAmount; }
        set
        {
            if (_clay.yieldAmount != value) // Only trigger if value actually changes
            {
                _clay.yieldAmount = value;
                clayChanged?.Invoke(_clay.yieldAmount); // Trigger event
            }
        }
    }
    public int sheepOwned
    {
        get { return _sheep.yieldAmount; }
        set
        {
            if (_sheep.yieldAmount != value) // Only trigger if value actually changes
            {
                _sheep.yieldAmount = value;
                sheepChanged?.Invoke(_sheep.yieldAmount); // Trigger event
            }
        }
    }
    public int wheatOwned
    {
        get { return _wheat.yieldAmount; }
        set
        {
            if (_wheat.yieldAmount != value) // Only trigger if value actually changes
            {
                _wheat.yieldAmount = value;
                wheatChanged?.Invoke(_wheat.yieldAmount); // Trigger event
            }
        }
    }
    public int goldOwned
    {
        get { return _gold.yieldAmount; }
        set
        {
            if (_gold.yieldAmount != value) // Only trigger if value actually changes
            {
                _gold.yieldAmount = value;
                goldChanged?.Invoke(_gold.yieldAmount); // Trigger event
            }
        }
    }

    private void Start()
    {
        _wood.yieldType = YieldTypes.yieldTypes.gold;
        _stone.yieldType = YieldTypes.yieldTypes.gold;
        _clay.yieldType = YieldTypes.yieldTypes.gold;
        _sheep.yieldType = YieldTypes.yieldTypes.gold;
        _wheat.yieldType = YieldTypes.yieldTypes.gold;
        _gold.yieldType = YieldTypes.yieldTypes.gold;

        woodChanged += HandleWoodChanged;
        stoneChanged += HandleStoneChanged;
        clayChanged += HandleClayChanged;
        sheepChanged += HandleSheepChanged;
        wheatChanged += HandleWheatChanged;
        goldChanged += HandleGoldChanged;

        woodOwned++;
        stoneOwned++;
        clayOwned++;
        sheepOwned++;
        wheatOwned++;
        goldOwned++;
    }

    /// <summary>
    /// temp
    /// </summary>
    public void IncreaseResources()
    {
        woodOwned++;
        woodOwned++;
        stoneOwned++;
        clayOwned++;
        clayOwned++;
        sheepOwned++;
        wheatOwned++;
        wheatOwned++;
        goldOwned++;
        goldOwned++;
        goldOwned++;
    }

    private void HandleWoodChanged(int value)
    {
        _woodDisplay.text = woodOwned.ToString();
    }
    private void HandleStoneChanged(int value)
    {
        _stoneDisplay.text = stoneOwned.ToString();
    }
    private void HandleClayChanged(int value)
    {
        _clayDisplay.text = clayOwned.ToString();
    }
    private void HandleSheepChanged(int value)
    {
        _sheepDisplay.text = sheepOwned.ToString();
    }
    private void HandleWheatChanged(int value)
    {
        _wheatDisplay.text = wheatOwned.ToString();
    }
    private void HandleGoldChanged(int value)
    {
        _goldDisplay.text = goldOwned.ToString();
    }
}
