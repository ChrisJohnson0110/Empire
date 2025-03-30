using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance; // Singleton instance
    List<Empire> allEmpires = new List<Empire>();

    [SerializeField] Material playerOne;
    [SerializeField] Material playerTwo;
    [SerializeField] Material playerThree;
    [SerializeField] Material playerFour;

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
        allEmpires.Add(new Empire("PlayerOne", playerOne));
        allEmpires.Add(new Empire("PlayerTwo", playerTwo));
        allEmpires.Add(new Empire("PlayerThree", playerThree));
        allEmpires.Add(new Empire("PlayerFour", playerFour));

        GameObject.FindAnyObjectByType<Player>().playersEmprie = allEmpires[0];

    }

    

}
