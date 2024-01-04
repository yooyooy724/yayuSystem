using System;
using System.Collections.Generic;
using System.Linq;

namespace yayu.Inventory
{
    public interface IInventory
    {
        int Capacity { get; }
        bool AddItem(IItem item, bool pushOutIfFull = false);
        bool RemoveItem(IItem item);
        IItem FindItem(string id);
        IReadOnlyCollection<ISlot> Slots { get; }
        // ���ɕK�v�ȃ��\�b�h��v���p�e�B������΂����ɒǉ�
    }

    public class Inventory<TSlot> : IInventory where TSlot : ISlot, new()
    {
        private List<ISlot> slots;
        public IReadOnlyCollection<ISlot> Slots => slots;
        public int Capacity { get; private set; }

        public Inventory(int capacity)
        {
            Capacity = capacity;
            slots = new List<ISlot>(capacity);
            for (int i = 0; i < capacity; i++)
            {
                slots.Add(new TSlot());
            }
        }

        public Inventory(int capacity, IItem[] items)
        {
            Capacity = capacity;
            slots = new List<ISlot>(capacity);
            for (int i = 0; i < capacity; i++)
            {
                slots.Add(new TSlot());
                if(i < items.Length)
                    slots[i].AddItem(items[i]);
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
                // �ŏ��̃X���b�g����A�C�e���������o��
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

        public IItem FindItem(string id)
        {
            return slots.Select(s => s.Item).FirstOrDefault(item => item != null && item.id == id);
        }

        // ���̕K�v�ȃ��\�b�h�͂����ɒǉ�
    }

}