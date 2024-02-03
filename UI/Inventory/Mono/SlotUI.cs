using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yayu.UI;
using System;
using R3;
using UnityEngine.UI;
using yayu.Inventory;

namespace yayu.UI.Inventory
{
    public class SlotUI : MonoBehaviour
    {
        private ISlot slot;
        [SerializeField] private UIButtonMono buttonComponent;
        [SerializeField] private Image iconComponent;
        [SerializeField] private List<GameObject> objectsToEnableOnHovered;
        [SerializeField] private List<GameObject> objectsToDisableOnHovered;
        [SerializeField] private List<GameObject> objectsToEnableOnClicked;
        [SerializeField] private List<GameObject> objectsToDisableOnClicked;
        IDisposable disposable;

        public void InjectSlot(ISlot newSlot)
        {
            slot = newSlot;

            var d1 = Observable.EveryValueChanged(slot, x => x.Item)
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
                });

            var d2 = Observable.EveryValueChanged(slot, x => x.isHovered)
                .Subscribe(UpdateGameObjectsOnHovered);
            var d3 = Observable.EveryValueChanged(slot, x => x.isClicked)
                .Subscribe(UpdateGameObjectsOnClicked);

            disposable = Disposable.Combine(d1, d2, d3);

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
            disposable.Dispose();
        }

    }

}