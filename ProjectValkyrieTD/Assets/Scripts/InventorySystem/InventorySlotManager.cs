using UnityEngine;

public class InventorySlotManager : MonoBehaviour
{
    public void PlaceItem(PrefabItemInventory item, PrefabInventorySlot inventorySlot)
    {
        item.transform.SetParent(inventorySlot.transform);
        item.transform.SetSiblingIndex(0);

        item.GetComponent<DragDrop>().currentSlotParent = inventorySlot.transform;

        item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        inventorySlot.SetSlotEmpty(false);
    }
}