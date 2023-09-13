using UnityEngine;

public class ArchTowerAbilityController : MonoBehaviour
{
    [Header("Core Ability:")]
    [SerializeField] private Ability coreAbility = default;
    public Ability CoreAbility => coreAbility;

    [Header("Passive Ability:")]
    [SerializeField] private Ability passiveAbility = default;
    public Ability PassiveAbility => passiveAbility;

    [Header("Ultimate Ability:")]
    [SerializeField] private Ability ultimateAbility = default;
    public Ability UltimateAbility => ultimateAbility;

    private ArchTower tower = default;

    private int upgradeAvailableAmount = default;
    public int UpgradeAvailableAmount => upgradeAvailableAmount;

    //===========================================================================
    private void Awake()
    {
        tower = GetComponentInParent<ArchTower>();
    }

    private void OnEnable()
    {
        tower.OnLevelUpEvent += Tower_OnLevelUpEvent;
    }

    private void OnDisable()
    {
        tower.OnLevelUpEvent -= Tower_OnLevelUpEvent;
    }

    //===========================================================================
    private void Tower_OnLevelUpEvent(object sender, ArchTower.OnLevelUpEventAgrs e)
    {
        upgradeAvailableAmount++;

        // Update Tower Ability UI when there is a selected Tower
        if (TowerManager.Instance.selectedTower == transform.GetComponentInParent<TowerController>())
        {
            TowerManager.Instance.UpdateSelectedTowerInfoUI();
        }
    }

    //===========================================================================
    public int LevelUpAbility(AbilityType type)
    {
        if (upgradeAvailableAmount == 0)
            return -1;

        upgradeAvailableAmount--;

        switch (type)
        {
            case AbilityType.Core:
                return coreAbility.IncreaseAbilityLevel();
            case AbilityType.Passive:
                return passiveAbility.IncreaseAbilityLevel();
            case AbilityType.Ultimate:
                return ultimateAbility.IncreaseAbilityLevel();
            default:
                return -1;
        }
    }

    public void UpgradeAbilityEvolution(AbilityType type, EvolvePath upgradePath)
    {
        switch (type)
        {
            case AbilityType.Core:
                coreAbility.UpgradeAbilityEvolution(upgradePath);
                break;
            case AbilityType.Passive:
                passiveAbility.UpgradeAbilityEvolution(upgradePath);
                break;
            case AbilityType.Ultimate:
                ultimateAbility.UpgradeAbilityEvolution(upgradePath);
                break;
            default:
                break;
        }
    }
}