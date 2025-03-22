using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateTerrainTypes : MonoBehaviour
{
    enum terrainTypes
    {
        open_terrain, //flat / level terrain (marsh counts)
        rough_terrain, //hilly, forst or jungle
        lake, //small collection of in land water 
        coast, //tiles that border land
        ocean, //deep sea tiles
        fresh_water //tiles around lakes/oasis
    }


    //is flood plain //flat river tile in desert  
    bool isTouchingRiver;


    void calc(Tile a_tileToUpdate)
    {


    }



}
