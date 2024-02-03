using UnityEngine;
using UnityEngine.UI;
using System;

namespace yayu.UI
{
    public class UnityToggle : UIToggleMono
    {
        [SerializeField] UIButtonMono button;
        [SerializeField] CanvasGroup canvasGroup;

        public override void OnValueChanged(bool inOn)
        {
            canvasGroup.alpha = inOn ? 1 : 0;
        }
        public override void AddListener_ForChangeValue(Action action)
        {
            button.AddListener_Click(action);
        }
    }
}