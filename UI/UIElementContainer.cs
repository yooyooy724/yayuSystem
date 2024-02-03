using System;
using System.Collections.Generic;

namespace yayu.UI
{
    public class UIElementContainer
    {
        Dictionary<string, UIElement> elementsDictionary = new Better.Dictionary<string, UIElement>();

        public void Register(UIElement uIElement)
        {
            elementsDictionary.Add((uIElement as IUIElement).Path() , uIElement);
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