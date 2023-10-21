using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Runtime.UIManager.Components;
using Doozy.Runtime.UIManager.Containers;
using UnityEngine.Events;

public class ContainerControlByButton : MonoBehaviour
{
    enum ContainerControlType
    {
        close,
        open,
        openAndClose
    }
    [SerializeField] ContainerControlType containerControlType;
    [SerializeField] UIButton button;
    [SerializeField] UIContainer container;
    private void Start()
    {
        UnityAction action = containerControlType switch
        {
            ContainerControlType.close => container.Hide,
            ContainerControlType.open => container.Show,
            ContainerControlType.openAndClose => SwitchContainer,
            _ => () => { }
        };
        button.AddBehaviour(Doozy.Runtime.UIManager.UIBehaviour.Name.PointerClick)
                    .Event.AddListener(action);
    }

    private void SwitchContainer()
    {
        if (container.isShowing) container.Hide();
        else container.Show();
    }
}
