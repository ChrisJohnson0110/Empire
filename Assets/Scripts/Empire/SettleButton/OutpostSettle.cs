using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is responsible for the function of creating an outpost
/// it creates the outposts and provideds in the needed perameters
/// it also assigns it to the needed empire
/// </summary>
public class OutpostSettle : MonoBehaviour
{
    public void SettleOutPost(Tile a_targetTile, Empire a_empire)
    {
        a_targetTile.ownedByXempire = a_empire;
        a_targetTile.hasStructure = new Outpost(a_targetTile, a_empire); //create new city

        Debug.Log($"outpost built by by{a_empire.empireName}");
    }
}
