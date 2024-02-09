namespace yayu.UI
{
    public interface IUIElement
    {
        string id { get; }
        string parentId { set; get; }
        public string Path() => parentId != default ? parentId + "/" + id : id;
        void SetActive(bool isActive);
    }

    public interface IUIUnit
    {
        UIElementIdentify id { get; }
    }

    public class UIElementIdentify
    {
        string id { get; }
        int index = -1;
        string parentId;
        public UIElementIdentify(string elementId, string parentId = default)
        {
            this.id = elementId;
            this.parentId = parentId;
        }
        public void SetIndex(int index) => this.index = index;
        public void SetParentId(string parentId) => this.parentId = parentId;
        public string Path() => parentId != default ? parentId + "/" + id + (index != -1 ? "_" + index : "") : id + (index != -1 ? "_" + index : "");
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