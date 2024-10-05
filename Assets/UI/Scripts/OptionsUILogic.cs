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
        
        private int _previousMovementSpeed;

        public event EventHandler LeaveOptionsMenu;
        protected virtual void OnLeaveOptionsButtonPressed()
        {
            LeaveOptionsMenu?.Invoke(this, EventArgs.Empty);
        }

        
        private UIDocument _optionsDocument;
        private void OnEnable()
        {
            _previousMovementSpeed = PlayerOptionsManager.Instance.MovementSpeed;
            
            _optionsDocument = GetComponent<UIDocument>();
            if (_optionsDocument == null)
            {
                Debug.LogError("No UIDocument found on Options object! Disabling Options script.");
                enabled = false;
                return;
            }
            
            SliderInt speedSlider = _optionsDocument.rootVisualElement.Q<SliderInt>(MovementSpeedSliderName);
            speedSlider.value = PlayerOptionsManager.Instance.MovementSpeed;
            speedSlider.RegisterValueChangedCallback(evt =>
            {
                PlayerOptionsManager.Instance.MovementSpeed = evt.newValue;
            });

            _optionsDocument.rootVisualElement.Q<Button>(ConfirmButtonName).clicked += OnLeaveOptionsButtonPressed;
            _optionsDocument.rootVisualElement.Q<Button>(CancelButtonName).clicked += () =>
            {
                PlayerOptionsManager.Instance.MovementSpeed = _previousMovementSpeed;
                OnLeaveOptionsButtonPressed();
            };
            _optionsDocument.rootVisualElement.Q<Button>(ResetButtonName).clicked += () =>
            {
                PlayerOptionsManager.Instance.Reset();
                speedSlider.value = PlayerOptionsManager.Instance.MovementSpeed;
            };
        }
    }
}