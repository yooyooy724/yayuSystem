using System;
using UnityEngine;

namespace My.UI
{
    public abstract class UIToggleMono : UIElementMono, IToggleStateApplier
    {
        public override Type UIAccessible => typeof(IToggleUIAccessible);
        public abstract void OnValueChanged(bool inOn);
        public abstract void AddListener_ForChangeValue(Action action);
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


