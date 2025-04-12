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

    public List<Player> players = new List<Player>();

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
        foreach (Player player in GameObject.FindObjectsOfType<Player>())
        {
            players.Add(player);
        }

        _allEmpires.Add(new Empire(players[0], "PlayerOne", _playerOne));
        //_allEmpires.Add(new Empire(players[0], "PlayerTwo", _playerTwo));
        //_allEmpires.Add(new Empire(players[0], "PlayerThree", _playerThree));
        //_allEmpires.Add(new Empire(players[0], "PlayerFour", _playerFour));
    }

    

}
