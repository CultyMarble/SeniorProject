using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Tower Data/SOCoreAbilitySpriteData")]
public class SOCoreAbilitySpriteData : ScriptableObject
{
    [SerializeField] private Sprite towerHeadD = default;
    [SerializeField] private Sprite towerHeadA = default;
    [SerializeField] private Sprite towerHeadAL = default;
    [SerializeField] private Sprite towerHeadAR = default;
    [SerializeField] private Sprite towerHeadB = default;
    [SerializeField] private Sprite towerHeadBL = default;
    [SerializeField] private Sprite towerHeadBR = default;

    public Sprite TowerHeadD => towerHeadD;
    public Sprite TowerHeadA => towerHeadA;
    public Sprite TowerHeadAL => towerHeadAL;
    public Sprite TowerHeadAR => towerHeadAR;
    public Sprite TowerHeadB => towerHeadB;
    public Sprite TowerHeadBL => towerHeadBL;
    public Sprite TowerHeadBR => towerHeadBR;
}