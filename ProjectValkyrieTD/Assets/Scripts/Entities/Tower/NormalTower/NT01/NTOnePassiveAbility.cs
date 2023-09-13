using UnityEngine;

public class NTOnePassiveAbility : Ability
{


    //======================================================================
    private void Start()
    {
        UpdateAbilityParameter();
    }

    //===========================================================================
    private void TriggerAbility()
    {

    }

    private void UpdateAbilityParameter()
    {

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