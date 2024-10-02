using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class HomeUILogic : MonoBehaviour
{
    private const string StartButtonName = "StartButton";
    private const string ExitButtonName = "QuitButton";
    private const string OptionsButtonName = "OptionsButton";

    public event EventHandler OptionsButtonPressed;

    protected virtual void OnOptionsButtonPressed()
    {
        OptionsButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    private UIDocument _mainMenuUIDocument;

    private void OnEnable()
    {
        _mainMenuUIDocument = GetComponent<UIDocument>();
        if (_mainMenuUIDocument == null)
        {
            Debug.LogError("Main Menu UI Document is not found");
            enabled = false;
        }

        _mainMenuUIDocument.rootVisualElement.Q<Button>(StartButtonName).clicked += () =>
        {
            Debug.Log("Start Button was clicked");
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        };

        _mainMenuUIDocument.rootVisualElement.Q<Button>(OptionsButtonName).clicked += () =>
        {
            Debug.Log("Options Button was clicked");
            OnOptionsButtonPressed();
        };

        _mainMenuUIDocument.rootVisualElement.Q<Button>(ExitButtonName).clicked += () =>
        {
            Debug.Log("Exit Button was clicked");
#if !UNITY_EDITOR
                        Application.Quit();
#elif UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif
        };
    }
}