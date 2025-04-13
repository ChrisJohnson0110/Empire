using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBaseTile : MonoBehaviour
{
    public static RandomBaseTile instance;

    [SerializeField] private List<BaseTile> _baseTiless = new List<BaseTile>(); //list of base tiles for random hex assignment
    [SerializeField] private TileMaterials _materials; //list of tile materials

    [Header("Perlin Noise Settings")]
    [SerializeField] private float _noiseSeed = -1;
    [SerializeField] private float _noiseFrequency = 100f;
    [SerializeField] private float _noiseThreshhold = 0.5f;

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

    public void RandomiseBaseTile(Tile a_tile, HexRenderer a_hexRenderer)
    {
        if (_noiseSeed == -1)
        {
            _noiseSeed = Random.Range(0, 1000000);
        }

        //caculate if is water based on cube coord applied to perlin noise
        float waterValue = Mathf.PerlinNoise((a_tile.offSetCoord.x + _noiseSeed) / _noiseFrequency, (a_tile.offSetCoord.y + _noiseSeed) / _noiseFrequency);
        bool isWater = waterValue < _noiseThreshhold;

        if (isWater)
        {
            a_tile.baseTileType = _baseTiless[2];
            a_hexRenderer.SetMaterial(_materials.ocean);
        }
        else
        {
            a_tile.baseTileType = _baseTiless[Random.Range(0, _baseTiless.Count - 1)]; //random for now

            //change material based on random basetile given
            if (a_tile.baseTileType.baseTileType == BaseTile.BaseTileTypes.grassland)
            {
                a_hexRenderer.SetMaterial(_materials.grass);
            }
            else if (a_tile.baseTileType.baseTileType == BaseTile.BaseTileTypes.plains)
            {
                a_hexRenderer.SetMaterial(_materials.plains);
            }
            else
            {
                a_hexRenderer.SetMaterial(_materials.unAssigned);
            }

        }
    }
}
