using System;
using UnityEngine;
using R3;
using yayu.Event;

namespace yayu.UI 
{
    public class UIMonoControlCallBack
    {
        public static UIMonoControlCallBack instance = new();

        public UIMonoControlCallBack()
        {
            onInitialize.AddListener(() => isInitialized = true);
        }
        bool isInitialized = false;
        CustomEvent onInitialize = new();
        public void RegisterOnInitialize(Action action)
        {
            if (isInitialized) action();
            else onInitialize.AddListener(action);
        }
        public void Initialize()
        {
            onInitialize.Invoke();
        }
    }

    public class UIElementInitializer : MonoBehaviour
    {
        UIElementMono[] elements;
        UIElementContainer container = UIElementContainerAccess.defaultContainer; // method injection
        IDisposable disposable;
        void Start()
        {
            elements = GetComponents<UIElementMono>();
            // dictionary init flag
            UIMonoControlCallBack.instance.RegisterOnInitialize(Initialize);
        }

        void Initialize()
        {
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