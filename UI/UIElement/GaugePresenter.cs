using System;
using System.Collections;
using System.Collections.Generic;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace yayu.UI
{
    public class GaugePresenter : MonoBehaviour, IDisposable
    {
        public enum TextKind
        {
            none,
            percent,
        }

        [SerializeField] Image fillImage;
        [SerializeField] UITextMono rateText;
        Func<float> valueDelegate = null;
        IDisposable disposable = null;
        NumberFormatter.Params numberParams = new NumberFormatter.Params { digit = 0, footType = NumberFormatter.FootType.percent };
        public void SetValue(float value, int digit = 0)
        {
            var numParams = digit == 0 ? numberParams : new NumberFormatter.Params { digit = digit, footType = NumberFormatter.FootType.percent };
            fillImage.fillAmount = value;
            rateText?.SetText(NumberFormatter.Text(value * 100f, numberParams));
        }
        public void SetValue(float value, NumberFormatter.Params numberParams)
        {
            fillImage.fillAmount = value;
            rateText?.SetText(NumberFormatter.Text(value * 100f, numberParams));
        }

        public void StartUI(Func<float> valueDelegate, int digit = 0)
        {
            var numParams = digit == 0 ? numberParams : new NumberFormatter.Params { digit = digit, footType = NumberFormatter.FootType.percent };

            this.valueDelegate = valueDelegate;

            if (disposable != null) disposable.Dispose();
            disposable = Observable.EveryValueChanged(this, _ => _.valueDelegate())
                .Subscribe((_) => SetValue(_, numParams));
        }

        public void StartUI(Func<float> rate, Func<double> current, Func<double> goal, int digit = 0)
        {
            var numParams = digit == 0 ? numberParams : new NumberFormatter.Params { digit = digit, footType = NumberFormatter.FootType.percent };

            this.valueDelegate = rate;

            if (disposable != null) disposable.Dispose();
            disposable = Observable.EveryValueChanged(this, _ => _.valueDelegate())
                .Subscribe((_) =>
                {
                    rateText.text = $"{NumberFormatter.Text(current(), NumberFormatter.defaultParams)} / {NumberFormatter.Text(goal(), NumberFormatter.defaultParams)} ({NumberFormatter.Text(_ * 100f, numParams)})";
                    fillImage.fillAmount = _;
                });
        }

        public void Dispose()
        {
            if (disposable != null) disposable.Dispose();
        }

        public void OnReset()
        {
            if (disposable != null) disposable.Dispose();
            fillImage.fillAmount = 0;
            rateText.SetText("");
        }

        private void OnDestroy()
        {
            valueDelegate = null;
            if (disposable != null) disposable.Dispose();
        }
    }
}