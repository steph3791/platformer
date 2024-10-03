using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class HomeUIManager : MonoBehaviour
    {
        [SerializeField] private HomeUILogic homePrefab;
        [SerializeField] private OptionsUILogic optionsPanelPrefab;
        private HomeUILogic _homePanel;
        private OptionsUILogic _optionsPanel;

        private void Awake()
        {
            _homePanel = Instantiate(homePrefab, transform);
            _optionsPanel = Instantiate(optionsPanelPrefab, transform);
        }

        private void Start()
        {
            _optionsPanel.gameObject.SetActive(false);
            _homePanel.OptionsButtonPressed += OnOptionsButtonPressed;
            _optionsPanel.LeaveOptionsMenu += OnLeaveOptionsMenu;
        }

        private void OnOptionsButtonPressed(object sender, EventArgs e)
        {
            _homePanel.gameObject.SetActive(false);
            _optionsPanel.gameObject.SetActive(true);
        }

        private void OnLeaveOptionsMenu(object sender, EventArgs e)
        {
            _homePanel.gameObject.SetActive(true);
            _optionsPanel.gameObject.SetActive(false);
        }
    }
}