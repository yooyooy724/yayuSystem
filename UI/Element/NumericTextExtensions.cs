using System;
using UniRx;
using static NumberFormatter;
using System.Collections.Generic;

public static class NumericTextExtensions
{
    public static void BindNumberDelegate(this TEXT textObj, Func<double> number, ICollection<IDisposable> container) 
        => BindNumberDelegate(textObj, number, defaultParams, container);
    public static void BindNumberDelegate(this TEXT textObj, Func<double> number, Params _params, ICollection<IDisposable> container)
    {
        textObj.ObserveEveryValueChanged(_ => number()).Subscribe(_ => ApplyNumber(textObj, _, _params)).AddTo(container);
    }
    public static void BindNumberDelegate(this TEXT textObj, Func<double> number)
    => BindNumberDelegate(textObj, number, defaultParams);
    public static void BindNumberDelegate(this TEXT textObj, Func<double> number, Params _params)
    {
        textObj.ObserveEveryValueChanged(_ => number()).Subscribe(_ => ApplyNumber(textObj, _, _params)).AddTo(textObj.gameObject);
    }

    public static void SetNumber(this TEXT textObj, double number) => SetNumber(textObj, number, defaultParams);
    public static void SetNumber(this TEXT textObj, double number, Params _params)
    {
        ApplyNumber(textObj, number, _params);
    }

    static void ApplyNumber(TEXT textObj, double number, Params _params)
    {
        textObj.text = Text(number, _params); // You might need to adjust this method call depending on how the Text method works
    }
}