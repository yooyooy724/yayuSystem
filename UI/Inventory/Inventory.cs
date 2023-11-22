using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Graphs;

namespace yayu.Inventory
{
    public interface IInventory
    {
        int Capacity { get; }
        bool AddItem(IItem item, bool pushOutIfFull = false);
        bool RemoveItem(IItem item);
        IItem FindItem(Guid id);
        IEnumerable<ISlot> Slots { get; }
        // 他に必要なメソッドやプロパティがあればここに追加
    }

    public class Inventory : IInventory
    {
        private List<Slot> slots;
        public IEnumerable<ISlot> Slots => slots;
        public int Capacity { get; private set; }

        public Inventory(int capacity)
        {
            Capacity = capacity;
            slots = new List<Slot>(capacity);
            for (int i = 0; i < capacity; i++)
            {
                slots.Add(new Slot());
            }
        }

        public bool AddItem(IItem item, bool pushOutIfFull = false)
        {
            var emptySlot = slots.FirstOrDefault(s => s.Item == null);
            if (emptySlot != null)
            {
                emptySlot.AddItem(item);
                return true;
            }
            else if (pushOutIfFull && slots.Count > 0)
            {
                // 最初のスロットからアイテムを押し出し
                slots[0].RemoveItem();
                slots[0].AddItem(item);
                return true;
            }
            return false;
        }

        public bool RemoveItem(IItem item)
        {
            var slot = slots.FirstOrDefault(s => s.Item != null && s.Item.id == item.id);
            if (slot != null)
            {
                slot.RemoveItem();
                return true;
            }
            return false;
        }

        public IItem FindItem(Guid id)
        {
            return slots.Select(s => s.Item).FirstOrDefault(item => item != null && item.id == id);
        }

        // 他の必要なメソッドはここに追加
    }

}