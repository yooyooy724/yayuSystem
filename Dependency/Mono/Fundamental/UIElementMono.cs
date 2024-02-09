using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yayu.UI
{
    public class UIElementMono : MonoBehaviour, IUIElement
    {
        [SerializeField] string _id;
        public string id => _id;
        public string parentId { set; get; }
        public void SetActive(bool isActive) => gameObject.SetActive(isActive);
        public virtual Type UIAccessible => null;
    }
}