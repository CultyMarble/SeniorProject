using UnityEngine;

public class InventoryManager : SingletonMonobehaviour<InventoryManager>
{
    [Header("Canvas References:")]
    [SerializeField] private InventorySlotManager towerInventorySlots = default;

    [Header("Prefab Item Inventory:")]
    [SerializeField] private Transform pfItemInventory = default;

    [Header("SO Data List:")]
    [SerializeField] private SOItemInventoryDataList itemList = default;

    //===========================================================================
    private bool AddNewItem(PrefabItemInventory _itemToInsert)
    {
        foreach (Transform _inventorySlotTransform in towerInventorySlots.transform)
        {
            if (_inventorySlotTransform.TryGetComponent(out PrefabInventorySlot _inventorySlot))
            {
                if (_inventorySlot.Empty)
                {
                    towerInventorySlots.PlaceItem(_itemToInsert, _inventorySlot);

                    // Update Item Record of Selected Tower
                    TowerManager.Instance.selectedTower.GetComponent<TowerData>().
                        SetSlotItemInventoryID(_inventorySlotTransform.GetSiblingIndex(), _itemToInsert.GetItemData.GetItemID);

                    return true;
                }
            }
        }

        return false;
    }

    //===========================================================================
    public void AddItemToTowerInventory(int itemID)
    {
        if (TowerManager.Instance.selectedTower == null)
            return;

        // Create New Item
        PrefabItemInventory _itemToAdd = Instantiate(pfItemInventory, towerInventorySlots.transform).GetComponent<PrefabItemInventory>();

        //// Pick Random One Item form Picked List
        //int _itemIndex = Random.Range(0, itemList.list.Count);

        //// Create New Item Data Based on SO parameters
        //_itemToAdd.SetItemData(itemList.list[_itemIndex]);

        // Get Item based on itemID
        foreach (SOItemInventoryData _itemData in itemList.list)
        {
            if (_itemData.GetItemID == itemID)
            {
                // Create New Item Data Based on SO parameters
                _itemToAdd.SetItemData(_itemData);

                break;
            }
        }

        // Add Item Logic
        if (AddNewItem(_itemToAdd))
            return;

        // Destroy Item If Cannot Add
        _itemToAdd.gameObject.SetActive(false);
        Destroy(_itemToAdd.gameObject);
    }

    public void RemoveItemFromTowerInventory(int slotIndex)
    {
        if (towerInventorySlots.transform.GetChild(slotIndex).TryGetComponent(out PrefabInventorySlot _slot))
        {
            if (_slot.Empty)
                return;

            // Get Item from Slot
            foreach (Transform _item in _slot.transform)
            {
                if (_item.GetComponent<PrefabItemInventory>())
                {
                    // Remove Item Effect

                    // Update Item Record of Selected Tower
                    TowerManager.Instance.selectedTower.GetComponent<TowerData>().SetSlotItemInventoryID(_slot.transform.GetSiblingIndex(), 0);

                    // Delete Item
                    _item.gameObject.SetActive(false);
                    Destroy(_item.gameObject);

                    // Set Slot to Empty
                    _slot.SetSlotEmpty(true);

                    break;
                }
            }
        }
    }
}