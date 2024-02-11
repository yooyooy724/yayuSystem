using R3;
using System;
using UnityEngine;

namespace yayu.UI
{
    public class UIUnitMonoInitialization: MonoBehaviour
    {
        [SerializeField] UIUnitMono unit;

        [Header("Option")]
        [SerializeField] string parentId;
        
        private void Start()
        {
            if(parentId != default) unit.InitWithParentId(parentId);
            else unit.Init();
        }
    }
}