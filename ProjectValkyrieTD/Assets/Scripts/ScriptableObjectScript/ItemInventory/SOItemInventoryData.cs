using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/SOItemInventoryData")]
public class SOItemInventoryData : ScriptableObject
{
    [Header("Base Data:")]
    [SerializeField] private string itemName = string.Empty;
    [SerializeField] private Sprite itemIcon = null;
    [SerializeField] private int itemID = default;

    public string GetItemName => itemName;
    public Sprite GetitemIcon => itemIcon;
    public int GetItemID => itemID;

    [Header("Item Data:")]
    // CoolDown
    [SerializeField] private float itemCoolDownModifier = default;
    public float GetItemCoolDownModifier => itemCoolDownModifier;

    // Damage
    [SerializeField] private float itemDamageModifier = default;
    public float GetItemDamageModifier => itemDamageModifier;

    // CritChance CritDamage Modifier
    [SerializeField] private float itemCritChanceModifier = default;
    [SerializeField] private float itemCritDamageModifier = default;

    public float GetItemCritChanceModifier => itemCritChanceModifier;
    public float GetItemCriticalDamageModifier => itemCritDamageModifier;

    // Utility
    [SerializeField] private int itemPierceModifier = default;
    public int GetItemPierceModifier => itemPierceModifier;
}