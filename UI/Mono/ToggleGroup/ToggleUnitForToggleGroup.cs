//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using R3;
//using System;

//namespace yayu.UI
//{
//    public class ToggleUnitForToggleGroup : MonoBehaviour
//    {
//        [Header("必須")]
//        [SerializeField] public UIToggleMono toggle;
//        [Header("無くてもいい")]
//        [SerializeField] public UITextMono textField = null;
//        Func<string> text;
//        IDisposable disposable;
//        public void InitToggle(Action<bool> onChangeIsOn)
//        {
//            if (toggle == null) Debug.LogWarning("null だが");
//            toggle.AddListener_OnValueChanged(onChangeIsOn);
//        }
//        public void InitTextField(Func<string> text)
//        {
//            this.text = text;
//            if (textField == null) return;
//            disposable = Observable.EveryValueChanged(this, _ => _.text()).Subscribe(textField.SetText);
//        }

//        private void OnDestroy()
//        {
//            disposable?.Dispose();
//        }
//    }
//}