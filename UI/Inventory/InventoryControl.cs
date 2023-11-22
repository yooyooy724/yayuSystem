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
        private IInventory inventory;
        public List<ISlot> HoveredSlots { get; private set; } = new List<ISlot>();
        public List<ISlot> ClickedSlots { get; private set; } = new List<ISlot>();

        public bool IsSingleHoverEnabled { get; set; }
        public bool IsSingleClickEnabled { get; set; }

        public bool IsHoverExitEnabled { get; set; }

        public InventoryControl(IInventory inventory, bool isSingleHoverEnabled, bool isSingleClickEnabled)
        {
            this.inventory = inventory;
            IsSingleHoverEnabled = isSingleHoverEnabled;
            IsSingleClickEnabled = isSingleClickEnabled;

            foreach (var slot in inventory.Slots)
            {
                slot.OnEnter.AddListener(item => OnSlotEnter(slot));
                slot.OnExit.AddListener(item => OnSlotExit(slot)); // OnExit���X�i�[��ǉ�
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
                // �ǉ���Hover���W�b�N�������Ɏ���
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

        private void OnSlotExit(ISlot slot)
        {
            if (IsHoverExitEnabled)
            {
                slot.isHovered = false;
                if (!HoveredSlots.Contains(slot)) return;
                HoveredSlots.Remove(slot);
                // �ǉ���Exit���W�b�N�������Ɏ���
            }
        }


        private void OnSlotClick(ISlot slot)
        {
            if (slot.Item != null)
            {
                if (IsSingleClickEnabled)
                {
                    ClearClickedSlots();
                }

                if (slot.isClicked)
                {
                    slot.isClicked = false;
                    if (HoveredSlots.Contains(slot))
                        HoveredSlots.Remove(slot);
                }
                else
                {
                    slot.isClicked = true;
                    ClickedSlots.Add(slot);
                }
                // �ǉ���Click���W�b�N�������Ɏ���
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

        public void SwapItems(ISlot targetSlot, SelectionType selectionType)
        {
            List<ISlot> selectedSlots = (selectionType == SelectionType.Hovered) ? HoveredSlots : ClickedSlots;

            if (selectedSlots.Count > 0)
            {
                ISlot firstSelectedSlot = selectedSlots[0];
                SwapItemsBetweenSlots(firstSelectedSlot, targetSlot);
            }
        }

        private void SwapItemsBetweenSlots(ISlot slot1, ISlot slot2)
        {
            IItem temp = slot1.Item;
            slot1.Item = slot2.Item;
            slot2.Item = temp;
        }
    }

}