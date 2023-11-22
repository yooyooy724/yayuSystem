using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yayu.Inventory
{
    using System;
    using UniRx;
    using UnityEngine;
    using UnityEngine.UI;
    using yayu.Inventory;

    public class SlotUI : MonoBehaviour
    {
        private ISlot slot;
        [SerializeField] private BUTTON buttonComponent;
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

            // ボタンのイベントリスナーを設定
            buttonComponent.AddListener_onClick(() => slot.OnClick.Invoke(slot.Item));
            buttonComponent.AddListener_onEnter(() => slot.OnEnter.Invoke(slot.Item));
            buttonComponent.AddListener_onExit(() => slot.OnExit.Invoke(slot.Item));
        }

        private void UpdateIcon(IItem item)
        {
            if (item != null && !string.IsNullOrEmpty(item.iconPath))
            {
                // ここでアイテムのアイコンを設定
                // 例: Resourcesからアイコンを読み込む
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
            foreach (var obj in objectsToEnableOnHovered)
            {
                if (obj != null) obj.SetActive(isState);
            }

            foreach (var obj in objectsToDisableOnHovered)
            {
                if (obj != null) obj.SetActive(!isState);
            }
        }

        private void OnDestroy()
        {
            // リスナーを削除
            buttonComponent.RemoveAllListeners();
        }

    }

}