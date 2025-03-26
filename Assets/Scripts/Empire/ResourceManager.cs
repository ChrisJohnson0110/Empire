using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    //display
    [SerializeField]
    TextMeshProUGUI WoodDisplay;
    [SerializeField]
    TextMeshProUGUI StoneDisplay;
    [SerializeField]
    TextMeshProUGUI ClayDisplay;
    [SerializeField]
    TextMeshProUGUI SheepDisplay;
    [SerializeField]
    TextMeshProUGUI WheatDisplay;
    [SerializeField]
    TextMeshProUGUI GoldDisplay;

    //owned resources
    public YieldTypes wood; //yeild type allows storeage of type and amount
    public YieldTypes stone; 
    public YieldTypes clay; 
    public YieldTypes sheep; 
    public YieldTypes wheat; 
    public YieldTypes gold;

    // Define delegate and event
    public delegate void OnChanged(int value);

    //events
    public event OnChanged WoodChanged;
    public event OnChanged StoneChanged;
    public event OnChanged ClayChanged;
    public event OnChanged SheepChanged;
    public event OnChanged WheatChanged;
    public event OnChanged GoldChanged;

    //public variables
    public int WoodOwned
    {
        get { return wood.yieldAmount; }
        set
        {
            if (wood.yieldAmount != value) // Only trigger if value actually changes
            {
                wood.yieldAmount = value;
                WoodChanged?.Invoke(wood.yieldAmount); // Trigger event
            }
        }
    }
    public int StoneOwned
    {
        get { return stone.yieldAmount; }
        set
        {
            if (stone.yieldAmount != value) // Only trigger if value actually changes
            {
                stone.yieldAmount = value;
                StoneChanged?.Invoke(stone.yieldAmount); // Trigger event
            }
        }
    }
    public int ClayOwned
    {
        get { return clay.yieldAmount; }
        set
        {
            if (clay.yieldAmount != value) // Only trigger if value actually changes
            {
                clay.yieldAmount = value;
                ClayChanged?.Invoke(clay.yieldAmount); // Trigger event
            }
        }
    }
    public int SheepOwned
    {
        get { return sheep.yieldAmount; }
        set
        {
            if (sheep.yieldAmount != value) // Only trigger if value actually changes
            {
                sheep.yieldAmount = value;
                SheepChanged?.Invoke(sheep.yieldAmount); // Trigger event
            }
        }
    }
    public int WheatOwned
    {
        get { return wheat.yieldAmount; }
        set
        {
            if (wheat.yieldAmount != value) // Only trigger if value actually changes
            {
                wheat.yieldAmount = value;
                WheatChanged?.Invoke(wheat.yieldAmount); // Trigger event
            }
        }
    }
    public int GoldOwned
    {
        get { return gold.yieldAmount; }
        set
        {
            if (gold.yieldAmount != value) // Only trigger if value actually changes
            {
                gold.yieldAmount = value;
                GoldChanged?.Invoke(gold.yieldAmount); // Trigger event
            }
        }
    }

    private void Start()
    {
        wood.yieldType = YieldTypes.yieldTypes.gold;
        stone.yieldType = YieldTypes.yieldTypes.gold;
        clay.yieldType = YieldTypes.yieldTypes.gold;
        sheep.yieldType = YieldTypes.yieldTypes.gold;
        wheat.yieldType = YieldTypes.yieldTypes.gold;
        gold.yieldType = YieldTypes.yieldTypes.gold;

        WoodChanged += HandleWoodChanged;
        StoneChanged += HandleStoneChanged;
        ClayChanged += HandleClayChanged;
        SheepChanged += HandleSheepChanged;
        WheatChanged += HandleWheatChanged;
        GoldChanged += HandleGoldChanged;

        WoodOwned++;
        StoneOwned++;
        ClayOwned++;
        SheepOwned++;
        WheatOwned++;
        GoldOwned++;
    }

    private void Update()
    {
        StoneOwned++;
    }

    private void HandleWoodChanged(int value)
    {
        WoodDisplay.text = WoodOwned.ToString();
    }
    private void HandleStoneChanged(int value)
    {
        StoneDisplay.text = StoneOwned.ToString();
    }
    private void HandleClayChanged(int value)
    {
        ClayDisplay.text = ClayOwned.ToString();
    }
    private void HandleSheepChanged(int value)
    {
        SheepDisplay.text = SheepOwned.ToString();
    }
    private void HandleWheatChanged(int value)
    {
        WheatDisplay.text = WheatOwned.ToString();
    }
    private void HandleGoldChanged(int value)
    {
        GoldDisplay.text = GoldOwned.ToString();
    }
}
