namespace yayu.UI
{
    public interface IPanel
    {
        bool isOn { get; set; }
        void Show();
        void Hide();
    }

    public interface IPanelUIAccess
    {
        bool IsOn();
    }

    public class UIPanel: IPanel, IPanelUIAccess
    {
        public UIPanel(bool initialState)
        {
            this.isOn = initialState;
        }
        public bool isOn { get; set; }
        public void Show() => isOn = true;
        public void Hide() => isOn = false; 
        public bool IsOn() => isOn;
    }

}