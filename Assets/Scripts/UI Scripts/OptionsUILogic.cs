using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class OptionsUILogic : MonoBehaviour
    {
        private const string CancelButtonName = "CancelButton";
        private const string ConfirmButtonName = "ConfirmButton";
        private const string ResetButtonName = "ResetButton";
        private const string MovementSpeedSliderName = "SpeedSlider";

        public event EventHandler LeaveOptionsMenu;
        protected virtual void OnLeaveOptionsButtonPressed()
        {
            LeaveOptionsMenu?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ResetButtonPressed;
        protected virtual void OnResetButtonPressed()
        {
            ResetButtonPressed?.Invoke(this, EventArgs.Empty);
        }

        
        private UIDocument _optionsDocument;
        private void OnEnable()
        {
            _optionsDocument = GetComponent<UIDocument>();
            if (_optionsDocument == null)
            {
                Debug.LogError("No UIDocument found on Options object! Disabling Options script.");
                enabled = false;
                return;
            }
            
            SliderInt speedSlider = _optionsDocument.rootVisualElement.Q<SliderInt>(MovementSpeedSliderName);
            speedSlider.value = OptionsManager.Instance.MovementSpeed;
            speedSlider.RegisterValueChangedCallback(evt =>
            {
                OptionsManager.Instance.MovementSpeed = evt.newValue;
            });

            _optionsDocument.rootVisualElement.Q<Button>(ConfirmButtonName).clicked += () =>
            {
                Debug.Log("Confirm button clicked!");
                Debug.Log("Movement Speed: " + OptionsManager.Instance.MovementSpeed);
                OnLeaveOptionsButtonPressed();
            };
            _optionsDocument.rootVisualElement.Q<Button>(CancelButtonName).clicked += () =>
            {
                Debug.Log("Cancel button clicked!");
                OnLeaveOptionsButtonPressed();
            };
            _optionsDocument.rootVisualElement.Q<Button>(ResetButtonName).clicked += () =>
            {
                Debug.Log("Reset button clicked!");
                OnResetButtonPressed();
            };
        }
    }
}