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

    public class UIPanel: UIElement, IPanel, IPanelUIAccessible
    {
        public UIPanel(bool initialState, string id) : base(id)
        {
            this.isOn = initialState;
        }
        public bool isOn { get; set; }
        public void Show() => isOn = true;
        public void Hide() => isOn = false; 
        public bool IsOn() => isOn;
    }

}