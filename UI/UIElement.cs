namespace yayu.UI
{
    public interface IUIElement
    {
        UIElementIdentify id { get; }
        void SetActive(bool isActive);
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
        public UIElementIdentify id { get; }
        public UIElement(string id)
        {
            this.id = new UIElementIdentify(id);
        }
        public bool isActive { get; private set; }
        public void SetActive(bool isActive) => this.isActive = isActive;
    }

    public interface IUIUnit : IUIElement
    {
    }
    internal interface IUIUnitUIAccessible : IUIElementUIAccessible
    {
    }

    public abstract class UIUnit : IUIUnit, IUIUnitUIAccessible
    {
        public abstract UIElementIdentify id { get; }
        public void SetActive(bool isActive) => this.isActive = isActive;
        public bool isActive { get; private set; }
    }
}