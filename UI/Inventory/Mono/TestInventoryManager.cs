using UnityEngine;
using yayu.Inventory;
using System;

namespace yayu.Inventory
{
    public class TestInventoryManager : MonoBehaviour
    {
        [SerializeField] private InventoryUIControl_SelectAndClaim inventoryUIControl;
        [SerializeField] private int inventoryCapacity = 10; // インベントリの容量
        [SerializeField] private BUTTON addItemButton;

        Inventory<Slot> inventory;

        void Start()
        {
            // Inventory インスタンスの作成
            inventory = new Inventory<Slot>(inventoryCapacity);

            // InventoryUIControl_SelectAndClaim コンポーネントの初期化
            inventoryUIControl.Init(inventory);

            // テスト用アイテムの追加
            addItemButton.AddListener_onClick(AddTestItemsToInventory);
        }

        int i = 0;

        private void AddTestItemsToInventory()
        {
            YDebugger.Log("AddTestItemsToInventory");
            MockItem item = new MockItem(
                    Guid.NewGuid().ToString(),
                    $"ATARASHII ITEM {i + 1}",
                    "Leaf",
                    $"Description for item {i + 1}",
                    DateTime.Now.ToString());

            inventory.AddItem(item);
            i++;
        }
    }
}