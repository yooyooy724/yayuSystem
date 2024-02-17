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
        Toggle Toggle(string id);
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

        public static Toggle Toggle(string id)
        {
            var toggle = new Toggle(id);
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

        static UIUnits RegisterUnits(Func<int> unitsCount, string id)
        {
            var _units = new UIUnits(unitsCount, id);
            UIElementContainerAccess.defaultContainer.Register(_units);
            return _units;
        }

        public static UIUnits Units<T>(string parentId, string unitId, Func<int, string, T> createUnitByIndexAndParentId, int length) where T : class
        {
            return Units<T>(parentId, unitId, createUnitByIndexAndParentId, length, out _);
        }
        public static UIUnits Units<T>(string parentId, string unitId, Func<int, string, T> createUnitByIndexAndParentId, int length, out T[] units) where T : class
        {
            T[] values = new T[length];
            for (int i = 0; i < length; i++)
            {
                values[i] = createUnitByIndexAndParentId(i, parentId);
            }
            units = values;
            return RegisterUnits(() => values.Length, parentId + "/" + unitId);
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
            return _id;
        }
        public Button Button(string id) => PureUI.Button(ID(id));
        public Toggle Toggle(string id) => PureUI.Toggle(ID(id));
        public Panel Panel(bool initialState, string id) => PureUI.Panel(initialState, ID(id));
        public Gauge Gauge(string id) => PureUI.Gauge(ID(id));
        public Text Text(string id) => PureUI.Text(ID(id));
    }

    public class UIElementContainer
    {
        Dictionary<string, UIElement> elementsDictionary = new Dictionary<string, UIElement>();

        public void Register(UIElement uIElement)
        {
            string path = (uIElement as IUIElement).id.Path();
            elementsDictionary.Add(path, uIElement);
            YDebugger.Log("REGISTER :    " + path);
        }

        public void Register(params UIElement[] uIElements)
        {
            foreach (UIElement uIElement in uIElements)
                elementsDictionary.Add(uIElement.id.Path(), uIElement);
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