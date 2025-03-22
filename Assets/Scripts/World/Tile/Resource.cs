using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource
{
    //bool isHidden; //is the resource shown //client side later maybe
    //Tech techRequiredToSee = new Tech();
    //Tech techRequiredToWork = new Tech();

    //yield
    public enum type
    {
        wheat,
        banana,
        cattle,
        sheep,
        bison,
        deer,
        stone,
        fish,


        horses,
        iron,
        coal,
        oil,
        aluminum,
        uranium,


        Cotton,
        spices,
        sugar,
        furs,
        ivory,
        silk,
        dyes,
        incense,
        wine,
        copper,
        gold,
        silver,
        marble,
        pearls,
        truffles,
        crab,
        salt,
        whales,
        citrus,
        cocoa,
        gems
    }

    public type resourceType;

    public List<YieldTypes> tileYieldType = new List<YieldTypes>();
}
