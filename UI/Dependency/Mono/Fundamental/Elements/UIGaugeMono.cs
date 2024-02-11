using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yayu.UI
{
    public abstract class UIGaugeMono : UIElementMono, IGauge
    {
        public override Type UIAccessible => typeof(IGaugeUIAccess);
        public abstract float rate { get; set; }
    }
}