using System;
using UnityEngine;

namespace yayu.UI
{
    public abstract class UITextMono: UIElementMono, IText
    {
        public override Type UIAccessible => typeof(ITextUIAccessible);
        public abstract string text { get; set; }
        public abstract void SetText(string text);
        public abstract Color color { get; set; }
        public abstract void SetAlpha(float alpha);
        public abstract void BindTextDelegate(Func<string> textDelegate);
        public abstract void OnReset();
    }
}