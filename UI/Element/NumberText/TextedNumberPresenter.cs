using System;
using UniRx;
using UnityEngine;

public class TextedNumberPresenter : MonoBehaviour
{
    [SerializeField] TEXT number;
    [SerializeField] TEXT _label;

    public void Init(string lable, Func<double> number) => Init(lable, number, NumberFormatter.defaultParams);
    public void Init(string label, Func<double> number, NumberFormatter.Params _params)
    {
        _label.SetText(label);
        this.number.BindNumberDelegate(number, _params);
    }

    public void SetActiveDelegate(Func<bool> isActive)
    {
        var _isActive = isActive;
        this.ObserveEveryValueChanged(_ => _isActive()).Subscribe(_ => gameObject.SetActive(_)).AddTo(this);
    }
}
