using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.UI
{
    public abstract class UIPanelMono : UIElementMono, IPanel
    {
        public override Type UIAccessible => typeof(IPanelUIAccessible);
        public abstract bool isOn { get; set; }
        public abstract void Show();
        public abstract void Hide();
    }
}