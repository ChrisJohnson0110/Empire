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
    public void SettleOutPost(Tile a_targetTile)
    {
        Empire empire = a_targetTile.ownedByXempire = GameObject.FindAnyObjectByType<Player>().playersEmprie;  //assign empire //TODO get correctplayer

        a_targetTile.hasStructure = new Outpost(a_targetTile, empire); //create new city

        Debug.Log($"now owned by{empire.empireName}");
    }
}
