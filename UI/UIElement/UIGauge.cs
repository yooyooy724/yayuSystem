
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

    public class UIGauge: UIElement, IGauge, IGaugeUIAccess
    {
        public UIGauge(string id) : base(id) { }

        float _rate;
        public float rate 
        { 
            get => _rate; 
            set => _rate = Mathf.Clamp01(value);
        }
        public float Rate() => rate;
    }
}