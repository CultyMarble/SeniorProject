using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PrefanInventorySlotButtonController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int slotIndex = default;
    [SerializeField] private Button sellItemButton = default;

    //===========================================================================
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != sellItemButton)
        {
            SetItemInteractButtonActive(false);
        }
    }

    //===========================================================================
    private void Awake()
    {
        sellItemButton.onClick.AddListener(() =>
        {
            InventoryManager.Instance.RemoveItemFromTowerInventory(slotIndex);
            SetItemInteractButtonActive(false);
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetItemInteractButtonActive(false);
        }
    }

    //===========================================================================
    public void SetItemInteractButtonActive(bool newBool)
    {
        sellItemButton.gameObject.SetActive(newBool);
    }
}