using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomResource : MonoBehaviour
{
    public static RandomResource instance;
    [SerializeField] private List<Resource> _landResources = new List<Resource>();
    [SerializeField] private List<Resource> _seaResources = new List<Resource>();

    public int _chanceForResourceOnTile = 20;

    private void Awake()
    {
        // Ensure only one instance exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy extra instances
            return;
        }
    }

    public void RandomiseResource(Tile a_tile)
    {
        if (Random.Range(0, 100) < _chanceForResourceOnTile) //chance for resource on tile
        {
            //land
            if (a_tile.baseTileType.baseTileType != BaseTile.BaseTileTypes.ocean)
            {
                 a_tile.resourceOnTile = _landResources[Random.Range(0, _landResources.Count)];
            }
            else //sea
            {
                a_tile.resourceOnTile = _seaResources[Random.Range(0, _seaResources.Count)];
            }
        }
    }


}
