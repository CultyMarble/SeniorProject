using UnityEngine;

public class PrefabInventorySlot : MonoBehaviour
{
    private Vector2 inventorySlotPosition = default;
    private bool empty = default;

    public bool Empty => empty;
    public Vector2 InventorySlotPosition => inventorySlotPosition;

    //===========================================================================
    private void Awake()
    {
        empty = true;
        inventorySlotPosition = GetComponent<RectTransform>().anchoredPosition;
    }

    //===========================================================================
    public void SetSlotEmpty(bool newBool)
    {
        empty = newBool;
    }
}