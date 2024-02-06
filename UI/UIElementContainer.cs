using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace yayu.UI
{
    public static class UIElementContainerAccess
    {
        public static UIElementContainer defaultContainer => main;
        static UIElementContainer main = new UIElementContainer();
    }

    public interface PureUIFactory
    {
        Button Button(string id);
        UIToggle Toggle(string id);
        Panel Panel(bool initialState, string id);
        Gauge Gauge(string id);
        Text Text(string id);
    }

    public static class PureUI // PureUIFactory
    {
        public static Button Button(string id)
        {
            var button = new Button(id);
            UIElementContainerAccess.defaultContainer.Register(button);
            return button;
        }

        public static UIToggle Toggle(string id)
        {
            var toggle = new UIToggle(id);
            UIElementContainerAccess.defaultContainer.Register(toggle);
            return toggle;
        }

        public static Panel Panel(bool initialState, string id)
        {
            var panel = new Panel(initialState, id);
            UIElementContainerAccess.defaultContainer.Register(panel);
            return panel;
        }

        public static Gauge Gauge(string id)
        {
            var gauge = new Gauge(id);
            UIElementContainerAccess.defaultContainer.Register(gauge);
            return gauge;
        }

        public static Text Text(string id)
        {
            var text = new Text(id);
            UIElementContainerAccess.defaultContainer.Register(text);
            return text;
        }

        public static UIUnits Units(Func<int> unitsCount, string id)
        {
            var _units = new UIUnits(unitsCount, id);
            UIElementContainerAccess.defaultContainer.Register(_units);
            return _units;
        }
    }

    public class UnitFactory
    {
        readonly string unitId;
        public UnitFactory(string unitId) 
        {
            this.unitId = unitId;
        }
        public string ID(string id)
        {
            var _id = unitId + "/" + id;
            YDebugger.Log("uf   " + _id);
            return _id;
        }
        public Button Button(string id) => PureUI.Button(ID(id));
        public UIToggle Toggle(string id) => PureUI.Toggle(ID(id));
        public Panel Panel(bool initialState, string id) => PureUI.Panel(initialState, ID(id));
        public Gauge Gauge(string id) => PureUI.Gauge(ID(id));
        public Text Text(string id) => PureUI.Text(ID(id));
    }

    public class UIElementContainer
    {
        Dictionary<string, UIElement> elementsDictionary = new Dictionary<string, UIElement>();

        public void Register(UIElement uIElement)
        {
            string id = (uIElement as IUIElement).Path();
            elementsDictionary.Add(id, uIElement);
            YDebugger.Log("id:    "+id);
        }

        public void Register(params UIElement[] uIElements)
        {
            foreach (UIElement uIElement in uIElements)
                elementsDictionary.Add(uIElement.id, uIElement);
        }

        public UIElement GetElement(string id)
        {
            if (elementsDictionary.TryGetValue(id, out UIElement e))
            {
                return e;
            }
            else
            {
                throw new KeyNotFoundException($"Element with ID '{id}' was not found.");
            }
        }

        public TElement GetElement<TElement>(string id) where TElement : UIElement
        {
            var e = GetElement(id);
            if (e is TElement element)
            {
                return element;
            }
            else
            {
                throw new InvalidCastException($"Requested element with ID '{id}' cannot be cast to {typeof(TElement).Name}.");
            }
        }

    }
}