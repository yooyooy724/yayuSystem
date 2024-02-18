using UnityEngine;
using UnityEngine.UI;
using System;

namespace My.UI
{
    //[RequireComponent(typeof(UIToggle))]
    //public class DoozyToggle : UIToggleMono
    //{
    //    private Doozy.Runtime.UIManager.Components.UIToggle _toggle;
    //    private Doozy.Runtime.UIManager.Components.UIToggle toggle
    //    {
    //        get => _toggle == null ? _toggle = GetComponent<Doozy.Runtime.UIManager.Components.UIToggle>() : _toggle;
    //    }

    //    public override bool isOn
    //    {
    //        get { return toggle.isOn; }
    //        set { toggle.isOn = value; }
    //    }

    //    Action<bool> action;
    //    bool isInit = false;

    //    public override void AddListener_OnValueChanged(Action<bool> action)
    //    {
    //        if (!isInit) Init();
    //        this.action += action;
    //    }

    //    public override void RemoveListener_OnValueChanged(Action<bool> action)
    //    {
    //        this.action -= action;
    //    }

    //    public override void RemoveAllListeners()
    //    {
    //        this.action = null;
    //    }

    //    public abstract bool OnValueChanged(bool inOn);
    //    public abstract void AddListener_ForChangeValue(Action action)
    //    {

    //    }

    //    private void Init()
    //    {
    //        toggle.OnValueChangedCallback.AddListener((_) => action?.Invoke(_));
    //        isInit = true;
    //    }
    //}
}