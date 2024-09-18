using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class OverlayUILogic : MonoBehaviour
    {
        private const string MainMenuButtonName = "MainMenuButton";
        private const string OptionsButtonName = "OptionsButton";

        public event EventHandler OptionsButtonPressed;
        protected virtual void OnOptionsButtonPressed()
        {
            OptionsButtonPressed?.Invoke(this, EventArgs.Empty);
        }
        
        private UIDocument _overlayDocument;

        private void OnEnable()
        {
            _overlayDocument = GetComponent<UIDocument>();
            if (_overlayDocument == null)
            {
                Debug.LogError("No UIDocument found on OverlayManager object! Disabling OverlayManager script.");
                enabled = false;
                return;
            }
            _overlayDocument.rootVisualElement.Q<Button>(MainMenuButtonName).clicked += () =>
            {
                Debug.Log("MainMenu button clicked!");
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            };
            
            _overlayDocument.rootVisualElement.Q<Button>(OptionsButtonName).clicked += () =>
            {
                Debug.Log("EditCart button clicked!");
                OnOptionsButtonPressed();
            };
        }    }
}