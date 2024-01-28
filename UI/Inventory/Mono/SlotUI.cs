using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yayu.UI;

namespace yayu.UI.Inventory
{
    using System;
    using UniRx;
    using UnityEngine;
    using UnityEngine.UI;
    using yayu.Inventory;

    public class SlotUI : MonoBehaviour
    {
        private ISlot slot;
        [SerializeField] private UIButtonMono buttonComponent;
        [SerializeField] private Image iconComponent;
        [SerializeField] private List<GameObject> objectsToEnableOnHovered;
        [SerializeField] private List<GameObject> objectsToDisableOnHovered;
        [SerializeField] private List<GameObject> objectsToEnableOnClicked;
        [SerializeField] private List<GameObject> objectsToDisableOnClicked;
        public void InjectSlot(ISlot newSlot)
        {
            slot = newSlot;

            slot.ObserveEveryValueChanged(x => x.Item)
                .Subscribe(item =>
                {
                    if (item != null)
                    {
                        UpdateIcon(item);
                    }
                    else
                    {
                        ClearIcon(item);
                    }
                }).AddTo(this);

            slot.ObserveEveryValueChanged(x => x.isHovered)
                .Subscribe(UpdateGameObjectsOnHovered).AddTo(this);
            slot.ObserveEveryValueChanged(x => x.isClicked)
                .Subscribe(UpdateGameObjectsOnClicked).AddTo(this);

            // �{�^���̃C�x���g���X�i�[��ݒ�
            buttonComponent.AddListener_Click(() => slot.OnClick.Invoke(slot.Item));
            buttonComponent.AddListener_Enter(() => slot.OnEnter.Invoke(slot.Item));
            buttonComponent.AddListener_Exit(() => slot.OnExit.Invoke(slot.Item));

            // �{�^���̃C�x���g���X�i�[��ݒ�
            buttonComponent.AddListener_Click(() => YDebugger.Log("OnClick"));
            buttonComponent.AddListener_Enter(() => YDebugger.Log("OnEnter"));
            buttonComponent.AddListener_Exit(() => YDebugger.Log("OnExit"));

        }

        private void UpdateIcon(IItem item)
        {
            if (item != null && !string.IsNullOrEmpty(item.iconPath))
            {
                // �����ŃA�C�e���̃A�C�R����ݒ�
                // ��: Resources����A�C�R����ǂݍ���
                var icon = SpriteManager.GetSprite(item.iconPath);
                iconComponent.sprite = icon;
            }
            iconComponent.enabled = true;
        }

        private void ClearIcon(IItem item)
        {
            iconComponent.sprite = null;
            iconComponent.enabled = false;
        }

        private void UpdateGameObjectsOnHovered(bool isState)
        {
            foreach (var obj in objectsToEnableOnHovered)
            {
                if (obj != null) obj.SetActive(isState);
            }

            foreach (var obj in objectsToDisableOnHovered)
            {
                if (obj != null) obj.SetActive(!isState);
            }
        }

        private void UpdateGameObjectsOnClicked(bool isState)
        {
            foreach (var obj in objectsToEnableOnClicked)
            {
                if (obj != null) obj.SetActive(isState);
            }

            foreach (var obj in objectsToDisableOnClicked)
            {
                if (obj != null) obj.SetActive(!isState);
            }
        }

        private void OnDestroy()
        {
            // ���X�i�[���폜
            buttonComponent.RemoveAllListeners();
        }

    }

}