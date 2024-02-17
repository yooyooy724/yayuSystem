using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace yayu.UI 
{
    public class UIUnitsMonoCreator : MonoBehaviour
    {
        UIElementContainer container = UIElementContainerAccess.defaultContainer;
        [SerializeField] UIUnitMono prefab;
        [SerializeField] Transform parent;
        [SerializeField] UnityEvent onCreateComplete;

        [Header("Option")]
        [SerializeField] string parentId;
        private void Start()
        {
            UIMonoControlCallBack.instance.RegisterOnInitialize(Initialize);
        }

        void Initialize()
        {
            UIUnits units;
            if (parentId != default) prefab.id.SetParentId(parentId);
            units = container.GetElement<UIUnits>(prefab.id.Path());

            for (int i = 0; i < units.Count; i++)
            {
                var unit = Instantiate(prefab, parent, false);
                if (parentId != default) unit.InitWithParentIdAndIndex(parentId, i);
                else unit.InitWithIndex(i);
            }
            onCreateComplete.Invoke();
        }
    }
}