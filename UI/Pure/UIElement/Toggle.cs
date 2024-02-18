using R3;
using System;
using My.Event;
    
    namespace My.UI
{
    public interface IToggle
    {
        bool isOn { get; set; }
        void AddListener_OnValueChanged(Action<bool> action);
        void RemoveListener_OnValueChanged(Action<bool> action);
    }

    public interface IToggleUIAccessible
    {
        bool IsOn();
        void ChangeValue();
    }

    public interface IToggleStateApplier
    {
        void OnValueChanged(bool inOn);
        void AddListener_ForChangeValue(Action action);

    }

    public class Toggle : UIElement, IToggle, IToggleUIAccessible
    {
        public Toggle(string id): base(id) { }

        bool _isOn;
        public bool isOn 
        { 
            get => _isOn;
            set 
            {
                if(_isOn != value) onValueChanged?.Invoke(_isOn);
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