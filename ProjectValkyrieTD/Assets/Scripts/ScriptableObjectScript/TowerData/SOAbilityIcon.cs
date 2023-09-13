using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Tower Data/SOAbilityIcon")]
public class SOAbilityIcon : ScriptableObject
{
    [Header("Tower Ability Icon:")]
    [SerializeField] private Sprite abilityIconD = default;
    [SerializeField] private Sprite abilityIconA = default;
    [SerializeField] private Sprite abilityIconAL = default;
    [SerializeField] private Sprite abilityIconAR = default;
    [SerializeField] private Sprite abilityIconB = default;
    [SerializeField] private Sprite abilityIconBL = default;
    [SerializeField] private Sprite abilityIconBR = default;

    public Sprite AbilityIconD => abilityIconD;
    public Sprite AbilityIconA => abilityIconA;
    public Sprite AbilityIconAL => abilityIconAL;
    public Sprite AbilityIconAR => abilityIconAR;
    public Sprite AbilityIconB => abilityIconB;
    public Sprite AbilityIconBL => abilityIconBL;
    public Sprite AbilityIconBR => abilityIconBR;
}
