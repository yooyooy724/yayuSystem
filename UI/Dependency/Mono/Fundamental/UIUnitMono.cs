using R3;
using System;
using UnityEngine;

namespace yayu.UI
{
    public class UIUnitMono : MonoBehaviour, IUIUnit
    {
        UIElementContainer container = UIElementContainerAccess.defaultContainer;
        public string UnitsId
        {
            get
            {
                string name = gameObject.name;
                name = name.Replace("(Clone)", string.Empty);
                string id = UIElementMono.GetId(name, "(unit)");
                return id;
            }
        }

        UIIdentify _id;
        public UIIdentify id
        {
            get
            {
                if (_id == null) _id = new UIIdentify(UnitsId);
                return _id;
            }
        }

        [SerializeField] UIElementMono[] elements;
        [SerializeField] UIUnitMono[] units;

        IDisposable disposable;

        public void Init() => Init(id.Path());
        public void InitWithIndex(int index)
        {
            id.SetIndex(index);
            Init();
        }
        public void InitWithParentId(string parentId)
        {
            id.SetParentId(parentId);
            Init();
        }
        public void InitWithParentIdAndIndex(string parentId, int index)
        {
            id.SetIndex(index);
            id.SetParentId(parentId);
            Init();
        }

        void Init(string path)
        {
            //Debug.Log("Init " + unitId);
            var d = Disposable.CreateBuilder();
            foreach (var element in elements)
            {
                if (element == null) continue;
                element.parentId = path;
                d.Add(UIElementConnection.Connect(element, container));
            }
            foreach (var unit in units)
            {
                unit?.InitWithParentId(path);
            }
            disposable = d.Build();
        }

        private void OnDestroy()
        {
            disposable?.Dispose();
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}