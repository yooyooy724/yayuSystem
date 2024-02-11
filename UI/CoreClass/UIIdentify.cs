namespace yayu.UI
{
    public class UIIdentify
    {
        string id { get; }
        int index = -1;
        string parentId;
        public UIIdentify(string elementId, string parentId = default)
        {
            this.id = elementId;
            this.parentId = parentId;
        }
        public void SetIndex(int index) => this.index = index;
        public void SetParentId(string parentId) => this.parentId = parentId;
        public string Path() => parentId != default ? parentId + "/" + id + (index != -1 ? "_" + index : "") : id + (index != -1 ? "_" + index : "");
    }


    /// <summary>
    /// this is a marker interface for UIElement and UIUnit
    /// </summary>
    public interface IUIIdentify
    {
        UIIdentify id { get; }
    }
}