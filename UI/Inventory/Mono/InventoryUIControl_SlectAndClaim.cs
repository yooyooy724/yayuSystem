using System;
using UnityEngine;
using UniRx;
using yayu.Inventory;
using System.Linq;
using UnityEngine.Assertions.Must;

namespace yayu.UI.Inventory
{
    public class InventoryUIControl_SelectAndClaim : MonoBehaviour
    {
        [Serializable]
        public class ItemDisplayPanelUnit
        {
            public GameObject panel;
            public TEXT nameText;
            public TEXT descriptionText;
        }

        private InventoryControl inventoryControl;
        [SerializeField] private InventoryUI inventoryUI;
        [SerializeField] private ItemDisplayPanelUnit clickUnit, hoverUnit;
        [SerializeField] private UIButtonMono claimButton;

        private Action<IItem> onClaimAction;

        public void Init(IInventory inventory)
        {
            inventoryControl = new InventoryControl(new IInventory[] { inventory }, true, true, true);
            inventoryControl.ObserveEveryValueChanged(_ => _.ClickedSlots.FirstOrDefault())
                .Subscribe(slot =>
                {
                    hoverUnit.panel.SetActive(false);
                    UpdateSlotInfo(slot, clickUnit.nameText, clickUnit.descriptionText, clickUnit.panel, claimButton);
                })
                .AddTo(this);

            inventoryControl.ObserveEveryValueChanged(_ => _.HoveredSlots.FirstOrDefault())
                .Where(_ => _ != inventoryControl.ClickedSlots.FirstOrDefault())
                .Subscribe(slot => UpdateSlotInfo(slot, hoverUnit.nameText, hoverUnit.descriptionText, hoverUnit.panel, null))
                .AddTo(this);

            SetupClaimButton();

            inventoryUI.Init(inventory);
        }

        private void UpdateSlotInfo(ISlot slot, TEXT nameText, TEXT descriptionText, GameObject panel, UIButtonMono button)
        {
            if (slot != null && slot.Item != null)
            {
                nameText.BindTextDelegate(slot.Item.name);
                descriptionText.BindTextDelegate(slot.Item.description);
                panel.SetActive(true);
                if (button != null)
                {
                    button.interactable = true;
                }
            }
            else
            {
                nameText.SetText("");
                descriptionText.SetText("");
                panel.SetActive(false);
                if (button != null)
                {
                    button.interactable = false;
                }
            }
        }

        public void RegisterOnClaimAction(Action<IItem> action)
        {
            onClaimAction = action;
        }

        private void SetupClaimButton()
        {
            claimButton.AddListener_Click(() =>
            {
                var clickedItem = inventoryControl.ClickedSlots.FirstOrDefault()?.Item;
                if (clickedItem != null)
                {
                    onClaimAction?.Invoke(clickedItem);
                }
            });
        }
    }
}