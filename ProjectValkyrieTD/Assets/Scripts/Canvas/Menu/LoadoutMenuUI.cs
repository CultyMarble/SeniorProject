using UnityEngine;
using UnityEngine.UI;

public class LoadoutMenuUI : SingletonMonobehaviour<LoadoutMenuUI>
{
    [SerializeField] private Image lo_towerPreviewImage = default;
    [SerializeField] private Button lo_SelectPrevButton = default;
    [SerializeField] private Button lo_SelectNextButton = default;
    [SerializeField] private Button lo_PurgeButton = default;

    [Header("ArchTower Select Preview:")]
    [SerializeField] private Sprite[] sprites = default;

    private int selectedArchTowerIndex = default;
    public int SelectedArchTowerIndex => selectedArchTowerIndex;

    //===========================================================================
    private void OnEnable()
    {
        lo_SelectPrevButton.onClick.AddListener(() =>
        {
            UpdateArchTowerIndex(-1);
        });

        lo_SelectNextButton.onClick.AddListener(() =>
        {
            UpdateArchTowerIndex(1);
        });

        lo_PurgeButton.onClick.AddListener(() =>
        {
            SceneLoadManager.Instance.LoadTestingMap();
            SetActive(false);
        });
    }

    //===========================================================================
    private void UpdateArchTowerIndex(int indexToAdd)
    {
        selectedArchTowerIndex += indexToAdd;

        if (selectedArchTowerIndex < 0)
        {
            selectedArchTowerIndex = 0;
        }
        else if (selectedArchTowerIndex > 2)
        {
            selectedArchTowerIndex = 2;
        }

        // Update Preview Image
        lo_towerPreviewImage.sprite = sprites[selectedArchTowerIndex];
    }

    //===========================================================================
    public void SetActive(bool newBool)
    {
        lo_towerPreviewImage.gameObject.SetActive(newBool);
        lo_SelectPrevButton.gameObject.SetActive(newBool);
        lo_SelectNextButton.gameObject.SetActive(newBool);
        lo_PurgeButton.gameObject.SetActive(newBool);
    }
}
