using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// game ticker is responsible for the timing between game ticks
/// each tick is a game turn
/// each tick will increase resources, units movement and decress turn time
/// 
/// currently temp increase with a set tickrate
/// </summary>
public class GameTicker : MonoBehaviour
{
    [SerializeField] float tickRate = 1f; // Tick time
    ResourceManager rm;

    void Start()
    {
        rm = GameObject.FindAnyObjectByType<ResourceManager>();
        InvokeRepeating(nameof(GameTickUpdate), 0f, tickRate);
    }

    void GameTickUpdate()
    {
        //rm.IncreaseResources();

        foreach (Player player in GameObject.FindObjectsOfType<Player>())
        {
            rm.woodOwned += player.playersEmprie.CondensedYieldPerTurn(YieldTypes.yieldTypes.Wood);
        }
        //resources
        //border expansion
        //unit movement
    }
}
