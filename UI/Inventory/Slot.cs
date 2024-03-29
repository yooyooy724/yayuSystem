using System;
using yayu.Event;

namespace yayu.Inventory
{
    public interface ISlot
    {
        IItem Item { get; set; }
        bool isHovered { get; set; }
        bool isClicked { get; set; }
        CustomEvent<IItem> OnClick { get; }
        CustomEvent<IItem> OnEnter { get; }
        CustomEvent<IItem> OnExit { get; }
        //ISlot Create();
    }

    public class Slot : ISlot
    {
        private IItem item;
        public IItem Item
        {
            get => item;
            set => item = value;
        }
        public bool isHovered { get; set; } = false;
        public bool isClicked { get; set; } = false;
        public CustomEvent<IItem> OnClick => onClick;
        public CustomEvent<IItem> OnEnter => onEnter;
        public CustomEvent<IItem> OnExit => onExit;
        CustomEvent<IItem> onClick = new();
        CustomEvent<IItem> onEnter = new();
        CustomEvent<IItem> onExit = new();
    }

    public class ConstantSlot : ISlot
    {
        private IItem item;
        public IItem Item
        {
            get => item;
            set => item = value;
        }
        public bool isHovered { get; set; } = false;
        public bool isClicked { get; set; } = false;
        public CustomEvent<IItem> OnClick => onClick;
        public CustomEvent<IItem> OnEnter => onEnter;
        public CustomEvent<IItem> OnExit => onExit;
        CustomEvent<IItem> onClick = new();
        CustomEvent<IItem> onEnter = new();
        CustomEvent<IItem> onExit = new();

        public void AddItem(IItem newItem)
        {
            if (Item == null) Item = newItem;
        }

        public void RemoveItem()
        {
        }
    }

}