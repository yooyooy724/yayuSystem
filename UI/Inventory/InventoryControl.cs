using Codice.CM.Common.Encryption;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace yayu.Inventory
{
    public enum SelectionType
    {
        Hovered,
        Clicked
    }
    
    public class InventoryControl
    {
        private Inventory inventory;
        public List<ISlot> HoveredSlots { get; private set; } = new List<ISlot>();
        public List<ISlot> ClickedSlots { get; private set; } = new List<ISlot>();

        public bool IsSingleHoverEnabled { get; set; }
        public bool IsSingleClickEnabled { get; set; }

        public InventoryControl(Inventory inventory, bool isSingleHoverEnabled, bool isSingleClickEnabled)
        {
            this.inventory = inventory;
            IsSingleHoverEnabled = isSingleHoverEnabled;
            IsSingleClickEnabled = isSingleClickEnabled;

            foreach (var slot in inventory.Slots)
            {
                slot.OnEnter.AddListener(item => OnSlotEnter(slot));
                slot.OnClick.AddListener(item => OnSlotClick(slot));
            }
        }

        private void OnSlotEnter(ISlot slot)
        {
            if (slot.Item != null)
            {
                if (IsSingleHoverEnabled)
                {
                    ClearHoveredSlots();
                }
                slot.isHovered = true;
                HoveredSlots.Add(slot);
                // 追加のHoverロジックをここに実装
            }
        }

        public void ClearHoveredSlots()
        {
            foreach (var slot in HoveredSlots)
            {
                slot.isHovered = false;
            }
            HoveredSlots.Clear();
        }

        private void OnSlotClick(ISlot slot)
        {
            if (slot.Item != null)
            {
                if (IsSingleClickEnabled)
                {
                    ClearClickedSlots();
                }
                slot.isClicked = true;
                ClickedSlots.Add(slot);
                // 追加のClickロジックをここに実装
            }
        }

        public void ClearClickedSlots()
        {
            foreach (var slot in ClickedSlots)
            {
                slot.isClicked = false;
            }
            ClickedSlots.Clear();
        }

        public void RemoveSelectedItems(SelectionType selectionType)
        {
            List<ISlot> selectedSlots = (selectionType == SelectionType.Hovered) ? HoveredSlots : ClickedSlots;
            RemoveItems(selectedSlots);

            ClearHoveredSlots();
            ClearClickedSlots();
        }

        public void MoveItems(Inventory targetInventory, SelectionType selectionType)
        {
            List<ISlot> selectedSlots = (selectionType == SelectionType.Hovered) ? HoveredSlots : ClickedSlots;
            RemoveItems(selectedSlots);

            HoveredSlots.Clear();
            ClickedSlots.Clear();
        }

        private void RemoveItems(IEnumerable<ISlot> slots)
        {
            foreach (var slot in slots)
            {
                if (slot.Item != null)
                {
                    slot.isHovered = false;
                    slot.isClicked = false;
                    slot.RemoveItem();
                }
            }
        }
    }

}