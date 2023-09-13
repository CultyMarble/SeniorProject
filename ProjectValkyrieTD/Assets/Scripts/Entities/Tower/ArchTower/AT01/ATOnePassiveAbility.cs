using UnityEngine;

public class ATOnePassiveAbility : Ability
{
    // Havoc Stack
    private readonly float havocDuration = 4.0f;
    private float havocDurationTimer = default;

    private readonly int havocMaxStackAmount = 10;
    private int havocStackAmount = default;

    private readonly float havocBaseCritChanceEffect = -2.0f;
    private readonly float havocBaseCritDamageModifierEffect = 5.0f;

    private float critChanceEffect = default;
    private float critDamageModifierEffect = default;

    private float totalHavocCritChanceEffect = default;
    public float TotalHavocCritChanceEffect => totalHavocCritChanceEffect;

    private float totalHavocCritDamageModifierEffect = default;
    public float TotalHavocCritDamageModifierEffect => totalHavocCritDamageModifierEffect;

    // Chaos Stack
    private readonly float chaosDuration = 4.0f;
    private float chaosDurationTimer = default;

    private readonly int ChaosMaxStackAmount = 10;
    private int chaosStackAmount = default;

    private readonly float chaosBaseCooldownModifierEffect = -5.0f;
    private readonly float chaosBaseDamageModifierEffect = 10.0f;

    private float coolDownModifierEffect = default;
    private float damageModifierEffect = default;

    private float totalChaosCooldownModifierEffect = default;
    public float TotalChaosCooldownModifierEffect => totalChaosCooldownModifierEffect;

    private float totalChaosDamageModifierEffect = default;
    public float TotalChaosDamageModifierEffect => totalChaosDamageModifierEffect;

    //===========================================================================
    protected override void Awake()
    {
        base.Awake();

        currentAbilityIcon = AbilityIconData.AbilityIconD;
    }

    private void OnEnable()
    {
        UpdateAbilityParameter();
    }

    private void Update()
    {
        HavocDurationCheck();
        ChaosDurationCheck();
    }

    private void UpdateAbilityParameter()
    {
        // Havoc Stack
        totalHavocCritChanceEffect = (havocBaseCritChanceEffect + critChanceEffect) * havocStackAmount;
        totalHavocCritDamageModifierEffect = (havocBaseCritDamageModifierEffect + critDamageModifierEffect) * havocStackAmount;

        // Chaos Stack
        totalChaosCooldownModifierEffect = (chaosBaseCooldownModifierEffect + coolDownModifierEffect) * chaosStackAmount;
        totalChaosDamageModifierEffect = (chaosBaseDamageModifierEffect + damageModifierEffect) * chaosStackAmount;
    }

    //===========================================================================
    private void HavocDurationCheck()
    {
        if (havocDurationTimer <= 0)
            return;

        havocDurationTimer -= Time.deltaTime;
        if (havocDurationTimer <= 0)
        {
            towerData.UpdateCritChanceModifier(-TotalHavocCritChanceEffect);
            towerData.UpdateCritDamageModifier(-TotalHavocCritDamageModifierEffect);

            havocStackAmount = 0;
            UpdateAbilityParameter();
        }
    }

    private void ChaosDurationCheck()
    {
        if (chaosDurationTimer <= 0)
            return;

        chaosDurationTimer -= Time.deltaTime;
        if (chaosDurationTimer <= 0)
        {
            towerData.UpdateCritChanceModifier(-TotalHavocCritChanceEffect);
            towerData.UpdateCritDamageModifier(-TotalHavocCritDamageModifierEffect);

            havocStackAmount = 0;
            UpdateAbilityParameter();
        }
    }

    //===========================================================================
    protected override void EvolutionA()
    {

        UpdateAbilityParameter();
        currentAbilityIcon = AbilityIconData.AbilityIconA;
    }

    protected override void EvolutionALeftPath()
    {

    }

    protected override void EvolutionARightPath()
    {

    }

    protected override void EvolutionB()
    {

    }

    protected override void EvolutionBLeftPath()
    {

    }

    protected override void EvolutionBRightPath()
    {

    }

    //===========================================================================
    protected override void AbilityUpgradeLevel01()
    {
        
    }

    protected override void AbilityUpgradeLevel02()
    {
        
    }

    protected override void AbilityUpgradeLevel03()
    {
        
    }

    protected override void AbilityUpgradeLevel04()
    {
        
    }

    protected override void AbilityUpgradeLevel05()
    {
        
    }

    //===========================================================================
    public void IncreaseHavocStackAmount()
    {
        if (havocStackAmount < havocMaxStackAmount)
            havocStackAmount++;

        havocDurationTimer = havocDuration;

        towerData.UpdateCritChanceModifier(-TotalHavocCritChanceEffect);
        towerData.UpdateCritDamageModifier(-TotalHavocCritDamageModifierEffect);

        UpdateAbilityParameter();

        towerData.UpdateCritChanceModifier(TotalHavocCritChanceEffect);
        towerData.UpdateCritDamageModifier(TotalHavocCritDamageModifierEffect);
    }

    public void IncreaseChaosStackAmount()
    {
        if (chaosStackAmount < ChaosMaxStackAmount)
            chaosStackAmount++;

        chaosDurationTimer = chaosDuration;

        towerData.UpdateCoolDownModifer(-TotalChaosCooldownModifierEffect);
        towerData.UpdateDamageModifier(-TotalChaosDamageModifierEffect);

        UpdateAbilityParameter();

        towerData.UpdateCoolDownModifer(TotalChaosCooldownModifierEffect);
        towerData.UpdateDamageModifier(TotalChaosDamageModifierEffect);
    }
}