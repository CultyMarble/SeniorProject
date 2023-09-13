using UnityEngine;

public class ATTwoPassiveAbility : Ability
{
    // Damage
    private readonly float baseDamage = 20;
    private float abilityDamageModifier = default;
    private float totalDamage = default;

    // Burning Damage
    private float burningDamageFactorModifier = default; // In percentage, starts at 0%

    //===========================================================================
    protected override void Awake()
    {
        base.Awake();

        currentAbilityIcon = AbilityIconData.AbilityIconD;
    }

    private void Start()
    {
        UpdateAbilityParameter();
    }

    private void FixedUpdate()
    {
        TriggerAbility();
    }

    //===========================================================================
    private void TriggerAbility()
    {
        UpdateAbilityParameter();

        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, towerData.Range);
        foreach (Collider2D collider2D in collider2DArray)
        {
            if (collider2D.CompareTag("Enemy"))
            {
                StatusEffectBurning _burning = collider2D.GetComponentInChildren<StatusEffectBurning>();

                _burning.SetBurningDamageFactor(burningDamageFactorModifier);
                _burning.SetBurningDamagePerTrigger(totalDamage);
                _burning.SetDuration();
                _burning.enabled = true;
            }
        }
    }

    private void UpdateAbilityParameter()
    {
        // Damage
        totalDamage = baseDamage * (1.0f + ((abilityDamageModifier + towerData.DamageModifier) * 0.01f));
    }

    //===========================================================================
    protected override void EvolutionA()
    {
        base.EvolutionA();



        UpdateAbilityParameter();
    }

    protected override void EvolutionALeftPath()
    {
        base.EvolutionALeftPath();



        UpdateAbilityParameter();
    }

    protected override void EvolutionARightPath()
    {
        base.EvolutionARightPath();



        UpdateAbilityParameter();
    }

    protected override void EvolutionB()
    {
        base.EvolutionB();



        UpdateAbilityParameter();
    }

    protected override void EvolutionBLeftPath()
    {
        base.EvolutionBLeftPath();



        UpdateAbilityParameter();
    }

    protected override void EvolutionBRightPath()
    {
        base.EvolutionBRightPath();



        UpdateAbilityParameter();
    }

    //===========================================================================
    protected override void AbilityUpgradeLevel01()
    {


        UpdateAbilityParameter();
    }

    protected override void AbilityUpgradeLevel02()
    {

        UpdateAbilityParameter();
    }

    protected override void AbilityUpgradeLevel03()
    {

        UpdateAbilityParameter();
    }

    protected override void AbilityUpgradeLevel04()
    {

        UpdateAbilityParameter();
    }

    protected override void AbilityUpgradeLevel05()
    {


        UpdateAbilityParameter();
    }
}