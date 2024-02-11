//using System;
//using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.EventSystems;

//public class UnityButton_Collider : BUTTON
//{
//    private EventTrigger eventTrigger;
//    private Collider collider;

//    void Awake()
//    {
//        eventTrigger = gameObject.AddComponent<EventTrigger>();
//        collider = GetComponent<Collider>();

//        if (collider == null)
//        {
//            Debug.LogError("Collider component not found on the GameObject.");
//        }
//    }

//    public override bool interactable
//    {
//        get { return collider.enabled; }
//        set { collider.enabled = value; }
//    }

//    public override bool visible
//    {
//        get { return gameObject.activeSelf; }
//        set { gameObject.SetActive(value); }
//    }

//    private void AddEventTriggerListener(EventTriggerType type, UnityAction action)
//    {
//        EventTrigger.Entry entry = new EventTrigger.Entry();
//        entry.eventID = type;
//        entry.callback.AddListener((data) => { action(); });
//        eventTrigger.triggers.Add(entry);
//    }

//    public override void AddListener_onClick(Action action)
//    {
//        AddEventTriggerListener(EventTriggerType.PointerClick, () => action());
//    }

//    public override void AddListener_onEnter(Action action)
//    {
//        AddEventTriggerListener(EventTriggerType.PointerEnter, () => action());
//    }

//    public override void AddListener_onExit(Action action)
//    {
//        AddEventTriggerListener(EventTriggerType.PointerExit, () => action());
//    }

//    // RemoveListener���\�b�h�́A�K�v�ɉ�����EventTrigger��triggers���X�g����K�؂ȃG���g�����폜����������K�v�ł��B
//    // �����ł́A�ȒP�̂��߂ɋ�̓I�Ȏ����͏ȗ����܂��B

//    public override void RemoveAllListeners()
//    {
//        if (eventTrigger != null)
//        {
//            eventTrigger.triggers.Clear();
//        }
//    }
//}
