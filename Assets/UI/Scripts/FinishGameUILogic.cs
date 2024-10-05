using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FinishGameUILogic : MonoBehaviour
{
    private const string ExitToMainMenuButton = "GoToMainMenuButton";
    private const string ExitGameButton = "QuitGameButton";

    private void OnEnable()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        uiDocument.rootVisualElement.Q<Button>(ExitToMainMenuButton).clicked += () =>
        {
            GameFlowManager.Instance.GoToStartScene();
        };
        uiDocument.rootVisualElement.Q<Button>(ExitGameButton).clicked += () =>
        {
            GameFlowManager.Instance.QuitApplication();
        };
    }
}
