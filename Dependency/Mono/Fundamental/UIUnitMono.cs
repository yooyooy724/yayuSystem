using R3;
using System;
using UnityEngine;

namespace yayu.UI
{
    public class UIUnitMono : MonoBehaviour
    {
        UIElementContainer container = UIElementContainerAccess.defaultContainer;
        public string UnitsId => UIElementMono.GetId(gameObject.name, "(unit)");
        [SerializeField] UIElementMono[] elements;
        [SerializeField] UIUnitMono[] units;

        IDisposable disposable;

        public void Init() => Init(UnitsId);
        public void InitWithIndex(int index) => Init(UnitsId + "_" + index); // Add this line
        public void InitWithParentId(string parentId) => Init(parentId + "/" + UnitsId); // Add this line
        public void InitWithParentIdAndIndex(string parentId, int index) => Init(parentId + "/" + UnitsId + "_" + index); // Add this line

        void Init(string unitId)
        {
            //Debug.Log("Init " + unitId);
            var d = Disposable.CreateBuilder();
            foreach (var element in elements)
            {
                if (element == null) continue;
                element.parentId = unitId;
                d.Add(UIElementConnection.Connect(element, container));
            }
            foreach (var unit in units)
            {
                unit?.InitWithParentId(unitId);
            }
            disposable = d.Build();
        }

        private void OnDestroy()
        {
            disposable?.Dispose();
        }
    }
}