using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class SceneUIManager : MonoBehaviour
{
    [SerializeField] private OverlayUILogic overlayUILogic;
    [SerializeField] private OptionsUILogic optionsUILogic;
    private OverlayUILogic _overlayPanel;
    private OptionsUILogic _optionsPanel;
    private float _timeScale;

    private void Awake()
    {
        _overlayPanel = Instantiate(overlayUILogic, transform);
        _optionsPanel = Instantiate(optionsUILogic, transform);
    }

    private void Start()
    {
        _optionsPanel.gameObject.SetActive(false);
        _overlayPanel.OptionsButtonPressed += OnOptionsButtonPressed;
        _optionsPanel.LeaveOptionsMenu += OnLeaveOptionsMenu;
    }
        
    private void OnOptionsButtonPressed(object sender, EventArgs e)
    {
        _timeScale = Time.timeScale;
        Time.timeScale = 0;
        _overlayPanel.gameObject.SetActive(false);
        _optionsPanel.gameObject.SetActive(true); 
    }
        
    private void OnLeaveOptionsMenu(object sender, EventArgs e)
    {
        Time.timeScale = _timeScale;
        _overlayPanel.gameObject.SetActive(true);
        _optionsPanel.gameObject.SetActive(false);
    }
}
