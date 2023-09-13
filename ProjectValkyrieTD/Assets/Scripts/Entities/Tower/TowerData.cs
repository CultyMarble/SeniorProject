using UnityEngine;

public class TowerData : MonoBehaviour
{
    // INVENTORY SYSTEM
    private int slot1ItemInventoryID = default;
    private int slot2ItemInventoryID = default;
    private int slot3ItemInventoryID = default;

    public int GetSlot1ItemInventoryID => slot1ItemInventoryID;
    public int GetSlot2ItemInventoryID => slot2ItemInventoryID;
    public int GetSlot3ItemInventoryID => slot3ItemInventoryID;

    // Range
    private float range = 10.0f;
    public float Range => range;

    // CoolDown
    private float coolDownModifier = default;
    public float CoolDownModifier => coolDownModifier;

    // Damage
    private float damageModifier = default;
    public float DamageModifier => damageModifier;

    // CritChance CritDamage Modifier
    private float critChanceModifier = default;
    public float CritChanceModifier => critChanceModifier;

    private float critDamageModifier = default;
    public float CriticalDamageModifier => critDamageModifier;

    // Utility
    private int pierceTime = default;
    public int PierceTime => pierceTime;

    //===========================================================================
    public void SetSlotItemInventoryID(int slotIndex, int itemID)
    {
        switch (slotIndex)
        {
            case 0:
                slot1ItemInventoryID = itemID;
                break;
            case 1:
                slot2ItemInventoryID = itemID;
                break;
            case 2:
                slot3ItemInventoryID = itemID;
                break;
            default:
                break;
        }
    }

    //===========================================================================
    public void UpdateCoolDownModifer(float amount)
    {
        coolDownModifier += amount;
    }

    public void UpdateDamageModifier(float amount)
    {
        damageModifier += amount;
    }

    public void UpdateCritChanceModifier(float amount)
    {
        critChanceModifier += amount;
    }

    public void UpdateCritDamageModifier(float amount)
    {
        critDamageModifier += amount;
    }

    public void UpdatePierceTime(int amount)
    {
        pierceTime += amount;
    }
}