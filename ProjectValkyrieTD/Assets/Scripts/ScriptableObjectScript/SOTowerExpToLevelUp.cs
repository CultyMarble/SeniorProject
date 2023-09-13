using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Tower Data/TowerExpToLevelUp")]
public class SOTowerExpToLevelUp : ScriptableObject
{
    [SerializeField] private int maxLevel = default;
    [SerializeField] private System.Collections.Generic.List<int> expToLevelUpList = default;

    public int MaxLevel => maxLevel;
    public System.Collections.Generic.List<int> ExpToLevelUpList => expToLevelUpList;
}
