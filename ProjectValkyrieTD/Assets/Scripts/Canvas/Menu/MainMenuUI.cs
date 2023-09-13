using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : SingletonMonobehaviour<MainMenuUI>
{
    [SerializeField] private GameObject mm_BGImage = default;
    [SerializeField] private Button mm_StartGameButton = default;

    //===========================================================================
    private void OnEnable()
    {
        mm_StartGameButton.onClick.AddListener(() =>
        {
            SetActive(false);
            LoadoutMenuUI.Instance.SetActive(true);
        });
    }

    //===========================================================================
    public void SetActive(bool newBool)
    {
        mm_BGImage.SetActive(newBool);
        mm_StartGameButton.gameObject.SetActive(newBool);
    }
}