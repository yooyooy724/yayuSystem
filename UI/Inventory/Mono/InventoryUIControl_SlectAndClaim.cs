using System;
using UnityEngine;
using yayu.Inventory;

public class InventoryUIControl_SelectAndClaim : MonoBehaviour
{
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private InventoryControl inventoryControl;
    [SerializeField] private TEXT clickInfoText;
    [SerializeField] private TEXT hoverInfoText;
    [SerializeField] private GameObject clickPanel;
    [SerializeField] private GameObject hoverPanel;
    [SerializeField] private BUTTON claimButton;

    private Action<IItem> onClaimAction;

    private void Start()
    {
        SetupSlotInteractions();
        SetupClaimButton();
    }

    private void SetupSlotInteractions()
    {
        foreach (var slotUI in inventoryUI.slotUIs)
        {
            slotUI.onClick += () => UpdateSlotInfo(inventoryControl.ClickedSlots, clickInfoText, clickPanel);
            slotUI.onHover += () => UpdateSlotInfo(inventoryControl.HoveredSlots, hoverInfoText, hoverPanel);
        }
    }

    private void UpdateSlotInfo(List<ISlot> slots, TEXT infoText, GameObject panel)
    {
        if (slots.Count > 0)
        {
            ISlot slot = slots[0];
            infoText.SetText($"Name: {slot.Item.Name}\nDescription: {slot.Item.Description}");
            panel.SetActive(true);
        }
        else
        {
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
            if (inventoryControl.ClickedSlots.Count > 0)
            {
                onClaimAction?.Invoke(inventoryControl.ClickedSlots[0].Item);
            }
        });
    }
}