using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI
{
    [RequireComponent(typeof(Image))]
    public class UnityGauge : UIGaugeMono
    {
        Image image;
        private void Awake()
        {
            image = GetComponent<Image>();
            if (image == null)
            {
                Debug.LogError("Image component is not attached to the GameObject.");
            }
        }
        public override float rate
        {
            get => image.fillAmount;
            set => image.fillAmount = value;
        }
    }
}
