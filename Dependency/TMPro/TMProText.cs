using UnityEngine;
using TMPro;
using UniRx;
using System;

[RequireComponent(typeof(TextMeshProUGUI))]
internal class TMProText : TEXT
{
    TextMeshProUGUI _textField;
    TextMeshProUGUI textField
    {
        get
        {
            if (_textField == null) _textField = GetComponent<TextMeshProUGUI>();
            return _textField;
        }
    }
    Func<string> textDelegate = null;
    IDisposable disposable = null;
    public override string text
    {
        get => textField.text;
        set => SetText(value);
    }
    public override void SetText(string text)
    {
        if (disposable != null) disposable.Dispose(); //{ Debug.LogWarning("cannot set txt when Observable is active"); return; }
        textField.text = text;
    }

    public override void BindTextDelegate(Func<string> textDelegate)
    {
        this.textDelegate = textDelegate;

        if (disposable != null) disposable.Dispose();
        disposable = this.ObserveEveryValueChanged(_ => _.textDelegate()).Subscribe(_ => textField.text = _);
    }

    public override Color color 
    { 
        get => textField.color;
        set => textField.color = value;
    }

    public override void SetAlpha(float alpha)
    {
        textField.color = new Color(textField.color.r, textField.color.g, textField.color.b, alpha);
    }

    private void OnDestroy()
    {
        textDelegate = null;
        if (disposable != null) disposable.Dispose();
    }

    public override void OnReset()
    {
        textDelegate = null;
        if (disposable != null) disposable.Dispose();
        textField.text = "";
    }
}