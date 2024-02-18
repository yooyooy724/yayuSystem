using UnityEngine;
using UnityEngine.UI;
using System;

namespace My.UI
{
    public class UnityToggle : UIToggleMono
    {
        [SerializeField] UIButtonMono button;
        [SerializeField] GameObject[] onObjects, offObjects;

        public override void OnValueChanged(bool inOn)
        {
            onObjects.ForEach(o => o.SetActive(inOn));
            offObjects.ForEach(o => o.SetActive(!inOn));
        }
        public override void AddListener_ForChangeValue(Action action)
        {
            button.AddListener_Click(action);
        }
    }
}