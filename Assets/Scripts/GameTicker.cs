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
    [SerializeField] float tickRate = 0.5f; // Tick time
    ResourceManager rm;

    void Start()
    {
        rm = GameObject.FindAnyObjectByType<ResourceManager>();
        InvokeRepeating(nameof(GameTickUpdate), 0f, tickRate);
    }

    void GameTickUpdate()
    {
        rm.IncreaseResources();
    }
}
