namespace yayu.UI
{
    public interface IUIElement
    {
        string id { get; }
        string parentId { set; get; }
        public string Path() => parentId != default ? parentId + "/" + id : id;
        void SetActive(bool isActive);
    }
    internal interface IUIElementUIAccessible
    {
        bool isActive { get; }
    }

    public class UIElement : IUIElement, IUIElementUIAccessible
    {
        public string id { get; }
        public string parentId { set; get; }
        public UIElement(string id)
        {
            this.id = id;
        }
        public bool isActive { get; private set; }
        public void SetActive(bool isActive) => this.isActive = isActive;
    }
}