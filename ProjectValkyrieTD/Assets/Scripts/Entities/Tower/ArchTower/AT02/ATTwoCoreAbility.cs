using UnityEngine;

public class ATTwoCoreAbility : Ability
{
    //[Header("Core Ability Sprite Settings:")]
    [SerializeField] private SpriteRenderer towerHead = default;
    [SerializeField] private SOCoreAbilitySpriteData spriteData = default;

    //// CoolDown
    //private readonly float baseCoolDown = 3.0f;
    //private float abilityCooldownModifier = default;
    //private float totalCooldown = default;
    //private float cooldownCounter = default;

    //// Utility
    //private readonly int baseEffectTowerAmount = 1;
    //private int abilityEffectTowerAmount = default;
    //private int totalEffectTowerAmount = default;

    //// Pooling
    //[Header("Pooling Settings:")]
    //[SerializeField] private Transform pfInspiration = default;
    //[SerializeField] private Transform inspirationPool = default;
    //private readonly int poolSize = 100;

    //======================================================================
    protected override void Awake()
    {
        base.Awake();

        PopulatePool();

        // towerHead.sprite = spriteData.TowerHeadD;
        currentAbilityIcon = AbilityIconData.AbilityIconD;
    }

    private void Start()
    {
        UpdateAbilityParameter();
        //cooldownCounter = totalCooldown;
    }

    private void Update()
    {
        //if (towerTargeting.CurrentTarget == null)
        //    return;

        //cooldownCounter -= Time.deltaTime;
        //if (cooldownCounter <= 0)
        //{
        //    cooldownCounter += totalCooldown;

        //    TriggerAbility();
        //}
    }

    //===========================================================================
    private void PopulatePool()
    {
        //for (int i = 0; i < poolSize; i++)
        //{
        //    Instantiate(pfInspiration, inspirationPool).gameObject.SetActive(false);
        //}
    }

    private void ApplyInspiration()
    {

    }

    private void TriggerAbility()
    {
        UpdateAbilityParameter();

        ApplyInspiration();
    }

    private void UpdateAbilityParameter()
    {
        //// Cooldown
        //totalCooldown = baseCoolDown * (1.0f + ((abilityCooldownModifier + towerData.CoolDownModifier) * 0.01f));

        //// Utility
        //totalEffectTowerAmount = baseEffectTowerAmount + abilityEffectTowerAmount;
    }

    //===========================================================================
    protected override void EvolutionA()
    {
        base.EvolutionA();



        UpdateAbilityParameter();
        towerHead.sprite = spriteData.TowerHeadA;
    }

    protected override void EvolutionALeftPath()
    {
        base.EvolutionALeftPath();



        UpdateAbilityParameter();
        towerHead.sprite = spriteData.TowerHeadAL;
    }

    protected override void EvolutionARightPath()
    {
        base.EvolutionARightPath();



        UpdateAbilityParameter();
        towerHead.sprite = spriteData.TowerHeadAR;
    }

    protected override void EvolutionB()
    {
        base.EvolutionB();



        UpdateAbilityParameter();
        towerHead.sprite = spriteData.TowerHeadB;
    }

    protected override void EvolutionBLeftPath()
    {
        base.EvolutionBLeftPath();



        UpdateAbilityParameter();
        towerHead.sprite = spriteData.TowerHeadBL;
    }

    protected override void EvolutionBRightPath()
    {
        base.EvolutionBRightPath();



        UpdateAbilityParameter();
        towerHead.sprite = spriteData.TowerHeadBR;
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
