using UnityEngine;

public class TowerController : MonoBehaviour
{
    [Header("Tower Select Indicator:")]
    [SerializeField] private SpriteRenderer rangeIndicator = default;
    [SerializeField] private SpriteRenderer hoverIndicator = default;

    [Header("Tower Ability Controller:")]
    [SerializeField] private ArchTowerAbilityController abilityController = default;
    public ArchTowerAbilityController AbilityController => abilityController;

    //===========================================================================
    private void OnMouseEnter()
    {
        SetHoverIndicatorActive(true);
        TowerManager.Instance.hoveringTower = this;
    }

    private void OnMouseExit()
    {
        SetHoverIndicatorActive(false);
        TowerManager.Instance.hoveringTower = null;
    }

    //===========================================================================
    public void SetRangeIndicatorActive(bool newBool) { rangeIndicator.enabled = newBool; }

    public void SetHoverIndicatorActive(bool newBool) { hoverIndicator.enabled = newBool; }
}