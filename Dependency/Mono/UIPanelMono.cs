using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yayu.UI
{
    public abstract class UIPanelMono : MonoBehaviour, IPanel
    {
        public abstract bool isOn { get; set; }
        public abstract void Show();
        public abstract void Hide();
    }
}