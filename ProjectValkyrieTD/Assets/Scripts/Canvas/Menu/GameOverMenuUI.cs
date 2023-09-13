using UnityEngine;
using UnityEngine.UI;

public class GameOverMenuUI : SingletonMonobehaviour<GameOverMenuUI>
{
    [SerializeField] private GameObject go_text = default;
    [SerializeField] private Button go_menuButton = default;

    //===========================================================================
    private void OnEnable()
    {
        go_menuButton.onClick.AddListener(() =>
        {
            SetActive(false);

            PlacingTowerManager.Instance.SetAllTowerActiveFalse();

            EnemyWaveManager.Instance.RemoveAllEnemy();
            EnemyWaveManager.Instance.ResetWaveNumber();

            TimeManager.Instance.runTimer = false;
            TimeManager.Instance.ResetTimer();

            GamePlayUI.Instance.SetEnemyWaveUIActive(false);
            GamePlayUI.Instance.SetInteractUIActive(false);

            MainMenuUI.Instance.SetActive(true);

            Time.timeScale = 1.0f;
        });
    }

    //===========================================================================
    public void SetActive(bool newBool)
    {
        go_text.SetActive(newBool);
        go_menuButton.gameObject.SetActive(newBool);
    }
}