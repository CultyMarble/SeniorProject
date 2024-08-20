using UnityEngine;
using UnityEngine.EventSystems;

public class PrefabItemInventoryInteract : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            transform.parent.GetComponent<PrefanInventorySlotButtonController>().SetItemInteractButtonActive(true);
        }
    }
}