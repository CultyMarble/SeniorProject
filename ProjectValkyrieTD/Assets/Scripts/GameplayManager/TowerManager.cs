using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TowerManager : SingletonMonobehaviour<TowerManager>
{
    [HideInInspector] public TowerController hoveringTower = default;
    [HideInInspector] public TowerController selectedTower = default;

    [Header("Tower Ability UI Settings:")]
    public ArchTowerAbilityUI archTowerAbilityUI = default;

    [Header("Tower Ability UI Settings:")]
    public GameObject TowerInventoryUI = default;

    [Header("Tower Health:")]
    [SerializeField] private TextMeshProUGUI archTowerHealthText = default;
    [SerializeField] private int archTowerHealth = default;
    private int currentArchTowerHealth = default;

    //===========================================================================
    private void OnEnable()
    {
        PlayerInputManager.Instance.OnEscButtonPressed += Instance_OnEscButtonPressed;
        PlayerInputManager.Instance.OnLeftMouseButtonPressed += Instance_OnLeftMouseButtonPressed;

        SceneLoadManager.Instance.OnPurge += Instance_OnPurge;
    }

    private void OnDisable()
    {
        PlayerInputManager.Instance.OnEscButtonPressed -= Instance_OnEscButtonPressed;
        PlayerInputManager.Instance.OnLeftMouseButtonPressed -= Instance_OnLeftMouseButtonPressed;

        SceneLoadManager.Instance.OnPurge -= Instance_OnPurge;
    }

    //===========================================================================
    private void Instance_OnEscButtonPressed(object sender, System.EventArgs e)
    {
        ClearSelectedTower();
    }

    private void Instance_OnLeftMouseButtonPressed(object sender, System.EventArgs e)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        ClearSelectedTower();

        if (hoveringTower == null)
            return;

        selectedTower = hoveringTower;

        selectedTower.SetRangeIndicatorActive(true);
        UpdateSelectedTowerInfoUI();

        TowerInventoryUI.SetActive(true);
    }

    private void Instance_OnPurge(object sender, System.EventArgs e)
    {
        ResetArchTowerHealth();
    }

    //===========================================================================
    private void ClearSelectedTower()
    {
        if (selectedTower)
        {
            selectedTower.SetRangeIndicatorActive(false);
            selectedTower = null;

            archTowerAbilityUI.SetSelectedTower(selectedTower);
            TowerInventoryUI.SetActive(false);
        }
    }

    private void UpdateArchTowerHealth()
    {
        archTowerHealthText.SetText("Health: " + currentArchTowerHealth);
    }

    //===========================================================================
    public void UpdateSelectedTowerInfoUI()
    {
        archTowerAbilityUI.SetSelectedTower(selectedTower);
    }

    public void UpdateArchTowerHealth(int amount)
    {
        currentArchTowerHealth += amount;

        if (currentArchTowerHealth > archTowerHealth)
        {
            currentArchTowerHealth = archTowerHealth;
        }
        else if (currentArchTowerHealth < 0)
        {
            currentArchTowerHealth = 0;

            Time.timeScale = 0.0f;

            GameOverMenuUI.Instance.SetActive(true);
        }

        UpdateArchTowerHealth();
    }

    public void ResetArchTowerHealth()
    {
        currentArchTowerHealth = archTowerHealth;

        UpdateArchTowerHealth();
    }
}