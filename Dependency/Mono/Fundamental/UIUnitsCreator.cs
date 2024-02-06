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
        [SerializeField] string unitsId;
        [SerializeField] UnityEvent onCreateComplete;

        private void Start()
        {
            UIUnits units = container.GetElement<UIUnits>(unitsId);
            for (int i = 0; i < units.Count; i++)
            {
                var unit = Instantiate(prefab, parent, false);
                unit.Init(unitsId + "_" + i);
            }
            onCreateComplete.Invoke();
        }
    }
}