using UnityEngine;
using My.Inventory;
using System;

namespace My.UI.Inventory
{
    public class TestInventoryManager : MonoBehaviour
    {
        [SerializeField] private InventoryUIControl_SelectAndClaim inventoryUIControl;
        [SerializeField] private int inventoryCapacity = 10; // �C���x���g���̗e��
        [SerializeField] private UIButtonMono addItemButton;

        Inventory<Slot> inventory;

        void Start()
        {
            // Inventory �C���X�^���X�̍쐬
            inventory = new Inventory<Slot>(inventoryCapacity);

            // InventoryUIControl_SelectAndClaim �R���|�[�l���g�̏�����
            inventoryUIControl.Init(inventory);

            // �e�X�g�p�A�C�e���̒ǉ�
            addItemButton.AddListener_Click(AddTestItemsToInventory);
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