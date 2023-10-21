using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Runtime.UIManager.Components;
using Doozy.Runtime.UIManager.Containers;
using UnityEngine.Events;
using UniRx;

public class ContainersSwitchByButton : MonoBehaviour
{
    [SerializeField] int startIndex;
    [SerializeField] int currentIndex;
    [SerializeField] UIButton button;
    [SerializeField] UIContainer[] containers;
    private void Start()
    {
        currentIndex = startIndex >= containers.Length ? containers.Length - 1 : startIndex;
        this.ObserveEveryValueChanged(_ => _.currentIndex).Subscribe(_ => SwitchContainer(_));
        button.AddBehaviour(Doozy.Runtime.UIManager.UIBehaviour.Name.PointerClick)
                    .Event.AddListener(() =>
                    {
                        currentIndex++;
                        currentIndex = currentIndex % containers.Length;
                    });
    }

    private void SwitchContainer(int index)
    {

        for (int i = 0; i < containers.Length; i++)
        {
            if (i == index)
            {
                containers[i].Show();
                continue;
            }
            containers[i].Hide();
        }
    }
}
