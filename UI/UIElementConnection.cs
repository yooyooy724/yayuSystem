using System;
using R3;

namespace yayu.UI
{
    /// <summary>
    /// Domain側がUI側にTriggerの発火を委譲し、Domain側の状態（Interactableなど）をUI側に流す
    /// </summary>
    internal class UIElementConnection
    {
        public static IDisposable ConnectButton(IButtonUIAccessible button_domainSide, IButton button_UISIde)
        {
            button_UISIde.AddListener_Click(button_domainSide.OnClick);
            button_UISIde.AddListener_Enter(button_domainSide.OnEnter);
            button_UISIde.AddListener_Exit(button_domainSide.OnExit);
            var d1 = Observable.EveryValueChanged(button_domainSide, _ => _.IsInteractable()).Subscribe(_ => button_UISIde.interactable = _);
            return d1;
        }
        public static IDisposable ConnectToggle(IToggleUIAccessible toggle_domainSide, IToggleStateApplier toggle_UISIde)
        {
            toggle_UISIde.AddListener_ForChangeValue(toggle_domainSide.ChangeValue);
            var d1 = Observable.EveryValueChanged(toggle_domainSide, _ => _.IsOn()).Subscribe(_ => toggle_UISIde.OnValueChanged(_));
            return d1;
        }
        public static IDisposable ConnectText(ITextUIAccessible text_domainSide, IText text_uiSide)
        {
            var d1 = Observable.EveryValueChanged(text_domainSide, _ => _.Text()).Subscribe(_ => text_uiSide.text = _);
            return d1;
        }
        public static IDisposable ConnectPanel(IPanelUIAccessible panel_domainSide, IPanel panel_uiSIde)
        {
            var d1 = Observable.EveryValueChanged(panel_domainSide, _ => _.IsOn()).Subscribe(_ => panel_uiSIde.isOn = _);
            return d1;
        }
        public static IDisposable ConnectGauge(IGaugeUIAccess gauge_domainSide, IGauge gauge_uiSIde)
        {
            var d1 = Observable.EveryValueChanged(gauge_domainSide, _ => _.Rate()).Subscribe(_ => gauge_uiSIde.rate = _);
            return d1;
        }

        public static IDisposable Connect(IUIElement element, UIElementContainer container)
        {
            switch (element)
            {
                case IButton button:  
                    var dButton = container.GetElement<UIButton>(element.Path());
                    if (dButton != null) return ConnectButton(dButton, button);
                    YDebugger.LogError("Nullった");
                    break;
                case IText text:
                    var dText = container.GetElement<UIText>(element.Path());
                    if (dText != null) return ConnectText(dText, text);
                    YDebugger.LogError("Nullった");
                    break;
                case IToggleStateApplier toggle:
                    var dToggle = container.GetElement<UIToggle>(element.Path());
                    if (dToggle != null) return ConnectToggle(dToggle, toggle);
                    YDebugger.LogError("Nullった");
                    break;
                case IPanel panel:
                    var dPanel = container.GetElement<UIPanel>(element.Path());
                    if (dPanel != null) return ConnectPanel(dPanel, panel);
                    YDebugger.LogError("Nullった");
                    break;
                case IGauge gauge:
                    var dGauge = container.GetElement<UIGauge>(element.Path());
                    if (dGauge != null) return ConnectGauge(dGauge, gauge);
                    YDebugger.LogError("Nullった");
                    break;
                default: 
                    break;
            }
            return Disposable.Empty;
        }
    }
}