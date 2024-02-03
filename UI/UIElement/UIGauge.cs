
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yayu.UI
{
    public interface IGauge
    {
        float rate { get; set; }
    }
    public interface IGaugeUIAccess
    {
        float Rate();
    }

    public class UIGauge: IGauge, IGaugeUIAccess
    {
        float _rate;
        public float rate 
        { 
            get => _rate; 
            set => _rate = Mathf.Clamp01(value);
        }
        public float Rate() => rate;
    }
}