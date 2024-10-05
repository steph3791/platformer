using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseUILogic : MonoBehaviour
{
    private const string ContinueButtonName = "ContinueButton";
    private const string OptionsButtonName = "OptionsButton";
    private const string ExitToMainButton = "ExitToMenuButton";
    private UIDocument _pauseUiDocument;

    public event EventHandler ContinueButtonPressed;

    protected virtual void OnContinueButtonPressed()
    {
        ContinueButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler OptionsButtonPressed;

    protected virtual void OnOptionsButtonPressed()
    {
        OptionsButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    private void OnEnable()
    {
        _pauseUiDocument = GetComponent<UIDocument>();
        if (_pauseUiDocument == null)
        {
            Debug.LogError("Pause UI Document is not found");
            enabled = false;
        }

        _pauseUiDocument.rootVisualElement.Q<Button>(ContinueButtonName).clicked += OnContinueButtonPressed;

        _pauseUiDocument.rootVisualElement.Q<Button>(OptionsButtonName).clicked += OnOptionsButtonPressed;

        _pauseUiDocument.rootVisualElement.Q<Button>(ExitToMainButton).clicked += () =>
        {
            gameObject.SetActive(false);
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        };
    }
}