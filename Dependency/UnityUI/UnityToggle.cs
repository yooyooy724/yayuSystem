using UnityEngine;
using UnityEngine.UI;
using System;

namespace yayu.UI
{
    [RequireComponent(typeof(Toggle))]
    public class UnityToggle : UIToggleMono
    {
        private Toggle _toggle;
        private Toggle toggle
        {
            get => _toggle == null ? _toggle = GetComponent<Toggle>() : _toggle;
        }

        public override bool isOn
        {
            get { return toggle.isOn; }
            set { toggle.isOn = value; }
        }

        Action<bool> action;
        bool isInit = false;

        public override void AddListener_OnValueChanged(Action<bool> action)
        {
            if (!isInit) Init();
            this.action += action;
        }

        public override void RemoveListener_OnValueChanged(Action<bool> action)
        {
            this.action -= action;
        }

        public override void RemoveAllListeners()
        {
            this.action = null;
        }

        private void Init()
        {
            toggle.onValueChanged.AddListener((_) => action?.Invoke(_));
            isInit = true;
        }
    }
}