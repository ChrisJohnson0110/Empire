using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// this script has all of the needed functions for the bottom left menu within the UI
/// holding functions for all of the buttons 
/// </summary>
public class BottomLeftMenu : MonoBehaviour
{
    [SerializeField] private Button _buildingsButton;
    [SerializeField] private Button _explorationButton;
    [SerializeField] private Button _combatButton;
    [SerializeField] private Button _researchButton;

    public void HideMenus()
    {
        _buildingsButton.gameObject.SetActive(false);
        _explorationButton.gameObject.SetActive(false);
        _combatButton.gameObject.SetActive(false);
        _researchButton.gameObject.SetActive(false);
    }

    public void ToggleBuildingsButton()
    {
        _buildingsButton.gameObject.SetActive(!_buildingsButton);
    }
    public void ToggleExplorationButton()
    {
        _explorationButton.gameObject.SetActive(!_explorationButton);
    }
    public void ToggleCombatButton()
    {
        _combatButton.gameObject.SetActive(!_combatButton);
    }
    public void ToggleResearchButton()
    {
        _researchButton.gameObject.SetActive(!_researchButton);
    }

}
