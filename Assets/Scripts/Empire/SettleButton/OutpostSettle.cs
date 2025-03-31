using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutpostSettle : MonoBehaviour
{
    public void SettleOutPost()
    {
        Tile targetTile = GameObject.FindAnyObjectByType<GetClicked>().currentlySeleceted.GetComponent<Tile>(); //the clicked tile that will be settled

        Empire empire = targetTile.ownedByXempire = GameObject.FindAnyObjectByType<Player>().playersEmprie;  //assign empire

        targetTile.hasStructure = new Outpost(targetTile, empire); //create new city

        Debug.Log($"now owned by{empire.empireName}");
    }
}
