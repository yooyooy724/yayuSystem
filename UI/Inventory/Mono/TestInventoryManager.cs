using UnityEngine;
using yayu.Inventory;
using System;

namespace yayu.Inventory
{
    public class TestInventoryManager : MonoBehaviour
    {
        [SerializeField] private InventoryUIControl_SelectAndClaim inventoryUIControl;
        [SerializeField] private int inventoryCapacity = 10; // �C���x���g���̗e��
        [SerializeField] private BUTTON addItemButton;

        Inventory inventory;

        void Start()
        {
            // Inventory �C���X�^���X�̍쐬
            inventory = new Inventory(inventoryCapacity);

            // InventoryUIControl_SelectAndClaim �R���|�[�l���g�̏�����
            inventoryUIControl.Init(inventory);

            // �e�X�g�p�A�C�e���̒ǉ�
            addItemButton.AddListener_onClick(AddTestItemsToInventory);
        }

        int i = 0;

        private void AddTestItemsToInventory()
        {
            MockItem item = new MockItem(
                    Guid.NewGuid(),
                    $"Item {i + 1}",
                    "leaf",
                    $"Description for item {i + 1}",
                    DateTime.Now.ToString());

            inventory.AddItem(item);
            i++;
        }
    }
}