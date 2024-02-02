using System;
using R3;

namespace yayu.UI
{
    /// <summary>
    /// Domain‘¤‚ªUI‘¤‚ÉTrigger‚Ì”­‰Î‚ğˆÏ÷‚µADomain‘¤‚Ìó‘ÔiInteractable‚È‚Çj‚ğUI‘¤‚É—¬‚·
    /// </summary>
    public class UIElementConnection
    {
        public static IDisposable ConnectButton(IButtonUIAccess button_domainSide, IButton button_UISIde)
        {
            button_UISIde.AddListener_Click(button_domainSide.OnClick);
            button_UISIde.AddListener_Enter(button_domainSide.OnEnter);
            button_UISIde.AddListener_Exit(button_domainSide.OnExit);
            var d1 = Observable.EveryValueChanged(button_domainSide, _ => _.IsVisible()).Subscribe(_ => button_UISIde.visible = _);
            var d2 = Observable.EveryValueChanged(button_domainSide, _ => _.IsInteractable()).Subscribe(_ => button_UISIde.interactable = _);
            return Disposable.Combine(d1, d2);
        }
        public static IDisposable ConnectText(IToggleUIAccess toggle_domainSide, IToggleStateApplier toggle_UISIde)
        {
            toggle_UISIde.AddListener_OnActForChangeValue(toggle_domainSide.ChangeValue);
            var d1 = Observable.EveryValueChanged(toggle_domainSide, _ => _.IsOn()).Subscribe(_ => toggle_UISIde.OnValueChanged(_));
            return d1;
        }
        public static IDisposable ConnectText(ITextUIAccess text_domainSide, IText text_uiSide)
        {
            var d1 = Observable.EveryValueChanged(text_domainSide, _ => _.Text()).Subscribe(_ => text_uiSide.text = _);
            return d1;
        }
        public static IDisposable ConnectPanel(IPanelUIAccess panel_domainSide, IPanel panel_uiSIde)
        {
            var d1 = Observable.EveryValueChanged(panel_domainSide, _ => _.IsOn()).Subscribe(_ => panel_uiSIde.isOn = _);
            return d1;
        }
        public static IDisposable ConnectGauge(IGaugeUIAccess gauge_domainSide, IGauge gauge_uiSIde)
        {
            var d1 = Observable.EveryValueChanged(gauge_domainSide, _ => _.Rate()).Subscribe(_ => gauge_uiSIde.rate = _);
            return d1;
        }
    }
}