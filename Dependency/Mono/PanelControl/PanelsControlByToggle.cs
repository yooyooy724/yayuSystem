//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using UnityEngine.UI;

//namespace yayu.UI
//{
//    public class PanelsControlByToggle : UIPanelMono
//    {
//        [SerializeField] private PanelToggleGroup toggleGroup;

//        public override bool isOn { get => toggleGroup.toggle.isOn; set => toggleGroup.toggle.isOn = value; }
//        public override void Show() => HandleToggleValueChanged(true);
//        public override void Hide() => HandleToggleValueChanged(false);

//        private void Start()
//        {
//            if (toggleGroup == null || toggleGroup.toggle == null)
//            {
//                Debug.LogError("Toggle group or toggle is not assigned", this);
//                return;
//            }
//            HandleToggleValueChanged(toggleGroup.toggle.isOn);
//            toggleGroup.toggle.AddListener_OnValueChanged(HandleToggleValueChanged);
//        }

//        private void OnDestroy()
//        {
//            if (toggleGroup?.toggle != null)
//            {
//                toggleGroup.toggle.RemoveListener_OnValueChanged(HandleToggleValueChanged);
//            }
//        }

//        private void HandleToggleValueChanged(bool isOn)
//        {
//            if (toggleGroup == null) return;

//            if (toggleGroup.toggle.isOn != isOn) toggleGroup.toggle.isOn = isOn;
//            TogglePanels(toggleGroup.toggleOnPanels, isOn);
//            TogglePanels(toggleGroup.toggleOffPanels, !isOn);

//            YDebugger.Log("isOn", isOn);
//        }

//        private void TogglePanels(UIPanelMono[] panels, bool show)
//        {
//            if (panels == null) return;

//            foreach (UIPanelMono panel in panels)
//            {
//                if (panel != null)
//                {
//                    if (show) panel.Show();
//                    else panel.Hide();
//                }
//            }
//        }
//    }

//    [Serializable]
//    public class PanelToggleGroup
//    {
//        public UIPanelMono[] toggleOnPanels;
//        public UIPanelMono[] toggleOffPanels;
//        public UIToggleMono toggle;
//    }
//}