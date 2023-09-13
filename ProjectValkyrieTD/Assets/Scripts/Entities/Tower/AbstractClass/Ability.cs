using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] private string abilityName = default;
    public string AbilityName => abilityName;

    [SerializeField] private SOAbilityIcon abilityIconData = default;
    public SOAbilityIcon AbilityIconData => abilityIconData;

    protected Sprite currentAbilityIcon = default;
    public Sprite CurrentAbilityIcon => currentAbilityIcon;
    
    private readonly int levelMax = 5;
    public int LevelMax => levelMax;

    private int level = default;
    public int AbilityLevel => level;

    private bool evoA = default;
    public bool EvoA => evoA;

    private bool evoLeft = default;
    public bool EvoLeft => evoLeft;

    protected TowerTargeting towerTargeting = default;
    protected TowerData towerData = default;

    //===========================================================================
    protected virtual void Awake()
    {
        towerTargeting = GetComponentInParent<TowerTargeting>();
        towerData = GetComponentInParent<TowerData>();
    }

    //===========================================================================
    public int IncreaseAbilityLevel()
    {
        if (level == levelMax)
            return -1;

        level++;

        switch (AbilityLevel)
        {
            case 1:
                AbilityUpgradeLevel01();
                break;
            case 2:
                AbilityUpgradeLevel02();
                break;
            case 3:
                AbilityUpgradeLevel03();
                break;
            case 4:
                AbilityUpgradeLevel04();
                break;
            case 5:
                AbilityUpgradeLevel05();
                break;
        }

        return level;
    }

    //===========================================================================
    protected virtual void EvolutionA() { currentAbilityIcon = AbilityIconData.AbilityIconA; }

    protected virtual void EvolutionALeftPath() { currentAbilityIcon = AbilityIconData.AbilityIconAL; }

    protected virtual void EvolutionARightPath() { currentAbilityIcon = AbilityIconData.AbilityIconAR; }

    protected virtual void EvolutionB() { currentAbilityIcon = AbilityIconData.AbilityIconB; }

    protected virtual void EvolutionBLeftPath() { currentAbilityIcon = AbilityIconData.AbilityIconBL; }

    protected virtual void EvolutionBRightPath() { currentAbilityIcon = AbilityIconData.AbilityIconBL; }

    //===========================================================================
    protected abstract void AbilityUpgradeLevel01();
    protected abstract void AbilityUpgradeLevel02();
    protected abstract void AbilityUpgradeLevel03();
    protected abstract void AbilityUpgradeLevel04();
    protected abstract void AbilityUpgradeLevel05();

    //===========================================================================
    public void UpgradeAbilityEvolution(EvolvePath upgradePath)
    {
        switch (AbilityLevel)
        {
            case 2:
                switch (upgradePath)
                {
                    case EvolvePath.Left:
                        evoA = true;
                        EvolutionA();
                        break;
                    case EvolvePath.Right:
                        evoA = false;
                        EvolutionB();
                        break;
                    default:
                        break;
                }
                break;
            case 5:
                switch (upgradePath)
                {
                    case EvolvePath.Left:
                        evoLeft = true;
                        if (evoA)
                            EvolutionALeftPath();
                        else
                            EvolutionBLeftPath();
                        break;
                    case EvolvePath.Right:
                        evoLeft = false;
                        if (evoA)
                            EvolutionARightPath();
                        else
                            EvolutionBRightPath();
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }
}