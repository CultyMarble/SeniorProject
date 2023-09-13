using UnityEngine;
using UnityEngine.Rendering;

public class ATOneCoreAbility : Ability
{
    [Header("Core Ability Sprite Settings:")]
    [SerializeField] private SpriteRenderer towerHead = default;
    [SerializeField] private SOCoreAbilitySpriteData spriteData = default;

    [Header("Core Ability Shooting Settings:")]
    [SerializeField] private Transform shootingPointDefault = default;
    [SerializeField] private Transform shootingPointA_Left = default;
    [SerializeField] private Transform shootingPointA_Right = default;
    [SerializeField] private Transform shootingPointAL_Left = default;
    [SerializeField] private Transform shootingPointAL_Right = default;
    [SerializeField] private Transform shootingPointB = default;

    // CoolDown
    private readonly float baseCoolDown = 0.5f;
    private float abilityCooldownModifier = default;
    private float totalCooldown = default;
    private float cooldownCounter = default;

    // Damage
    private readonly float baseDamage = 30.0f;
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
    private readonly int baseBulletPierceTime = 1;
    private int abilityBulletPierceTimeModifier = default;
    private int totalBulletPierceTime = default;

    // Pooling
    private readonly int poolSize = 100;

    [Header("Ability Bullet Pooling Settings:")]
    [SerializeField] private Transform pfBullet = default;
    [SerializeField] private Transform bulletPool = default;

    [Header("Drone Pooling Settings:")]
    [SerializeField] private Transform pfDroneBullet = default;
    [SerializeField] private Transform droneBulletPool = default;

    [SerializeField] private GameObject pfDrone1 = default;
    [SerializeField] private GameObject pfDrone2 = default;

    [Header("Frag Bullet Pooling Settings:")]
    [SerializeField] private Transform pfFragBullet = default;
    [SerializeField] private Transform fragBulletPool = default;
    [SerializeField] private Transform pfSmallFragBullet = default;
    [SerializeField] private Transform smallFragBulletPool = default;

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

    private void OnDisable()
    {
        pfDrone1.SetActive(false);
        pfDrone2.SetActive(false);
        pfDrone1.transform.position = Vector3.zero;
        pfDrone2.transform.position = Vector3.zero;
    }

    //===========================================================================
    private void PopulatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Instantiate(pfBullet, bulletPool).gameObject.SetActive(false);
        }

        for (int i = 0; i < poolSize; i++)
        {
            Instantiate(pfDroneBullet, droneBulletPool).gameObject.SetActive(false);
        }

        for (int i = 0; i < poolSize; i++)
        {
            Instantiate(pfFragBullet, fragBulletPool).gameObject.SetActive(false);
        }

        for (int i = 0; i < poolSize; i++)
        {
            Instantiate(pfSmallFragBullet, smallFragBulletPool).gameObject.SetActive(false);
        }
    }

    private void CreateBullet(Transform shootingPoint)
    {
        foreach (Transform bullet in bulletPool.transform)
        {
            if (bullet.gameObject.activeSelf == false)
            {
                ATOneCoreAbilityBullet _bullet = bullet.gameObject.GetComponent<ATOneCoreAbilityBullet>();

                _bullet.SetShootingPoint(shootingPoint);
                _bullet.SetCurrentTarget(transform.parent);

                _bullet.SetBulletDamage(totalDamage + Random.Range(-5, 5));

                _bullet.SetBulletCriticalChance(totalCritChance);
                _bullet.SetBulletCriticalDamage(totalCritDamageModifier);

                _bullet.SetPieceTime(totalBulletPierceTime);

                _bullet.transform.position = shootingPoint.position;
                _bullet.gameObject.SetActive(true);

                return;
            }
        }
    }

    private void CreateFragBullet(Transform shootingPoint)
    {
        foreach (Transform bullet in fragBulletPool.transform)
        {
            if (bullet.gameObject.activeSelf == false)
            {
                ATOneFragBullet _bullet = bullet.gameObject.GetComponent<ATOneFragBullet>();

                _bullet.SetSmallBulletPool(smallFragBulletPool);

                _bullet.SetShootingPoint(shootingPoint);
                _bullet.SetCurrentTarget(transform.parent);

                _bullet.SetBulletDamage(totalDamage);

                _bullet.SetBulletCriticalChance(totalCritChance);
                _bullet.SetBulletCriticalDamage(totalCritDamageModifier);

                _bullet.SetPierceTime(totalBulletPierceTime);

                if (EvoA == false && EvoLeft == true)
                    _bullet.ConvertPierceTimeToFragAmount(2);

                _bullet.transform.position = shootingPoint.position;
                _bullet.gameObject.SetActive(true);

                return;
            }
        }
    }

    private void TriggerAbility()
    {
        UpdateAbilityParameter();

        if (AbilityLevel > 2)
        {
            if (EvoA == false)
            {
                CreateFragBullet(shootingPointB);
                return;
            }
        }

        CreateBullet(shootingPointDefault);

        if (EvoA)
        {
            CreateBullet(shootingPointA_Left);
            CreateBullet(shootingPointA_Right);

            if (EvoLeft)
            {
                CreateBullet(shootingPointAL_Left);
                CreateBullet(shootingPointAL_Right);
            }   
        }
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

        // Utility
        totalBulletPierceTime = baseBulletPierceTime + abilityBulletPierceTimeModifier + towerData.PierceTime;
    }

    //===========================================================================
    protected override void EvolutionA()
    {
        base.EvolutionA();

        abilityCooldownModifier += -2.0f;
        abilityDamageModifier += -20.0f;
        abilityCritDamageModifier += -20.0f;

        UpdateAbilityParameter();
        towerHead.sprite = spriteData.TowerHeadA;
    }

    protected override void EvolutionALeftPath()
    {
        base.EvolutionALeftPath();

        abilityCooldownModifier += -3.0f;
        abilityDamageModifier += -30.0f;
        abilityCritDamageModifier += -30.0f;

        UpdateAbilityParameter();
        towerHead.sprite = spriteData.TowerHeadAL;
    }

    protected override void EvolutionARightPath()
    {
        base.EvolutionARightPath();

        abilityCooldownModifier += -3.0f;
        abilityDamageModifier += -20.0f;
        abilityCritDamageModifier += -20.0f;

        pfDrone1.SetActive(true);
        pfDrone2.SetActive(true);

        UpdateAbilityParameter();
        towerHead.sprite = spriteData.TowerHeadAR;
    }

    protected override void EvolutionB()
    {
        base.EvolutionB();

        abilityCooldownModifier += 100.0f;
        abilityDamageModifier += 50.0f;
        abilityBulletPierceTimeModifier += 2;

        UpdateAbilityParameter();
        towerHead.sprite = spriteData.TowerHeadB;
    }

    protected override void EvolutionBLeftPath()
    {
        base.EvolutionBLeftPath();

        abilityCooldownModifier += 200.0f;
        abilityDamageModifier += 200.0f;

        UpdateAbilityParameter();
        towerHead.sprite = spriteData.TowerHeadBL;
    }

    protected override void EvolutionBRightPath()
    {
        base.EvolutionBRightPath();

        abilityCooldownModifier += 100.0f;
        abilityDamageModifier += 100.0f;
        abilityBulletPierceTimeModifier += 3;

        UpdateAbilityParameter();
        towerHead.sprite = spriteData.TowerHeadBR;
    }

    //===========================================================================
    protected override void AbilityUpgradeLevel01()
    {
        abilityCooldownModifier += -5.0f;
        abilityDamageModifier += 10.0f;

        UpdateAbilityParameter();
    }

    protected override void AbilityUpgradeLevel02()
    {
        abilityCooldownModifier += -10.0f;
        abilityDamageModifier += 25.0f;

        UpdateAbilityParameter();
    }

    protected override void AbilityUpgradeLevel03()
    {
        abilityCooldownModifier += -5.0f;
        abilityDamageModifier += 10.0f;

        UpdateAbilityParameter();
    }

    protected override void AbilityUpgradeLevel04()
    {
        abilityCooldownModifier += -5.0f;
        abilityDamageModifier += 10.0f;

        UpdateAbilityParameter();
    }

    protected override void AbilityUpgradeLevel05()
    {
        abilityCooldownModifier += -10.0f;
        abilityDamageModifier += 25.0f;

        UpdateAbilityParameter();
    }
}