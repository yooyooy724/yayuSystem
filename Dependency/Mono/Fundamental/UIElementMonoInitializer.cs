using System;
using UnityEngine;
using R3;

namespace yayu.UI 
{
    public class UIElementInitializer : MonoBehaviour
    {
        UIElementMono[] elements;
        UIElementContainer container = UIElementContainerAccess.defaultContainer;
        IDisposable disposable;
        void Start()
        {
            elements = GetComponents<UIElementMono>();
            var d = Disposable.CreateBuilder();
            foreach (var element in elements)
            {
                d.Add(UIElementConnection.Connect(element, container));
            }
            disposable = d.Build();
        }

        private void OnDestroy()
        {
            disposable?.Dispose();
        }
    }
}