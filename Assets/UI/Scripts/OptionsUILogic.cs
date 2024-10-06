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
        
        private int _movementSpeed;

        public event EventHandler LeaveOptionsMenu;
        protected virtual void OnLeaveOptionsButtonPressed()
        {
            LeaveOptionsMenu?.Invoke(this, EventArgs.Empty);
        }

        
        private UIDocument _optionsDocument;
        private void OnEnable()
        {
            //TODO how to access default 
            _movementSpeed = PersistentDataManager.MovementSpeed;

            _optionsDocument = GetComponent<UIDocument>();
            if (_optionsDocument == null)
            {
                Debug.LogError("No UIDocument found on Options object! Disabling Options script.");
                enabled = false;
                return;
            }
            
            SliderInt speedSlider = _optionsDocument.rootVisualElement.Q<SliderInt>(MovementSpeedSliderName);
            speedSlider.value = _movementSpeed;
            speedSlider.RegisterValueChangedCallback(evt =>
            {
                _movementSpeed = evt.newValue;
            });

            _optionsDocument.rootVisualElement.Q<Button>(ConfirmButtonName).clicked += () =>
            {
                PersistentDataManager.MovementSpeed = _movementSpeed;
                OnLeaveOptionsButtonPressed();
            };
            _optionsDocument.rootVisualElement.Q<Button>(CancelButtonName).clicked += OnLeaveOptionsButtonPressed;
            _optionsDocument.rootVisualElement.Q<Button>(ResetButtonName).clicked += () =>
            {
                PersistentDataManager.Reset();
                speedSlider.value = PersistentDataManager.MovementSpeed;
                _movementSpeed = PersistentDataManager.MovementSpeed;
            };
        }
    }
}