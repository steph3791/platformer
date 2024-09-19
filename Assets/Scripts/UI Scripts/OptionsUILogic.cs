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
        private float _movementSpeed;

        private void OnEnable()
        {
            _optionsDocument = GetComponent<UIDocument>();
            if (_optionsDocument == null)
            {
                Debug.LogError("No UIDocument found on Options object! Disabling Options script.");
                enabled = false;
                return;
            }
            
            SliderInt powerSlider = _optionsDocument.rootVisualElement.Q<SliderInt>(MovementSpeedSliderName);
            _movementSpeed = powerSlider.value;
            powerSlider.RegisterValueChangedCallback(evt => { _movementSpeed = evt.newValue; });

            _optionsDocument.rootVisualElement.Q<Button>(ConfirmButtonName).clicked += () =>
            {
                Debug.Log("Confirm button clicked!");
                Debug.Log("Movement Speed: " + _movementSpeed);
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