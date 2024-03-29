using System;
using yayu.Event;

namespace yayu.UI
{
    public interface IToggle
    {
        bool isOn { get; set; }
        void AddListener_OnValueChanged(Action<bool> action);
        void RemoveListener_OnValueChanged(Action<bool> action);
    }

    public interface IToggleUIAccess
    {
        bool IsOn();
        void ChangeValue();
    }

    public interface IToggleStateApplier
    {
        bool OnValueChanged(bool inOn);
        void AddListener_OnActForChangeValue(Action action);

    }

    public class UIToggle : IToggle, IToggleUIAccess
    {
        bool _isOn;
        public bool isOn 
        { 
            get => isOn;
            set 
            {
                if(_isOn != value) onValueChanged?.Invoke(isOn);
                _isOn = value;
            }
        }
        CustomEvent<bool> onValueChanged = new();

        public void AddListener_OnValueChanged(Action<bool> action) => onValueChanged.AddListener(action);
     
        public void RemoveListener_OnValueChanged(Action<bool> action) => onValueChanged.RemoveListener(action);
     
        public void RemoveAllListeners()
        {
            onValueChanged.ClearListener();
        }

        public bool IsOn() => isOn;
        public void ChangeValue()
        {
            isOn = !isOn;
        }
    }

}