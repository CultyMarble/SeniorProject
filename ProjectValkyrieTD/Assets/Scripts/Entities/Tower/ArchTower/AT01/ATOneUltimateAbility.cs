using UnityEngine;

public class ATOneUltimateAbility : Ability
{
    // CoolDown
    private readonly float baseCoolDown = 7.0f;
    private float abilityCooldownModifier = default;
    private float totalCooldown = default;
    private float cooldownCounter = default;

    // Damage
    private readonly float baseDamage = 50.0f;
    private float abilityDamageModifier = default;
    private float totalDamage = default;

    // Crit Chance & Crit Damage Modifier
    private readonly float baseCritChance = 5.0f;
    private float abilityCritChanceModifier = default;
    private float totalCritChance = default;

    private readonly float baseCritDamageModifier = 200.0f;
    private float abilityCritDamageModifier = default;
    private float totalCritDamageModifier = default;

    // Utility
    private readonly float baseMineDuration = 3.0f;
    private readonly float baseMineRange = 1.5f;

    // Pooling
    [Header("Pooling Settings:")]
    [SerializeField] private Transform pfMine = default;
    [SerializeField] private Transform minePool = default;
    private readonly int poolSize = 100;

    //======================================================================
    protected override void Awake()
    {
        base.Awake();

        currentAbilityIcon = AbilityIconData.AbilityIconD;

        PopulatePool();
    }

    private void Start()
    {
        UpdateAbilityParameter();

        cooldownCounter = totalCooldown;
    }

    private void Update()
    {
        cooldownCounter -= Time.deltaTime;
        if (cooldownCounter <= 0)
        {
            cooldownCounter += totalCooldown;

            TriggerAbility();
        }
    }

    //======================================================================
    private void PopulatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Instantiate(pfMine, minePool).gameObject.SetActive(false);
        }
    }

    private void CreateMine()
    {
        for (int i = 0; i < 5; i++)
        {
            foreach (Transform mine in minePool.transform)
            {
                if (mine.gameObject.activeSelf == false)
                {
                    var _mine = mine.GetComponent<ATOneUtimateAbilityMine>();

                    _mine.SetDamage(totalDamage);

                    _mine.SetCritChance(totalCritChance);
                    _mine.SetCritDamageModifier(totalCritDamageModifier);

                    _mine.SetDurationTimer(baseMineDuration);
                    _mine.SetRange(baseMineRange);

                    mine.position = transform.position + (CultyMarbleHelper.GetRamdomDirection() * towerData.Range * 0.66f);
                    mine.gameObject.SetActive(true);

                    break;
                }
            }
        }
    }

    private void TriggerAbility()
    {
        UpdateAbilityParameter();

        CreateMine();
    }

    private void UpdateAbilityParameter()
    {
        // Cooldown
        totalCooldown = baseCoolDown * (1.0f + ((abilityCooldownModifier + towerData.CoolDownModifier) * 0.01f));

        // Damage
        totalDamage = baseDamage * (1.0f + ((abilityDamageModifier + towerData.DamageModifier) * 0.01f));

        // Crit Chance & Crit Damage Modifier
        totalCritChance = baseCritChance + abilityCritChanceModifier + towerData.CritChanceModifier;
        totalCritDamageModifier = baseCritDamageModifier + abilityCritDamageModifier + towerData.CriticalDamageModifier;
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