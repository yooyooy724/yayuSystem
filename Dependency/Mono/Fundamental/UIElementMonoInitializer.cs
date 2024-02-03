using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yayu.UI 
{
    public class UIElementInitializer : MonoBehaviour
    {
        UIElementMono[] elements;
        UIElementContainer container;
        void Start()
        {
            elements = GetComponents<UIElementMono>();
            foreach (var element in elements)
            {
                UIElementConnection.Connect(element, container);
            }
        }
    }
}