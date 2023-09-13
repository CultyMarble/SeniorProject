using UnityEngine;

public class NTOneCoreAbility : Ability
{
    [Header("Core Ability Sprite Settings:")]
    [SerializeField] private SpriteRenderer towerHead = default;
    [SerializeField] private SOCoreAbilitySpriteData spriteData = default;

    // CoolDown
    private readonly float baseCoolDown = 5.0f;
    private float abilityCooldownModifier = default;
    private float totalCooldown = default;
    private float cooldownCounter = default;

    // Utility
    private readonly float baseBlackholeDuration = 2.0f;
    private float abilityDurationModifier = default;
    private float totalBlackholeDuration = default;

    // Pooling
    [Header("Pooling Settings:")]
    [SerializeField] private Transform pfBlackhole = default;
    [SerializeField] private Transform blackholePool = default;
    // private readonly int poolSize = 100;

    //======================================================================
    protected override void Awake()
    {
        base.Awake();

        PopulatePool();

        towerHead.sprite = spriteData.TowerHeadD;
        currentAbilityIcon = AbilityIconData.AbilityIconD;
    }

    private void Start()
    {
        UpdateAbilityParameter();
        cooldownCounter = totalCooldown;
    }

    private void Update()
    {
        if (towerTargeting.CurrentTarget == null)
            return;

        cooldownCounter -= Time.deltaTime;
        if (cooldownCounter <= 0)
        {
            cooldownCounter += totalCooldown;

            TriggerAbility();
        }
    }

    //===========================================================================
    private void PopulatePool()
    {

    }

    private void CreateBlackhole()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, towerData.Range);

        Transform _blackhole = Instantiate(pfBlackhole, blackholePool);
        _blackhole.position = collider2DArray[Random.Range(1, collider2DArray.Length)].transform.position;
        Destroy(_blackhole.gameObject, totalBlackholeDuration);
    }

    private void TriggerAbility()
    {
        CreateBlackhole();
    }

    private void UpdateAbilityParameter()
    {
        // Cooldown
        totalCooldown = baseCoolDown * (1.0f + ((abilityCooldownModifier + towerData.CoolDownModifier) * 0.01f));

        // Utility
        totalBlackholeDuration = baseBlackholeDuration + abilityDurationModifier;
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
