using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmSettle : MonoBehaviour
{
    public void SettleFarm(Tile a_targetTile, Empire a_empire)
    {
        a_targetTile.hasStructure = new Farm(a_targetTile, a_empire); //create new city
        Debug.Log($"new farm created by {a_empire.empireName}");
    }

}
