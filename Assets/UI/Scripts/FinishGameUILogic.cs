using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FinishGameUILogic : MonoBehaviour
{
    private const string ExitToMainMenuButton = "StartButton";
    private const string ExitGameButton = "QuitButton";

    private void OnEnable()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        uiDocument.rootVisualElement.Q<Button>(ExitToMainMenuButton).clicked += () =>
        {
            GameFlowManager.Instance.LoadNextLevel(transform.position);
        };
        uiDocument.rootVisualElement.Q<Button>(ExitGameButton).clicked += () =>
        {
            GameFlowManager.Instance.QuitApplication();
        };
    }
}
