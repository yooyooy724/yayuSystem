using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GaugePresenter : MonoBehaviour
{
    public enum TextKind
    {
        none,
        percent, 
    }

    [SerializeField] Image fillImage;
    [SerializeField] TEXT rateText;
    Func<float> valueDelegate = null;
    IDisposable disposable = null;
    NumberFormatter.Params numberParams = new NumberFormatter.Params { digit = 0, footType = NumberFormatter.FootType.percent };
    public void SetValue(float value)
    {
        fillImage.fillAmount = value;
        rateText?.SetText(NumberFormatter.Text(value * 100f, numberParams));
    }

    public void StartUI(Func<float> valueDelegate)
    {
        this.valueDelegate = valueDelegate;

        if (disposable != null) disposable.Dispose();
        disposable = this.ObserveEveryValueChanged(_ => _.valueDelegate())
            .Subscribe(SetValue);
    }

    public void Dispose()
    {
        if (disposable != null) disposable.Dispose();
    }

    private void OnDestroy()
    {
        valueDelegate = null;
        if (disposable != null) disposable.Dispose();
    }
}
