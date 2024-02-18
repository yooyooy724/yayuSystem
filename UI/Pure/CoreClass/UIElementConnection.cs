using System;
using R3;

namespace My.UI
{
    /// <summary>
    /// Domain側がUI側にTriggerの発火を委譲し、Domain側の状態（Interactableなど）をUI側に流す
    /// </summary>
    public class UIElementConnection
    {
        public static IDisposable ConnectUIElement(IUIElementUIAccessible serviceSide, IUIElement clientSide)
        {
            var d1 = Observable.EveryValueChanged(serviceSide, _ => _.isActive).Subscribe(_ => clientSide.SetActive(_));
            return d1;
        }
        public static IDisposable ConnectButton(IButtonUIAccessible button_serviceSide, IButton button_clientSide)
        {
            button_clientSide.AddListener_Click(button_serviceSide.OnClick);
            button_clientSide.AddListener_Enter(button_serviceSide.OnEnter);
            button_clientSide.AddListener_Exit(button_serviceSide.OnExit);
            var d1 = Observable.EveryValueChanged(button_serviceSide, _ => _.IsInteractable()).Subscribe(_ => button_clientSide.interactable = _);
            return d1;
        }
        public static IDisposable ConnectToggle(IToggleUIAccessible toggle_serviceSide, IToggleStateApplier toggle_clientSide)
        {
            toggle_clientSide.AddListener_ForChangeValue(toggle_serviceSide.ChangeValue);
            var d1 = Observable.EveryValueChanged(toggle_serviceSide, _ => _.IsOn()).Subscribe(_ => toggle_clientSide.OnValueChanged(_));
            return d1;
        }
        public static IDisposable ConnectText(ITextUIAccessible text_serviceSide, IText text_clientSide)
        {
            var d1 = Observable.EveryValueChanged(text_serviceSide, _ => _.Txt()).Subscribe(_ => text_clientSide.text = _);
            return d1;
        }
        public static IDisposable ConnectPanel(IPanelUIAccessible panel_serviceSide, IPanel panel_clientSide)
        {
            var d1 = Observable.EveryValueChanged(panel_serviceSide, _ => _.IsOn()).Subscribe(_ => panel_clientSide.isOn = _);
            return d1;
        }
        public static IDisposable ConnectGauge(IGaugeUIAccess gauge_serviceSide, IGauge gauge_clientSide)
        {
            var d1 = Observable.EveryValueChanged(gauge_serviceSide, _ => _.Rate()).Subscribe(_ => gauge_clientSide.rate = _);
            return d1;
        }

        public static IDisposable Connect(IUIElement element, UIElementContainer container) => Connect(element.id.Path(), element, container);
        //public static IDisposable ConnectWithParentId(string parentId, IUIElement element, UIElementContainer container) => Connect(parentId+"/"+element.Path(), element, container);

        public static IDisposable Connect(string path, IUIElement element, UIElementContainer container)
        {
            var s_element = container.GetElement(path);
            if (s_element == null) { YDebugger.Log("Cannot Get Element"); return Disposable.Empty; }
            IDisposable d1 = ConnectUIElement(s_element, element);
            IDisposable d2 = null;
            YDebugger.Log("CONNECT :   " + path);
            switch (element)
            {
                case IButton button:
                    if (s_element is Button) d2 = ConnectButton(s_element as Button, button);
                    else YDebugger.LogError($"Type Not Match: {element.id.Path()} & {s_element.id.Path()}");
                    break;
                case IText text:
                    if (s_element is Text) d2 = ConnectText(s_element as Text, text);
                    else YDebugger.LogError($"Type Not Match: {element.id.Path()} & {s_element.id.Path()}");
                    break;
                case IToggleStateApplier toggle:
                    if (s_element is Toggle dToggle) d2 = ConnectToggle(dToggle, toggle);
                    else YDebugger.LogError($"Type Not Match: {element.id.Path()} & {s_element.id.Path()}");
                    break;
                case IPanel panel:
                    if (s_element is Panel dPanel) d2 = ConnectPanel(dPanel, panel);
                    else YDebugger.LogError($"Type Not Match: {element.id.Path()} & {s_element.id.Path()}");
                    break;
                case IGauge gauge:
                    if (s_element is Gauge dGauge) d2 = ConnectGauge(dGauge, gauge);
                    else YDebugger.LogError($"Type Not Match: {element.id.Path()} & {s_element.id.Path()}");
                    break;
                default:
                    YDebugger.Log($"Unsupported Element Type: {element.id.Path()} & {s_element.id.Path()}");
                    break;
            }
            if(d2 == null) return d1;
            return R3.Disposable.Combine(d1, d2);
        }
    }
}