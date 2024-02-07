
using Doozy.Editor.UIManager.Drawers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yayu.UI
{
    public interface IGauge
    {
        float rate { get; set; }
    }
    internal interface IGaugeUIAccess
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