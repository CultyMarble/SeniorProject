using UnityEngine;

public class GamePlayUI : SingletonMonobehaviour<GamePlayUI>
{
    [SerializeField] private GameObject enemyWaveUI = default;
    [SerializeField] private GameObject interactUI = default;
    [SerializeField] private GameObject shopUI = default;

    private bool isGameplay = default;

    //===========================================================================
    private void OnEnable()
    {
        SceneLoadManager.Instance.OnPurge += Instance_OnPurge;
    }

    private void Update()
    {
        if (isGameplay == false)
            return;

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (shopUI.activeInHierarchy)
            {
                SetShopUIActive(false);
            }
            else
            {
                SetShopUIActive(true);
            }
        }
    }

    private void OnDisable()
    {
        SceneLoadManager.Instance.OnPurge -= Instance_OnPurge;
    }

    //===========================================================================
    private void Instance_OnPurge(object sender, System.EventArgs e)
    {
        isGameplay = true;

        SetEnemyWaveUIActive(true);
        SetInteractUIActive(true);
    }

    //===========================================================================
    public void SetEnemyWaveUIActive(bool newBool)
    {
        enemyWaveUI.SetActive(newBool);
    }

    public void SetInteractUIActive(bool newBool)
    {
        interactUI.SetActive(newBool);
    }

    public void SetShopUIActive(bool newBool)
    {
        shopUI.SetActive(newBool);
    }
}