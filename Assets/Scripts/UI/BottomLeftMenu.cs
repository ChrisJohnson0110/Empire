using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script has all of the needed functions for the bottom left menu within the UI
/// controlling the display of the menu pop ups
/// </summary>
public class BottomLeftMenu : MonoBehaviour
{
    public static BottomLeftMenu instance;

    [SerializeField] private GameObject _buildingsButton;
    [SerializeField] private GameObject _movementButton;
    [SerializeField] private GameObject _explorationButton;
    [SerializeField] private GameObject _combatButton;
    [SerializeField] private GameObject _researchButton;

    private GameObject _lastButton;

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

    private void Start()
    {
        HideMenus();
    }

    public void HideMenus()
    {
        _buildingsButton.gameObject.SetActive(false);
        _movementButton.gameObject.SetActive(false);
        _combatButton.gameObject.SetActive(false);
        _researchButton.gameObject.SetActive(false);
        _explorationButton.gameObject.SetActive(false);
    }
    public void ToggleBuildingsButton()
    {
        DisplayButton(_buildingsButton);
    }
    public void ToggleMovementButton()
    {
        DisplayButton(_movementButton);
    }
    public void ToggleCombatButton()
    {
        DisplayButton(_combatButton);
    }
    public void ToggleResearchButton()
    {
        DisplayButton(_researchButton);
    } 
    public void ToggleExplorationButton()
    {
        DisplayButton(_explorationButton);
    }
    private void DisplayButton(GameObject a_buttonToShow)
    {
        if (_lastButton != null)
        {
            _lastButton.SetActive(false);
        }
        
        if (_lastButton == a_buttonToShow)
        {
            a_buttonToShow.SetActive(false);
            _lastButton = null;
        }
        else
        {
            a_buttonToShow.gameObject.SetActive(true);
            _lastButton = a_buttonToShow;
        }
    }
}
