namespace yayu.UI
{
    public interface IUIElement : IUIIdentify
    {
        void SetActive(bool isActive);
    }

    internal interface IUIElementUIAccessible
    {
        bool isActive { get; }
    }

    public class UIElement : IUIElement, IUIElementUIAccessible
    {
        public UIIdentify id { get; }
        public UIElement(string id)
        {
            this.id = new UIIdentify(id);
        }
        public bool isActive { get; private set; }
        public void SetActive(bool isActive) => this.isActive = isActive;
    }
}