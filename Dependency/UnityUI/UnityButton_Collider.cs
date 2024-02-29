using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace yayu.UI
{
    public class UnityButton_Collider : UIButtonMono
    {
        private EventTrigger eventTrigger;
        //private Collider collider;
        private Collider2D collider2D;
        public UnityEvent onClick = new UnityEvent();
        public UnityEvent onEnter = new UnityEvent();
        public UnityEvent onExit = new UnityEvent();

        void Awake()
        {
            eventTrigger = gameObject.AddComponent<EventTrigger>();
            //collider = GetComponent<Collider>();
            collider2D = GetComponent<Collider2D>();

            if (collider2D == null)
            {
                Debug.LogError("Collider or Collider2D component not found on the GameObject.");
            }

            AddListener_Click(() => onClick.Invoke());
            AddListener_Enter(() => onEnter.Invoke());
            AddListener_Exit(() => onExit.Invoke());
        }

        public override bool interactable
        {
            get
            {
                // Collider2D���l��������Ԏ擾
                //if (collider != null) return collider.enabled;
                if (collider2D != null) return collider2D.enabled;
                return false;
            }
            set
            {
                // Collider��Collider2D�̗L���ɉ����ď�Ԑݒ�
                //if (collider != null) collider.enabled = value;
                if (collider2D != null) collider2D.enabled = value;
            }
        }

        public override bool visible
        {
            get { return gameObject.activeSelf; }
            set { gameObject.SetActive(value); }
        }

        private void AddEventTriggerListener(EventTriggerType type, UnityAction action)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = type;
            entry.callback.AddListener((data) => { action(); });
            eventTrigger.triggers.Add(entry);
        }

        public override void AddListener_Click(Action action)
        {
            AddEventTriggerListener(EventTriggerType.PointerClick, () => action());
        }

        public override void AddListener_Enter(Action action)
        {
            AddEventTriggerListener(EventTriggerType.PointerEnter, () => action());
        }

        public override void AddListener_Exit(Action action)
        {
            AddEventTriggerListener(EventTriggerType.PointerExit, () => action());
        }

        // RemoveListener���\�b�h�̋�̓I�Ȏ����́A���X�i�[�������I�ɊǗ����邽�߂̎d�g�݂�݌v����K�v������܂��B
        public override void RemoveListener_Click(Action action) { }
        public override void RemoveListener_Enter(Action action) { }
        public override void RemoveListener_Exit(Action action) { }

        public override void RemoveAllListeners()
        {
            if (eventTrigger != null)
            {
                eventTrigger.triggers.Clear();
            }
        }
    }
}
