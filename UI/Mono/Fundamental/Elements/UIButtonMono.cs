
using System;
using UnityEngine;

namespace My.UI
{
    /// <summary>
    /// MonoなButtonクラス
    /// IButtonControlを開発環境依存のIButtonに渡す
    /// </summary>
    public abstract class UIButtonMono : UIElementMono, IButton
    {
        public override Type UIAccessible => typeof(IButtonUIAccessible); 
        public abstract bool interactable { get; set; }
        public abstract bool visible { get; set; }
        public abstract void AddListener_Click(Action action);
        public abstract void AddListener_Enter(Action action);
        public abstract void AddListener_Exit(Action action);
        public abstract void RemoveListener_Click(Action action);
        public abstract void RemoveListener_Enter(Action action);
        public abstract void RemoveListener_Exit(Action action);
        public abstract void RemoveAllListeners();
    }
}