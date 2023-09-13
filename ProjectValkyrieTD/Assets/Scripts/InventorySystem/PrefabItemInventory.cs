using UnityEngine;
using UnityEngine.UI;

public class PrefabItemInventory : MonoBehaviour
{
    private SOItemInventoryData itemData = default;
    public SOItemInventoryData GetItemData => itemData;

    //===========================================================================
    private void UpdateItemUIData()
    {
        GetComponent<Image>().sprite = itemData.GetitemIcon;
    }

    //===========================================================================
    public void SetItemData(SOItemInventoryData newData)
    {
        itemData = newData;

        UpdateItemUIData();
    }

    public void ApplyItemStat(TowerController tower)
    {
        tower.GetComponent<TowerData>().UpdatePierceTime(itemData.GetItemPierceModifier);
    }

    public void RemoveItemStat(TowerController tower)
    {
        tower.GetComponent<TowerData>().UpdatePierceTime(-itemData.GetItemPierceModifier);
    }
}