using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

namespace yayu.UI
{
    public class TextedNumberPresenter : MonoBehaviour
    {
        [SerializeField] UITextMono number;
        [SerializeField] UITextMono _label;

        IDisposable disposable_number, disposable_active;

        public void Init(string lable, Func<double> number) => Init(lable, number, NumberFormatter.defaultParams);
        public void Init(string label, Func<double> number, NumberFormatter.Params _params)
        {
            _label.SetText(label);
            disposable_number = this.number.BindNumberDelegate(number, _params);
        }

        public void OnReset()
        {
            disposable_number?.Dispose();
            disposable_active?.Dispose();
            number.SetText("");
            _label.SetText("");
            gameObject.SetActive(true);
        }

        public void SetActiveDelegate(Func<bool> isActive)
        {
            var _isActive = isActive;
            disposable_active = Observable.EveryValueChanged(this, _ => _isActive()).Subscribe(_ => gameObject.SetActive(_));
        }

        private void OnDestroy()
        {
            disposable_number?.Dispose();
            disposable_active?.Dispose();
        }
    }
}