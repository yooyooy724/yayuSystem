using System;
using UniRx;
using static NumberFormatter;
using System.Collections.Generic;

public static class NumericTextExtensions
{
    public static IDisposable BindNumberDelegate(this TEXT textObj, Func<double> number) 
        => BindNumberDelegate(textObj, number, defaultParams);
    public static IDisposable BindNumberDelegate(this TEXT textObj, Func<double> number, Params _params)
    {
        return textObj.ObserveEveryValueChanged(_ => number()).Subscribe(_ => ApplyNumber(textObj, _, _params));
    }
    public static void BindNumberDelegate_SelfDispose(this TEXT textObj, Func<double> number)
        => BindNumberDelegate_SelfDispose(textObj, number, defaultParams);
    public static void BindNumberDelegate_SelfDispose(this TEXT textObj, Func<double> number, Params _params)
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