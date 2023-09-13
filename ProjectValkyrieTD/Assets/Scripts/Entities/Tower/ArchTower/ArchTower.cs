using UnityEngine;

public class ArchTower : MonoBehaviour
{
    // LEVEL SYSTEM
    public struct OnExpChangedEventAgrs { public float expRatio; }
    public event System.EventHandler<OnExpChangedEventAgrs> OnExpChangedEvent;

    public struct OnLevelUpEventAgrs { public int level; }
    public event System.EventHandler<OnLevelUpEventAgrs> OnLevelUpEvent;

    [Header("Tower Level Settings:")]
    [SerializeField] private SOTowerExpToLevelUp towerExpData = default;

    private int maxLevel = default;
    private int level = default;
    private int expToLevelUp = default;
    private int currentExp = default;

    [Header("Tower Settings:")]
    [SerializeField] private string towerName = "The Annihilator";
    public string TowerName => towerName;

    //===========================================================================
    private void Start()
    {
        InitTowerLevelSystem();
    }

    //===========================================================================
    private void InitTowerLevelSystem()
    {
        maxLevel = towerExpData.MaxLevel;
        level = 0;

        UpdateExpToLevelUp();
        UpdateExp();
    }

    private void UpdateExpToLevelUp()
    {
        expToLevelUp = Mathf.RoundToInt((((level + 1) * towerExpData.ExpToLevelUpList[level]) * 0.25f) + 100);
    }

    //===========================================================================
    public void UpdateExp(int amount = 0)
    {
        if (level == maxLevel)
            return;

        currentExp += amount;
        if (currentExp >= expToLevelUp)
        {
            level++;

            //Invoke Event
            OnLevelUpEvent?.Invoke(this, new OnLevelUpEventAgrs { level = level });

            if (level == maxLevel)
            {
                currentExp = expToLevelUp;
            }
            else
            {
                currentExp -= expToLevelUp;

                UpdateExpToLevelUp();
            }
        }

        //Invoke Event
        OnExpChangedEvent?.Invoke(this, new OnExpChangedEventAgrs { expRatio = (float)currentExp / (float)expToLevelUp });
    }
}