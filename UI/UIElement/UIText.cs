using Codice.Client.BaseCommands;

namespace yayu.UI
{
    public interface IText
    {
        string text { get; set; }
    }

    internal interface ITextUIAccessible
    {
        string Text();
    }

    public class UIText : UIElement, IText, ITextUIAccessible
    {
        public UIText(string id) : base(id) { }
        public string text { get; set; }
        public string Text() => text;   
    }
}