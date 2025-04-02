using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// script for managing and assigning empires to players
/// will be built on more when netcode added
/// </summary>
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance; // Singleton instance
    private List<Empire> _allEmpires = new List<Empire>(); //all empires in the game

    [SerializeField] private Material _playerOne;
    [SerializeField] private Material _playerTwo;
    [SerializeField] private Material _playerThree;
    [SerializeField] private Material _playerFour;

    [SerializeField] private Material _buildingMaterial;

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

        DontDestroyOnLoad(gameObject); // Optional: persist across scenes
    }

    private void Start()
    {
        _allEmpires.Add(new Empire("PlayerOne", _playerOne));
        _allEmpires.Add(new Empire("PlayerTwo", _playerTwo));
        _allEmpires.Add(new Empire("PlayerThree", _playerThree));
        _allEmpires.Add(new Empire("PlayerFour", _playerFour));
         
        //temp adding city material
        foreach (Empire empire in _allEmpires)
        {
            empire.buildingMaterial = _buildingMaterial;
        }

        GameObject.FindAnyObjectByType<Player>().playersEmprie = _allEmpires[0];

    }

    

}
