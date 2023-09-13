using UnityEngine;

public class TowerExpBarUI : MonoBehaviour
{
    [SerializeField] private Transform enemyHealthBar;
    private ArchTower tower = default;

    //===========================================================================
    private void Awake()
    {
        tower = GetComponentInParent<ArchTower>();
    }

    private void OnEnable()
    {
        tower.OnExpChangedEvent += Tower_OnExpChangedEvent;
    }

    private void OnDisable()
    {
        tower.OnExpChangedEvent -= Tower_OnExpChangedEvent;
    }

    //===========================================================================
    private void Tower_OnExpChangedEvent(object sender, ArchTower.OnExpChangedEventAgrs e)
    {
        UpdateHealthBarScale(e.expRatio);
    }

    private void UpdateHealthBarScale(float healthRatio)
    {
        enemyHealthBar.localScale = new Vector3(healthRatio, 1.0f, 1.0f);
    }
}
