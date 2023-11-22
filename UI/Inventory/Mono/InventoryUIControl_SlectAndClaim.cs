using System;
using UnityEngine;
using UniRx;
using yayu.Inventory;

public class InventoryUIControl_SelectAndClaim : MonoBehaviour
{
    [SerializeField] private InventoryControl inventoryControl;
    [SerializeField] private TEXT clickInfoText;
    [SerializeField] private TEXT hoverInfoText;
    [SerializeField] private GameObject clickPanel;
    [SerializeField] private GameObject hoverPanel;
    [SerializeField] private BUTTON claimButton;

    private Action<IItem> onClaimAction;

    private void Start()
    {
        inventoryControl.ObserveEveryValueChanged(_ => _.ClickedSlots.FirstOrDefault())
            .Subscribe(slot => UpdateInfoTextAndPanel(slot, clickInfoText, clickPanel))
            .AddTo(this);

        inventoryControl.ObserveEveryValueChanged(_ => _.HoveredSlots.FirstOrDefault())
            .Subscribe(slot => UpdateInfoTextAndPanel(slot, hoverInfoText, hoverPanel))
            .AddTo(this);

        SetupClaimButton();
    }

    private void UpdateInfoTextAndPanel(ISlot slot, TEXT infoText, GameObject panel)
    {
        if (slot != null && slot.Item != null)
        {
            infoText.SetText($"Name: {slot.Item.Name}\nDescription: {slot.Item.Description}");
            panel.SetActive(true);
        }
        else
        {
            infoText.SetText("");
            panel.SetActive(false);
        }
    }

    public void RegisterOnClaimAction(Action<IItem> action)
    {
        onClaimAction = action;
    }

    private void SetupClaimButton()
    {
        claimButton.AddListener_onClick(() => {
            var clickedItem = inventoryControl.ClickedSlots.FirstOrDefault()?.Item;
            if (clickedItem != null)
            {
                onClaimAction?.Invoke(clickedItem);
            }
        });
    }
}