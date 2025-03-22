using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    //display
    [SerializeField]
    GameObject WoodDisplay;
    [SerializeField]
    GameObject StoneDisplay;
    [SerializeField]
    GameObject ClayDisplay;
    [SerializeField]
    GameObject SheepDisplay;
    [SerializeField]
    GameObject WheatDisplay;
    [SerializeField]
    GameObject GoldDisplay;

    //value
    public int wood
    {
        set { wood = value; WoodDisplay.GetComponent<TextMeshPro>().text = wood.ToString(); }
        get { return wood; }
    }
    public int stone
    {
        set { stone = value; StoneDisplay.GetComponent<TextMeshPro>().text = stone.ToString(); }
        get { return stone; }
    }
    public int clay
    {
        set { clay = value; ClayDisplay.GetComponent<TextMeshPro>().text = clay.ToString(); }
        get { return clay; }
    }
    public int sheep
    {
        set { sheep = value; SheepDisplay.GetComponent<TextMeshPro>().text = sheep.ToString(); }
        get { return sheep; }
    }
    public int wheat
    {
        set { wheat = value; WheatDisplay.GetComponent<TextMeshPro>().text = wheat.ToString(); }
        get { return wheat; }
    }
    public int gold
    {
        set { gold = value; GoldDisplay.GetComponent<TextMeshPro>().text = gold.ToString(); }
        get { return gold; }
    }

    private void Start()
    {
        //wood = 0;
        //stone = 0;
        //clay = 0;
        //sheep = 0;
        //wheat = 0;
        //gold = 0;
    }

}
