using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using System;

namespace yayu.UI
{
    public class ToggleUnitForToggleGroup : MonoBehaviour
    {
        [Header("ïKê{")]
        [SerializeField] public UIToggleMono toggle;
        [Header("ñ≥Ç≠ÇƒÇ‡Ç¢Ç¢")]
        [SerializeField] public UITextMono textField = null;
        Func<string> text;
        IDisposable disposable;
        public void InitToggle(Action<bool> onChangeIsOn)
        {
            if (toggle == null) Debug.LogWarning("null ÇæÇ™");
            toggle.AddListener_OnValueChanged(onChangeIsOn);
        }
        public void InitTextField(Func<string> text)
        {
            this.text = text;
            if (textField == null) return;
            disposable = Observable.EveryValueChanged(this, _ => _.text()).Subscribe(textField.SetText);
        }

        private void OnDestroy()
        {
            disposable?.Dispose();
        }
    }
}