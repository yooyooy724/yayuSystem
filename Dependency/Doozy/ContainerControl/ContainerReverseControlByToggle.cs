using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Runtime.UIManager.Components;
using Doozy.Runtime.UIManager.Containers;
using System;

public class ContainerReverseControlByToggle : MonoBehaviour
{
    [SerializeField] ContainerAndContainerAndToggle containerAndToggle;
    ContainerAndContainerAndToggle unit => containerAndToggle;
    // Start is called before the first frame update

    private void Start()
    {
        unit.toggle.OnValueChangedCallback.AddListener(OnToggleValueChanged);
    }

    private void OnDestroy()
    {
        unit.toggle.OnValueChangedCallback.RemoveListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool newValue)
    {
        if (unit.toggle == null)
        {
            Debug.LogWarning($"[{nameof(ContainerAndContainerAndToggle)}] OnToggleValueChanged - toggle is null", this);
            return;
        }

        if (newValue)
        {
            ShowContainers(unit.toggleOnContainers);
            HideContainers(unit.toggleOffContainers);
        }
        else
        {
            ShowContainers(unit.toggleOffContainers);
            HideContainers(unit.toggleOnContainers);
        }
    }

    private void ShowContainers(UIContainer[] containers)
    {
        if (containers == null || containers.Length == 0)
        {
            Debug.LogWarning($"[{nameof(ContainerAndContainerAndToggle)}] ShowContainers - containers is null or empty", this);
            return;
        }

        foreach (UIContainer container in containers)
        {
            if (container == null) continue;
            container.Show();
        }
    }

    private void HideContainers(UIContainer[] containers)
    {
        if (containers == null || containers.Length == 0)
        {
            Debug.LogWarning($"[{nameof(ContainerAndContainerAndToggle)}] HideContainers - containers is null or empty", this);
            return;
        }

        foreach (UIContainer container in containers)
        {
            if (container == null) continue;
            container.Hide();
        }
    }
    
}

[Serializable]
public class ContainerAndContainerAndToggle
{
    public UIContainer[] toggleOnContainers;
    public UIContainer[] toggleOffContainers;
    public UIToggle toggle;
}