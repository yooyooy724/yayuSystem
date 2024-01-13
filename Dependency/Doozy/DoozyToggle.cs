using UnityEngine;
using UnityEngine.UI;
using System;
using Doozy.Runtime.UIManager.Components;

[RequireComponent(typeof(UIToggle))]
public class DoozyToggle : TOGGLE
{
    private UIToggle _toggle;
    private UIToggle toggle
    {
        get => _toggle == null ? _toggle = GetComponent<UIToggle>() : _toggle;
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
        toggle.OnValueChangedCallback.AddListener((_) => action?.Invoke(_));
        isInit = true;
    }
}