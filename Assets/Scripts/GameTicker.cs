using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
