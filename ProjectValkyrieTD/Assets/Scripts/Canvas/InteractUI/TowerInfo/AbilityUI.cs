using UnityEngine;
using UnityEngine.UI;

public enum EvolvePath { Left, Right, }

public class AbilityUI : MonoBehaviour
{
    [SerializeField] private AbilityType abilityType = default;
    public AbilityType AbilityType => abilityType;

    private Button levelUpAbilityButton = default;
    private Button showUpgradePathButton = default;
    private Button lUpgradePathButton = default;
    private Button rUpgradePathButton = default;

    private ArchTowerAbilityUI archTowerAbilityInfoUI = default;
    private Ability abilityToEvolve = default;

    //===========================================================================
    private void Awake()
    {
        archTowerAbilityInfoUI = GetComponentInParent<ArchTowerAbilityUI>();

        levelUpAbilityButton = GetComponentsInChildren<Button>()[0];
        showUpgradePathButton = GetComponentsInChildren<Button>()[1];
        lUpgradePathButton = GetComponentsInChildren<Button>()[2];
        rUpgradePathButton = GetComponentsInChildren<Button>()[3];

        SetLevelUpAbilityButtonActive(false);
        SetShowEvolutionPathButtonActive(false);
        SetEvolutionPathButtonActive(false);

        levelUpAbilityButton.onClick.AddListener(LevelUpAbility);
        showUpgradePathButton.onClick.AddListener(ToggleEvolutionPathButtonActive);

        lUpgradePathButton.onClick.AddListener(() => EvolveAbility(EvolvePath.Left));
        rUpgradePathButton.onClick.AddListener(() => EvolveAbility(EvolvePath.Right));
    }

    private void OnEnable()
    {
        archTowerAbilityInfoUI.OnSelectedTowerEvent += AbilityInfoUI_OnSelectedTowerEvent;
    }

    private void OnDisable()
    {
        archTowerAbilityInfoUI.OnSelectedTowerEvent -= AbilityInfoUI_OnSelectedTowerEvent;
    }

    //===========================================================================
    private void AbilityInfoUI_OnSelectedTowerEvent(object sender, System.EventArgs e)
    {
        switch (abilityType)
        {
            case AbilityType.Core:
                abilityToEvolve = archTowerAbilityInfoUI.SelectedTower.GetComponentInChildren<ArchTowerAbilityController>().CoreAbility;
                break;
            case AbilityType.Passive:
                abilityToEvolve = archTowerAbilityInfoUI.SelectedTower.GetComponentInChildren<ArchTowerAbilityController>().PassiveAbility;
                break;
            case AbilityType.Ultimate:
                abilityToEvolve = archTowerAbilityInfoUI.SelectedTower.GetComponentInChildren<ArchTowerAbilityController>().UltimateAbility;
                break;
            default:
                break;
        }

        if (abilityToEvolve.GetComponentInParent<ArchTowerAbilityController>().UpgradeAvailableAmount == 0 ||
            abilityToEvolve.AbilityLevel == abilityToEvolve.LevelMax)
            return;

        SetLevelUpAbilityButtonActive(true);
    }

    private void LevelUpAbility()
    {
        int _level = archTowerAbilityInfoUI.SelectedTower.GetComponentInChildren<ArchTowerAbilityController>().LevelUpAbility(abilityType);
        switch (_level)
        {
            case 2:
                SetShowEvolutionPathButtonActive(true);
                break;
            case 5:
                SetShowEvolutionPathButtonActive(true);
                break;
            default:
                break;
        }

        archTowerAbilityInfoUI.CheckUpgradeAvailableAmount();
    }

    //===========================================================================
    private void SetShowEvolutionPathButtonActive(bool newBool)
    {
        showUpgradePathButton.gameObject.SetActive(newBool);

        if (archTowerAbilityInfoUI.SelectedTower == null)
            return;

        switch (abilityType)
        {
            case AbilityType.Core:
                abilityToEvolve = archTowerAbilityInfoUI.SelectedTower.GetComponentInChildren<ArchTowerAbilityController>().CoreAbility;
                break;
            case AbilityType.Passive:
                abilityToEvolve = archTowerAbilityInfoUI.SelectedTower.GetComponentInChildren<ArchTowerAbilityController>().PassiveAbility;
                break;
            case AbilityType.Ultimate:
                abilityToEvolve = archTowerAbilityInfoUI.SelectedTower.GetComponentInChildren<ArchTowerAbilityController>().UltimateAbility;
                break;
            default:
                break;
        }

        // Set Evolution Path Icon
        switch (abilityToEvolve.AbilityLevel)
        {
            case 2:
                lUpgradePathButton.GetComponent<Image>().sprite = abilityToEvolve.AbilityIconData.AbilityIconA;
                rUpgradePathButton.GetComponent<Image>().sprite = abilityToEvolve.AbilityIconData.AbilityIconB;
                break;
            case 5:
                if (((Ability)abilityToEvolve).EvoA == true)
                {
                    lUpgradePathButton.GetComponent<Image>().sprite = abilityToEvolve.AbilityIconData.AbilityIconAL;
                    rUpgradePathButton.GetComponent<Image>().sprite = abilityToEvolve.AbilityIconData.AbilityIconAR;
                }
                else
                {
                    lUpgradePathButton.GetComponent<Image>().sprite = abilityToEvolve.AbilityIconData.AbilityIconBL;
                    rUpgradePathButton.GetComponent<Image>().sprite = abilityToEvolve.AbilityIconData.AbilityIconBR;
                }
                break;
            default:
                break;
        }
    }

    private void ToggleEvolutionPathButtonActive()
    {
        lUpgradePathButton.gameObject.SetActive(!lUpgradePathButton.gameObject.activeInHierarchy);
        rUpgradePathButton.gameObject.SetActive(!rUpgradePathButton.gameObject.activeInHierarchy);
    }

    private void EvolveAbility(EvolvePath path)
    {
        ArchTowerAbilityController _abilityController = archTowerAbilityInfoUI.SelectedTower.GetComponentInChildren<ArchTowerAbilityController>();

        _abilityController.UpgradeAbilityEvolution(abilityType, path);

        GetComponentInParent<ArchTowerAbilityUI>().UpdateAbilityIconUI();

        SetEvolutionPathButtonActive(false);
        SetShowEvolutionPathButtonActive(false);
    }

    //===========================================================================
    public void SetLevelUpAbilityButtonActive(bool newBool)
    {
        levelUpAbilityButton.gameObject.SetActive(newBool);
    }

    public void SetEvolutionPathButtonActive(bool newBool)
    {
        lUpgradePathButton.gameObject.SetActive(newBool);
        rUpgradePathButton.gameObject.SetActive(newBool);
    }
}