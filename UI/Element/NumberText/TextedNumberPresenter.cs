using System;
using UnityEngine;

public class TextedNumberPresenter : MonoBehaviour
{
    [SerializeField] TEXT number;
    [SerializeField] TEXT lable;
    public void Init(string lable, Func<double> number) => Init(lable, number, NumberFormatter.defaultParams);
    public void Init(string label, Func<double> number, NumberFormatter.Params _params)
    {
        lable.SetText(label);
        this.number.BindNumberDelegate(number, _params);
    }
}
