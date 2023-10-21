using Doozy.Runtime.UIManager.Components;
using Doozy.Runtime.UIManager.Containers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainersControlByToggles : MonoBehaviour
{
    [SerializeField] ContainerAndToggle[] pairs;
    void OnEnable()
    {
        for (int i = 0; i < pairs.Length; i++)
        {
            var count1 = i;
            pairs[count1].toggle.OnToggleOnCallback.Event.AddListener(pairs[count1].container.Show);
            pairs[count1].toggle.OnInstantToggleOnCallback.Event.AddListener(pairs[count1].container.InstantShow);
            pairs[count1].toggle.OnToggleOffCallback.Event.AddListener(pairs[count1].container.Hide);
            pairs[count1].toggle.OnInstantToggleOffCallback.Event.AddListener(pairs[count1].container.InstantHide);
            pairs[count1].container.AutoSelectAfterShow = true;
            pairs[count1].container.AutoSelectTarget = pairs[count1].toggle.gameObject;
        }
    }
}


[Serializable]
public class ContainerAndToggle
{
    public UIContainer container; 
    public UIToggle toggle;
}
