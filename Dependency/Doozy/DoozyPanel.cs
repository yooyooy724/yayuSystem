using Doozy.Runtime.UIManager.Containers;
using UnityEngine;

namespace yayu.UI
{
    [RequireComponent(typeof(UIView))]
    public class DoozyPanel : UIPanelMono
    {
        private UIContainer _uiContainer;
        private UIContainer uiContainer
        {
            get
            {
                if (_uiContainer == null)
                {
                    _uiContainer = GetComponent<UIContainer>();
                }
                return _uiContainer;
            }
        }

        public override bool isOn 
        { 
            get => uiContainer.isVisible || uiContainer.isShowing; 
            set 
            {
                if (isOn == value) return;
                if(value) Show();
                else Hide();
            }
        }
        public override void Hide() => uiContainer.Hide();
        public override void Show() => uiContainer.Show();
    }
}
