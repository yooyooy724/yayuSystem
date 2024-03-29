using System;
using System.Collections.Generic;
using UnityEngine;
using R3;
using yayu.Inventory;


namespace yayu.UI.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private GameObject slotUIPrefab;
        [SerializeField] private Transform parent;
        [SerializeField] private ReCulcurateLayout reCulcurateLayout;
        private IInventory inventory;
        private List<SlotUI> slotUIs = new List<SlotUI>();
        private IDisposable slotSubscription;

        public void Init(IInventory inventory)
        {
            this.inventory = inventory;
            CreateSlotUIs();

            // UniRxを使用してSlotsの変更を監視し、UIを更新する
            slotSubscription = Observable.EveryValueChanged(inventory, _ => _.Capacity).Subscribe(UpdateUI);
        }

        private void CreateSlotUIs()
        {
            foreach (var slot in inventory.Slots)
            {
                var slotUIInstance = Instantiate(slotUIPrefab, parent).GetComponent<SlotUI>();
                if (slotUIInstance == null)
                {
                    throw new InvalidOperationException("SlotUI component not found on the instantiated prefab.");
                }

                slotUIInstance.InjectSlot(slot);
                slotUIs.Add(slotUIInstance);
            }
        }

        private void UpdateUI(int count)
        {
            ClearSlotUIs();
            CreateSlotUIs();
            reCulcurateLayout.ResizeLayoutAfterFixedFrame(1);
        }

        private void ClearSlotUIs()
        {
            foreach (var slotUI in slotUIs)
            {
                Destroy(slotUI.gameObject);
            }

            slotUIs.Clear();
        }

        private void OnDestroy()
        {
            // オブジェクトが破棄されるときにUniRxのサブスクリプションも解除する
            slotSubscription?.Dispose();
        }
    }

}