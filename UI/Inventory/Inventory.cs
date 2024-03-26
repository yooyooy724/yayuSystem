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
        void Clear();
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
                    slots[i].Item = items[i];
            }
        }

        public bool AddItem(IItem item, bool pushOutIfFull = false)
        {
            if (pushOutIfFull)
            {
                for (int i = slots.Count - 1; i > 0; i--)
                {
                    slots[i].Item = slots[i - 1].Item; // �A�C�e��������O�ɃV�t�g
                }
                slots[0].Item = item; // �ŏ��̃X���b�g�ɐV�����A�C�e����ǉ�
                return true;
            }

            var emptySlot = slots.FirstOrDefault(s => s.Item == null);
            if (emptySlot != null)
            {
                emptySlot.Item = item;
                return true;
            }
            return false;
        }

        public bool RemoveItem(IItem item)
        {
            var slot = slots.FirstOrDefault(s => s.Item != null && s.Item.id == item.id);
            if (slot != null)
            {
                slot.Item = null;
                return true;
            }
            return false;
        }

        public void Clear()
        {
            foreach (var slot in slots)
            {
                slot.Item = null;
            }
        }

        public IItem FindItem(string id)
        {
            return slots.Select(s => s.Item).FirstOrDefault(item => item != null && item.id == id);
        }

        // ���̕K�v�ȃ��\�b�h�͂����ɒǉ�
    }

}