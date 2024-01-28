
using System;
using UnityEngine;

namespace yayu.UI
{
    /// <summary>
    /// Mono��Button�N���X
    /// IButtonControl���J�����ˑ���IButton�ɓn��
    /// </summary>
    public abstract class UIButtonMono : MonoBehaviour, IButton
    {
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