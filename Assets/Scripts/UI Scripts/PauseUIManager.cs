using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class PauseUIManager : MonoBehaviour
{
    [SerializeField] private PauseUILogic pauseUiLogic;
    [SerializeField] private OptionsUILogic optionsPanelPrefab;
    private PauseUILogic _pauseUILogic;
    private OptionsUILogic _optionsPanel;

    private void Awake()
    {
        _pauseUILogic = Instantiate(pauseUiLogic, transform);
        _optionsPanel = Instantiate(optionsPanelPrefab, transform);

    }

    private void Start()
    {
        _pauseUILogic.gameObject.SetActive(false);
        _optionsPanel.gameObject.SetActive(false);

        _pauseUILogic.ContinueButtonPressed += (sender, args) =>
        {
            _pauseUILogic.gameObject.SetActive(false);
            Time.timeScale = 1f;
        };

        _pauseUILogic.OptionsButtonPressed += (sender, args) =>
        {
            _optionsPanel.gameObject.SetActive(true);
        };
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Pressed Escape");
            Time.timeScale = 0;
            _pauseUILogic.gameObject.SetActive(true);
        }
    }
}
