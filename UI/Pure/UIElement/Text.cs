using Codice.Client.BaseCommands;
using System;
using System.Diagnostics;

namespace My.UI
{
    public interface IText
    {
        string text { get; set; }
    }

    public interface ITextUIAccessible
    {
        string Txt();
    }

    public class Text : UIElement, IText, ITextUIAccessible
    {
        public Text(string id) : base(id) { }

        string _text;
        Func<string> _textFunc;

        public string text 
        {
            get 
            {
                if(_textFunc == null) return _text;
                return _textFunc();
            }
            set 
            { 
                _text = value;
                if (_textFunc != null) YDebugger.LogWarning("already set delegate");
            }
        }
        public string Txt() => text;   
        public void SetTextFunc(Func<string> textFunc)
        {
            _textFunc = textFunc;
        }
    }
}