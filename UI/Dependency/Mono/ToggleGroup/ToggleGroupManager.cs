//using System;
//using System.Linq;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using R3;

//namespace yayu.UI
//{
//    public class ToggleGroupManager : MonoBehaviour
//    {
//        [SerializeField] Transform togglesParent;
//        ToggleUnitForToggleGroup[] units;
//        UIToggleMono[] toggles;
//        [HideInInspector] public int currentIndex;
//        ToggleTextInfo[] toggleTextInfos;
//        IDisposable disposable;

//        public void Init(ToggleTextInfo[] _toggleTextInfos)
//        {
//            this.toggleTextInfos = _toggleTextInfos;
//            int len = toggleTextInfos.Length;
//            //Debug.Log("len: " + len);
//            units = togglesParent.GetComponentsInChildren<ToggleUnitForToggleGroup>();
//            toggles = units.Select(_ => _.toggle).ToArray();
//            if (units.Length < len)
//            {
//                Debug.LogWarning("unit 足りてないが");
//                len = units.Length;
//            }
//            for (int i = 0; i < len; i++)
//            {
//                var info = toggleTextInfos[i];
//                units[i].InitToggle((_) =>
//                {
//                    UpdateIndex();
//                });
//                units[i].InitTextField(info.text);
//                //Debug.Log(info.text());
//            }
//            disposable = Observable.EveryValueChanged(this, _ => _.currentIndex).Subscribe(_ => toggleTextInfos[_].onSelected());
//        }

//        private void UpdateIndex()
//        {
//            for (int i = 0; i < toggles.Length; i++)
//            {
//                if (toggles[i].isOn == true)
//                {
//                    currentIndex = i;
//                    break;
//                }
//            }
//        }

//        private void OnDestroy()
//        {
//            disposable?.Dispose();
//        }
//    }

//    public class ToggleTextInfo
//    {
//        public ToggleTextInfo(Action onSelected, Func<string> text)
//        {
//            this.onSelected = onSelected;
//            this.text = text;
//        }
//        public Action onSelected { get; private set; }
//        public Func<string> text { get; private set; }
//    }
//}