using UnityEngine;

public class ATTwoUltimateAbility : Ability
{
    // CoolDown
    private readonly float baseCoolDown = 10.0f;
    private float abilityCooldownModifier = default;
    private float totalCooldown = default;
    private float cooldownCounter = default;

    // Duration
    // private float baseDuration = 4.0f;

    // Pooling
    [Header("Pooling Settings:")]
    [SerializeField] private Transform pfWhip = default;
    [SerializeField] private Transform whipPool = default;
    private readonly int poolSize = 100;

    //======================================================================
    protected override void Awake()
    {
        base.Awake();

        PopulatePool();

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

    //======================================================================
    private void PopulatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Instantiate(pfWhip, whipPool).gameObject.SetActive(false);
        }
    }

    private void CreateWhipAndApplyEffect()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, towerData.Range);
        foreach (Collider2D collider2D in collider2DArray)
        {
            if (collider2D.TryGetComponent(out Enemy enemy))
            {// is an enemy
                // Create Whip
                Transform _whip = Instantiate(pfWhip, whipPool);
                _whip.position = collider2D.transform.position;

                Destroy(_whip.gameObject, 1.0f);

                // Apply Effect

            }
        }
    }

    private void TriggerAbility()
    {
        UpdateAbilityParameter();

        CreateWhipAndApplyEffect();
    }

    private void UpdateAbilityParameter()
    {
        // Cooldown
        totalCooldown = baseCoolDown * (1.0f + ((abilityCooldownModifier + towerData.CoolDownModifier) * 0.01f));
    }

    //======================================================================
    protected override void EvolutionA()
    {
        base.EvolutionA();



        UpdateAbilityParameter();
        currentAbilityIcon = AbilityIconData.AbilityIconA;
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

    //======================================================================
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
}
