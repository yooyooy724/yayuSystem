using System;
using UnityEngine;

namespace yayu.UI
{
    public abstract class UITextMono: MonoBehaviour, IText
    {
        public abstract string text { get; set; }
        public abstract void SetText(string text);
        public abstract Color color { get; set; }
        public abstract void SetAlpha(float alpha);
        public abstract void BindTextDelegate(Func<string> textDelegate);
        public abstract void OnReset();
    }
}