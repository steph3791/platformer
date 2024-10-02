using System;
using UnityEngine;

namespace UI
{
    public class HomeUIManager : MonoBehaviour
    {
        [SerializeField] private MainMenuUILogic mainMenuPanelPrefab;
        [SerializeField] private OptionsUILogic optionsPanelPrefab;
        private MainMenuUILogic _mainMenuPanel;
        private OptionsUILogic _optionsPanel;

        private void Awake()
        {
            _mainMenuPanel = Instantiate(mainMenuPanelPrefab, transform);
            Debug.Log("Instantiated mainMenu");
            _optionsPanel = Instantiate(optionsPanelPrefab, transform);
        }

        private void Start()
        {
            _optionsPanel.gameObject.SetActive(false);
            _mainMenuPanel.OptionsButtonPressed += OnOptionsButtonPressed;
            _optionsPanel.LeaveOptionsMenu += OnLeaveOptionsMenu;
        }

        private void OnOptionsButtonPressed(object sender, EventArgs e)
        {
            _mainMenuPanel.gameObject.SetActive(false);
            _optionsPanel.gameObject.SetActive(true);
        }

        private void OnLeaveOptionsMenu(object sender, EventArgs e)
        {
            _mainMenuPanel.gameObject.SetActive(true);
            _optionsPanel.gameObject.SetActive(false);
        }
    }
}