using UnityEngine;
using UnityEngine.UI;

public class ArchTowerAbilityUI : MonoBehaviour
{
    public event System.EventHandler OnSelectedTowerEvent;

    private TowerController selectedTower = default;
    public TowerController SelectedTower => selectedTower;

    //======================================================================
    private void Start()
    {
        SetAbilityUIActive(false);
    }

    //======================================================================
    private void SetAbilityUIActive(bool newBool)
    {
        foreach (Transform abilityUI in transform)
        {
            // Set Ability Icon UI
            if (newBool && selectedTower != null)
            {
                AbilityUI _abilityUI = abilityUI.GetComponent<AbilityUI>();

                switch (_abilityUI.AbilityType)
                {
                    case AbilityType.Core:
                        abilityUI.GetComponent<Image>().sprite = selectedTower.AbilityController.CoreAbility.CurrentAbilityIcon;
                        break;
                    case AbilityType.Passive:
                        abilityUI.GetComponent<Image>().sprite = selectedTower.AbilityController.PassiveAbility.CurrentAbilityIcon;
                        break;
                    case AbilityType.Ultimate:
                        abilityUI.GetComponent<Image>().sprite = selectedTower.AbilityController.UltimateAbility.CurrentAbilityIcon;
                        break;
                    default:
                        break;
                }
            }

            abilityUI.gameObject.SetActive(newBool);
        }
    }

    //======================================================================
    public void UpdateAbilityIconUI()
    {
        foreach (Transform abilityUI in transform)
        {
            AbilityUI _abilityUI = abilityUI.GetComponent<AbilityUI>();
            switch (_abilityUI.AbilityType)
            {
                case AbilityType.Core:
                    abilityUI.GetComponent<Image>().sprite = selectedTower.AbilityController.CoreAbility.CurrentAbilityIcon;
                    break;
                case AbilityType.Passive:
                    abilityUI.GetComponent<Image>().sprite = selectedTower.AbilityController.PassiveAbility.CurrentAbilityIcon;
                    break;
                case AbilityType.Ultimate:
                    abilityUI.GetComponent<Image>().sprite = selectedTower.AbilityController.UltimateAbility.CurrentAbilityIcon;
                    break;
                default:
                    break;
            }
        }
    }

    public void SetSelectedTower(TowerController newTower)
    {
        selectedTower = newTower;

        if (selectedTower != null)
            SetAbilityUIActive(true);
        else
            SetAbilityUIActive(false);

        // Invoke Event
        OnSelectedTowerEvent?.Invoke(this, System.EventArgs.Empty);
    }

    public void CheckUpgradeAvailableAmount()
    {
        if (selectedTower.GetComponentInChildren<ArchTowerAbilityController>().UpgradeAvailableAmount != 0)
            return;

        foreach (Transform abilityUI in transform)
        {
            abilityUI.GetComponentInChildren<AbilityUI>().SetLevelUpAbilityButtonActive(false);
        }
    }
}