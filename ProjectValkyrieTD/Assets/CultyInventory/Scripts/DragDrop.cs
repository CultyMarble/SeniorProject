using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IInitializePotentialDragHandler
{
    [HideInInspector] public Transform currentSlotParent = default;

    private Canvas canvas = default;
    private Transform towerInventory = default;

    private CanvasGroup canvasGroup = default;
    private RectTransform rectTransform = default;

    private float beginDragAlpha = 0.75f;
    private float endDragAlpha = 1.0f;

    //===========================================================================
    private void Awake()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        towerInventory = GameObject.Find("TowerInventory").transform;

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (currentSlotParent == null)
            currentSlotParent = this.transform.parent;
    }

    //===========================================================================
    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        eventData.useDragThreshold = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = beginDragAlpha;
        canvasGroup.blocksRaycasts = false;

        transform.SetParent(towerInventory);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move item to mouse position
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        if (eventData.pointerCurrentRaycast.gameObject)
            return;

        transform.SetParent(currentSlotParent);
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        canvasGroup.alpha = endDragAlpha;
        canvasGroup.blocksRaycasts = true;

        eventData.pointerDrag = null;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out PrefabInventorySlot slot))
        {// EndDrag on Empty Inventory Slot
            MoveItemToEmptySlot(eventData, slot);
        }
        else if (eventData.pointerCurrentRaycast.gameObject.transform.TryGetComponent(out PrefabItemInventory overlapItem))
        {// EndDrag on Inventory Slot with Item Inventory
            SwapItem(eventData, overlapItem);
        }
        else
        {// EndDrag on nothing
            SetItemParentAndResetPosition(currentSlotParent);
        }

        canvasGroup.alpha = endDragAlpha;
        canvasGroup.blocksRaycasts = true;
    }
    
    //===========================================================================
    private void SetItemParentAndResetPosition(Transform slotParent)
    {
        transform.SetParent(slotParent);
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    private void MoveItemToEmptySlot(PointerEventData eventData, PrefabInventorySlot slot)
    {
        if (slot.Empty)
        {
            currentSlotParent.gameObject.GetComponent<PrefabInventorySlot>().SetSlotEmpty(true);

            currentSlotParent = eventData.pointerCurrentRaycast.gameObject.transform;
            currentSlotParent.gameObject.GetComponent<PrefabInventorySlot>().SetSlotEmpty(false);
        }

        SetItemParentAndResetPosition(currentSlotParent);
    }

    private void SwapItem(PointerEventData eventData, PrefabItemInventory overlapItem)
    {
        Transform _overlapInventorySlot = overlapItem.transform.parent;

        overlapItem.transform.SetParent(currentSlotParent);
        overlapItem.GetComponent<DragDrop>().currentSlotParent = currentSlotParent;
        overlapItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        currentSlotParent = _overlapInventorySlot;
        SetItemParentAndResetPosition(_overlapInventorySlot);
    }
}