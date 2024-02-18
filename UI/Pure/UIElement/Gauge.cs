
using System;
using UnityEngine;

namespace My.UI
{
    public interface IGauge
    {
        float rate { get; set; }
    }
    public interface IGaugeUIAccess
    {
        float Rate();
    }

    public class Gauge: UIElement, IGauge, IGaugeUIAccess
    {
        public Gauge(string id) : base(id) { }

        float _rate;
        Func<float> _rateFunc;
        public float rate 
        {
            get
            {
                if (_rateFunc == null) return _rate;
                return _rateFunc();
            }
            set
            {
                if (_rateFunc != null) YDebugger.LogWarning("already sed func");
                _rate = Mathf.Clamp01(value);
            }
        }
        public void SetRateFunc(Func<float> rateFunc)
        {
            _rateFunc = rateFunc;
        }
        public float Rate() => rate;
    }
}