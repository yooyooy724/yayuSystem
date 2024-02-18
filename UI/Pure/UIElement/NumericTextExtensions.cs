using System;
using R3;
using static NumberFormatter;

namespace My.UI
{
    public static class NumericTextExtensions
    {
        public static IDisposable BindNumberDelegate(this IText textObj, Func<double> number)
            => BindNumberDelegate(textObj, number, defaultParams);
        public static IDisposable BindNumberDelegate(this IText textObj, Func<double> number, Params _params)
        {
            return Observable.EveryValueChanged(textObj, _ => number()).Subscribe(_ => ApplyNumber(textObj, _, _params));
        }
        public static void BindNumberDelegate_SelfDispose(this IText textObj, Func<double> number, ref IDisposable disposable)
            => BindNumberDelegate_SelfDispose(textObj, number, defaultParams, ref disposable);
        public static void BindNumberDelegate_SelfDispose(this IText textObj, Func<double> number, Params _params, ref IDisposable disposable)
        {
            disposable = Observable.EveryValueChanged(textObj, _ => number()).Subscribe(_ => ApplyNumber(textObj, _, _params));
        }

        public static void SetNumber(this IText textObj, double number) => SetNumber(textObj, number, defaultParams);
        public static void SetNumber(this IText textObj, double number, Params _params)
        {
            ApplyNumber(textObj, number, _params);
        }

        static void ApplyNumber(IText textObj, double number, Params _params)
        {
            textObj.text = Text(number, _params); // You might need to adjust this method call depending on how the Text method works
        }
    }
}