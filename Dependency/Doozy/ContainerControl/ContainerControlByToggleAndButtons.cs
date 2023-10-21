using Doozy.Runtime.UIManager.Components;
using Doozy.Runtime.UIManager.Containers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerControlByToggleAndButtons : MonoBehaviour
{
    [SerializeField] ContainerAndToggleAndButtons[] pairs;
    void Start()
    {
        for (int i = 0; i < pairs.Length; i++)
        {
            var count1 = i;
            pairs[count1].toggle.OnToggleOnCallback.Event.AddListener(pairs[count1].container.Show);
            pairs[count1].toggle.OnInstantToggleOnCallback.Event.AddListener(pairs[count1].container.InstantShow);
            pairs[count1].toggle.OnToggleOffCallback.Event.AddListener(pairs[count1].container.Hide);
            pairs[count1].toggle.OnInstantToggleOffCallback.Event.AddListener(pairs[count1].container.InstantHide);

            for (int j = 0; j < pairs[i].closers.Length; j++)
            {
                var count2 = j;
                if (pairs[count1].closers[count2] == null) break;
                pairs[count1].closers[count2]
                    .AddBehaviour(Doozy.Runtime.UIManager.UIBehaviour.Name.PointerClick)
                    .Event.AddListener(() => { Debug.Log("OSHITA"); pairs[count1].toggle.isOn = false; });
            }
        }
    }
}

[Serializable]
public class ContainerAndToggleAndButtons
{
    public UIContainer container;
    public UIToggle toggle;
    public UIButton[] closers;
}

