using System;
using UnityEngine;
using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Components;

namespace My.UI
{
    [RequireComponent(typeof(Doozy.Runtime.UIManager.Components.UIButton))]
    internal class DoozyButton : UIButtonMono
    {
        Doozy.Runtime.UIManager.Components.UIButton button;
        public Doozy.Runtime.UIManager.Components.UIButton MyButton
        {
            get
            {
                if (button == null) button = GetComponent<Doozy.Runtime.UIManager.Components.UIButton>();
                return button;
            }
        }

        public override bool interactable
        {
            get { return MyButton.interactable; }
            set { MyButton.interactable = value; }
        }

        public override bool visible
        {
            get { return MyButton.gameObject.activeSelf; }
            set { MyButton.gameObject.SetActive(value); }
        }

        public override void AddListener_Click(Action action)
        {
            MyButton.AddBehaviour(UIBehaviour.Name.PointerClick).Event.AddListener(() => action());
        }

        public override void AddListener_Enter(Action action)
        {
            MyButton.AddBehaviour(UIBehaviour.Name.PointerEnter).Event.AddListener(() => action());
        }

        public override void AddListener_Exit(Action action)
        {
            // Assuming UIBehaviour.Name.PointerExit exists. If it doesn't, modify this line accordingly.
            MyButton.AddBehaviour(UIBehaviour.Name.PointerExit).Event.AddListener(() => action());
        }

        public override void RemoveListener_Click(Action action)
        {
            // Assuming you want to remove all listeners for this action. If specific removal is needed, more work is required.
            MyButton.AddBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveAllListeners();
        }

        public override void RemoveListener_Enter(Action action)
        {
            // Assuming you want to remove all listeners for this action. If specific removal is needed, more work is required.
            MyButton.AddBehaviour(UIBehaviour.Name.PointerEnter).Event.RemoveAllListeners();
        }

        public override void RemoveListener_Exit(Action action)
        {
            // Assuming UIBehaviour.Name.PointerExit exists. If it doesn't, or if specific removal is needed, modify this line accordingly.
            MyButton.AddBehaviour(UIBehaviour.Name.PointerExit).Event.RemoveAllListeners();
        }

        public override void RemoveAllListeners()
        {
            MyButton.AddBehaviour(UIBehaviour.Name.PointerClick).Event.RemoveAllListeners();
            MyButton.AddBehaviour(UIBehaviour.Name.PointerEnter).Event.RemoveAllListeners();
            // Also remove listeners for PointerExit if you added them.
            MyButton.AddBehaviour(UIBehaviour.Name.PointerExit).Event.RemoveAllListeners();
        }
    }

    //[RequireComponent(typeof(Button))]
    //public class ButtonManager : MonoBehaviour, IButtonManager
    //{
    //    Button _button;
    //    Button button
    //    {
    //        get
    //        {
    //            if (_button = null) _button = GetComponent<Button>();
    //            return _button;
    //        }
    //    }
    //    EventTrigger _eventTrigger;
    //    EventTrigger eventTrigger
    //    {
    //        get
    //        {
    //            if (_eventTrigger == null)
    //            {
    //                _eventTrigger = gameObject.AddComponent<EventTrigger>();
    //            }
    //            return _eventTrigger;
    //        }
    //    }

    //    public void SetInteractable(bool clickable)
    //    {
    //        button.interactable = clickable;
    //    }
    //    public void AddAction_Click(Action action)
    //    {
    //        button.onClick.AddListener(() => action());
    //    }
    //    public void AddAction_Hover(Action action)
    //    {
    //        // Create a new trigger entry for pointer enter event
    //        EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
    //        pointerEnter.eventID = EventTriggerType.PointerEnter;

    //        // Add a callback to invoke the provided action
    //        pointerEnter.callback.AddListener((eventData) => { action(); });

    //        // Add the trigger entry to the event trigger
    //        eventTrigger.triggers.Add(pointerEnter);

    //        // Create a new trigger entry for pointer exit event
    //        EventTrigger.Entry pointerExit = new EventTrigger.Entry();
    //        pointerExit.eventID = EventTriggerType.PointerExit;

    //        // Add a callback to reset the button state
    //        pointerExit.callback.AddListener((eventData) => { button.OnPointerExit(null); });

    //        // Add the trigger entry to the event trigger
    //        eventTrigger.triggers.Add(pointerExit);
    //    }

    //    // Remove event listener
    //    public void ResetAction()
    //    {
    //        button.onClick.RemoveAllListeners();
    //        eventTrigger.triggers.Clear();
    //    }
    //}
}