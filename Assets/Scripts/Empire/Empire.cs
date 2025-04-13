using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// empire script stores infomation about the entire empire
/// all of the things this empire owns and infomation about the empire itself
/// </summary>
public class Empire
{
    public Player playerOwner;

    public string empireName;
    public Material ownedMaterial; //material for land owned by this empire
    public Material buildingMaterial; //material for building owned by this empire
    public List<Structure> structures = new List<Structure>();
    public List<Tile> ownedTiles = new List<Tile>();

    public List<YieldTypes> yeildPerTurn = new List<YieldTypes>();

    // score ?

    // info text?

    //values from owned
    //number of worked resources

    public Empire(Player a_player, string a_empireName, Material a_material)
    {
        playerOwner = a_player;
        a_player.playersEmprie = this;
        
        empireName = a_empireName;
        ownedMaterial = a_material;
    }
 
    //calculate the total yeild per turn of owned tiles
    public void CalculateYieldPerTurn()
    {
        List<YieldTypes> yt = new List<YieldTypes>();
        foreach (Tile t in ownedTiles)
        {
            //base tile
            foreach (YieldTypes y in t.baseTileType.tileYield)
            {
                yt.Add(y);
            }
            //resource
            if (t.hasStructure is City city)
            {
                foreach (YieldTypes y in t.resourceOnTile.tileYieldType)
                {
                    yt.Add(y);
                }
            }
        }

        yeildPerTurn = yt;
    }

    //calculate the amount of a given resource that is earned per turn
    public int CondensedYieldPerTurn(YieldTypes.yieldTypes a_yeildToReturn)
    {
        int yieldAmount = 0;
        if (yeildPerTurn.Count <= 0)
        {
            return yieldAmount;
        }

        foreach (YieldTypes yt in yeildPerTurn)
        {
            if (yt.yieldType == a_yeildToReturn)
            {
                yieldAmount += yt.yieldAmount;
            }
        }

        return yieldAmount;
    }
}
