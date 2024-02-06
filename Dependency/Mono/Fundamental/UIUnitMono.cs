using R3;
using System;
using UnityEngine;

namespace yayu.UI
{
    public class UIUnitMono : MonoBehaviour
    {
        UIElementContainer container = UIElementContainerAccess.defaultContainer;
        [SerializeField] UIElementMono[] elements;

        IDisposable disposable;

        public void Init(string unitId)
        {
            var d = Disposable.CreateBuilder();
            foreach (var element in elements)
            {
                element.parentId = unitId;
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