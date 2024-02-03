using Codice.Client.BaseCommands;

namespace yayu.UI
{
    public interface IText
    {
        string text { get; set; }
    }

    public interface ITextUIAccess
    {
        string Text();
    }

    public class UIText : IText, ITextUIAccess
    {
        public string text { get; set; }
        public string Text() => text;   
    }
}