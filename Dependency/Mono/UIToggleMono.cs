using System;
using UnityEngine;

namespace yayu.UI
{
    public abstract class UIToggleMono : MonoBehaviour, IToggle
    {
        public abstract bool isOn { get; set; }
        public abstract void AddListener_OnValueChanged(Action<bool> action);
        public abstract void RemoveListener_OnValueChanged(Action<bool> action);
        public abstract void RemoveAllListeners();
    }
}

//UIElementGroup
//        Count
//        Name
//        Index


//UIElemetConector
//        かたが　Init(string id, string containerId) 内部でContainerを参照


//UIUnitConnector
//    UIElement[]
//        IdにUnitIdを追加しInit

//UIGroup

    

//    bool toggle 

//    float slider

//    panel 


