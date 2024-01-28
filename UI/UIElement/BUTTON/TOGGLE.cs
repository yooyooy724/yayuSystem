using System;
using yayu.Event;

namespace yayu.UI
{
    public interface IToggle: IButton
    {
        bool isOn { get; set; }
        void AddListener_OnValueChanged(Action<bool> action);
        void RemoveListener_OnValueChanged(Action<bool> action);
    }

    public interface IToggleUIAccess: IButtonUIAccess
    {
        bool IsOn();
    }

    public class UIToggle : UIButton, IToggle, IToggleUIAccess
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
     
        public override void RemoveAllListeners()
        {
            base.RemoveAllListeners();
            onValueChanged.ClearListener();
        }

        public bool IsOn() => isOn;
        public override void OnClick()
        {
            base.OnClick();
            isOn = !isOn;
        }
    }

}