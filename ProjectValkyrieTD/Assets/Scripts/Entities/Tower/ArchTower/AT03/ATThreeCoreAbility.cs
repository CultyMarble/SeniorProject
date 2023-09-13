using UnityEngine;

public class ATThreeCoreAbility : Ability
{
    [Header("Core Ability Sprite Settings:")]
    [SerializeField] private SpriteRenderer towerHead = default;
    [SerializeField] private SOCoreAbilitySpriteData spriteData = default;

    [Header("Core Ability Shooting Settings:")]
    [SerializeField] private Transform shootingPointDefault = default;

    // CoolDown
    private readonly float baseCoolDown = 0.25f;
    private float abilityCooldownModifier = default;
    private float totalCooldown = default;
    private float cooldownCounter = default;

    // Damage
    private readonly float baseDamage = 10.0f;
    private float abilityDamageModifier = default;
    private float totalDamage = default;

    // Crit Chance & Crit Damage Modifier
    // private readonly float baseCritChance = 5.0f;
    // private float abilityCritChanceModifier = default;
    // private float totalCritChance = default;

    // private readonly float baseCritDamageModifier = 200.0f;
    // private float abilityCritDamageModifier = default;
    // private float totalCritDamageModifier = default;

    // Utility
    private int chainTimes = 3;
    private float chainRange = 5;

    // Pooling
    [Header("Pooling Settings:")]
    [SerializeField] private Transform pfLaserHead = default;
    [SerializeField] private Transform laserHeadPool = default;

    [SerializeField] private Transform pfLaserBody = default;
    [SerializeField] private Transform laserBodyPool = default;

    // private readonly int poolSize = 50;

    // [SerializeField] private Transform pfLaserBody = default;

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
        //for (int i = 0; i < poolSize; i++)
        //{
        //    Instantiate(pfBullet, bulletPool).gameObject.SetActive(false);
        //}
    }

    private void CreateLaserBodyChain(Transform laserHead1, Transform laserHead2)
    {
        Transform _pfLaserBody = Instantiate(pfLaserBody, laserBodyPool);

        _pfLaserBody.position = Vector3.LerpUnclamped(laserHead1.position, laserHead2.position, 0.5f);
        _pfLaserBody.localScale = new Vector3(Vector3.Distance(laserHead1.position, laserHead2.position) * 4, _pfLaserBody.localScale.y, _pfLaserBody.localScale.z);

        Vector3 _direction = (laserHead1.position - laserHead2.position).normalized;
        float _targetdirection = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        _pfLaserBody.eulerAngles = new Vector3(0.0f, 0.0f, _targetdirection);
        
        Destroy(_pfLaserBody.gameObject, 0.15f);
    }

    private void CreateLaser()
    {
        Transform _currentTarget = towerTargeting.CurrentTarget.transform;

        // Create first laserHead
        Transform _pfLaserHeadFront = Instantiate(pfLaserHead, laserHeadPool);
        _pfLaserHeadFront.position = shootingPointDefault.position;
        Destroy(_pfLaserHeadFront.gameObject, 0.15f);

        // Create laserChain for first target
        Transform _pfLaserHeadChain = Instantiate(pfLaserHead, laserHeadPool);
        _pfLaserHeadChain.position = _currentTarget.position;
        _currentTarget.GetComponent<EnemyHealth>().UpdateCurrentHealth(-totalDamage);
        Destroy(_pfLaserHeadChain.gameObject, 0.15f);

        // Create laserBody for first target
        CreateLaserBodyChain(shootingPointDefault, _currentTarget);

        // Create laserChain for chain target
        for (int i = 0; i < chainTimes - 1; i++)
        {
            Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(_currentTarget.position, chainRange);

            if (collider2DArray.Length < 1)
                return;

            // Pick random target in range
            Transform _currentTargetNext = default;
            bool _targetPicked = false;
            while(_targetPicked == false)
            {
                _currentTargetNext = collider2DArray[Random.Range(0, collider2DArray.Length)].transform;
                if (_currentTargetNext != _currentTarget)
                    _targetPicked = true;
            }

            // Pick closest target to _currentTarget
            foreach (Collider2D collider2D in collider2DArray)
            {
                if (collider2D.transform != _currentTarget.transform)
                {
                    if (Vector3.Distance(collider2D.transform.position, _currentTarget.transform.position) <
                        Vector3.Distance(_currentTargetNext.transform.position, _currentTarget.transform.position))
                    {
                        _currentTargetNext = collider2D.transform;
                    }
                }
            }

            // Create LaserHead
            Transform _pfLaserChainEnd = Instantiate(pfLaserHead, laserHeadPool);
            _pfLaserChainEnd.position = _currentTargetNext.position;
            _currentTargetNext.GetComponent<EnemyHealth>().UpdateCurrentHealth(-totalDamage);
            Destroy(_pfLaserChainEnd.gameObject, 0.15f);

            // Create LaserChain
            CreateLaserBodyChain(_currentTarget, _currentTargetNext);

            _currentTarget = _currentTargetNext;
        }
    }

    private void TriggerAbility()
    {
        UpdateAbilityParameter();

        CreateLaser();
    }

    private void UpdateAbilityParameter()
    {
        // Cooldown
        totalCooldown = baseCoolDown * (1.0f + ((abilityCooldownModifier + towerData.CoolDownModifier) * 0.01f));

        // Damage
        totalDamage = baseDamage * (1.0f + ((abilityDamageModifier + towerData.DamageModifier) * 0.01f));

        // Crit Chance & Crit Damage Modifier
        // totalCritChance = baseCritChance + abilityCritChanceModifier + towerData.CritChanceModifier;
        // totalCritDamageModifier = baseCritDamageModifier + abilityCritDamageModifier + towerData.CriticalDamageModifier;

        // Utility
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