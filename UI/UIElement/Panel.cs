using System;

namespace yayu.UI
{
    public interface IPanel
    {
        bool isOn { get; set; }
        void Show();
        void Hide();
    }

    public interface IPanelUIAccessible
    {
        bool IsOn();
    }

    public class Panel: UIElement, IPanel, IPanelUIAccessible
    {
        public Panel(bool initialState, string id) : base(id)
        {
            this._isOn = initialState;
        }

        private bool _isOn;
        private Func<bool> isOnFunc;

        public bool isOn 
        {
            get 
            {
                if(isOnFunc == null) return _isOn;
                return isOnFunc();
            }
            set 
            {
                if (isOnFunc != null) YDebugger.Log("Already set delegate");
                _isOn = value;
            }
        }
        public void SetIsOnFunc(Func<bool> func)
        {
            isOnFunc = func;
        }
        public void Show()
        {
            if (isOnFunc != null) YDebugger.Log("Already set delegate");
            isOn = true;
        }
        public void Hide()
        {
            if (isOnFunc != null) YDebugger.Log("Already set delegate");
            isOn = false;
        }
        public bool IsOn() => isOn;
        
    }

}