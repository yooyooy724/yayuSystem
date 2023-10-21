using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Runtime.UIManager.Components;
using Doozy.Runtime.UIManager.Containers;
using UnityEngine.Events;

public class ContainerControlByButtons : MonoBehaviour
{
    [SerializeField] UIButton[] openButtons;
    [SerializeField] UIButton[] closeButtons;

    [SerializeField] UIContainer container;
    private void Start()
    {
        foreach (var button in openButtons)
        {
            button.AddBehaviour(Doozy.Runtime.UIManager.UIBehaviour.Name.PointerClick)
                    .Event.AddListener(container.Show);
        }
        foreach (var button in closeButtons)
        {
            button.AddBehaviour(Doozy.Runtime.UIManager.UIBehaviour.Name.PointerClick)
                    .Event.AddListener(container.Hide);
        }
    }
}
