using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATThreePassiveAbility : Ability
{


    //===========================================================================
    private void UpdateAbilityParameter()
    {
        //// Cooldown
        //totalCooldown = baseCoolDown * (1.0f + ((abilityCooldownModifier + towerData.CoolDownModifier) * 0.01f));

        //// Damage
        //totalDamage = baseDamage * (1.0f + ((abilityDamageModifier + towerData.DamageModifier) * 0.01f));

        //// Crit Chance & Crit Damage Modifier
        //totalCritChance = baseCritChance + abilityCritChanceModifier + towerData.CritChanceModifier;
        //totalCritDamageModifier = baseCritDamageModifier + abilityCritDamageModifier + towerData.CriticalDamageModifier;

        //// Utility
        //totalBulletPierceTime = baseBulletPierceTime + abilityBulletPierceTimeModifier;
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
