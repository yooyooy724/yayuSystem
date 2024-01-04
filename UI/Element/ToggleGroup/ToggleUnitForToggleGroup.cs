using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class ToggleUnitForToggleGroup : MonoBehaviour
{
    [Header("�K�{")]
    [SerializeField] public TOGGLE toggle;
    [Header("�����Ă�����")]
    [SerializeField] public TEXT textField = null;
    Func<string> text;
    public void InitToggle(Action<bool> onChangeIsOn)
    {
        if (toggle == null) Debug.LogWarning("null ����");
        toggle.AddListener_OnValueChanged(onChangeIsOn);
    }
    public void InitTextField(Func<string> text)
    {
        this.text = text;
        if (textField == null) return;
        this.ObserveEveryValueChanged(_ => _.text()).Subscribe(textField.SetText).AddTo(this);
    }
}
