using UnityEngine;

public class PlacingTowerManager : SingletonMonobehaviour<PlacingTowerManager>
{
    [SerializeField] private Transform towerPreview = default;

    [Header("ArchTower List:")]
    [SerializeField] private Transform[] archTowerList = default;

    //===========================================================================
    private void OnEnable()
    {
        SceneLoadManager.Instance.OnPurge += Instance_OnPurge;
    }

    private void OnDisable()
    {
        SceneLoadManager.Instance.OnPurge -= Instance_OnPurge;
    }

    //===========================================================================
    private void Instance_OnPurge(object sender, System.EventArgs e)
    {
        PlaceArchTower(LoadoutMenuUI.Instance.SelectedArchTowerIndex);
    }

    //===========================================================================
    private void PlaceArchTower(int towerIndex)
    {
        Instantiate(archTowerList[towerIndex]).position = new Vector3(37.5f, 37.5f);
    }

    //===========================================================================
    public void UpdateTowerPreviewPosition(Transform newPosition)
    {
        towerPreview.position = newPosition.position;
    }

    public void SetAllTowerActiveFalse()
    {
        foreach (Transform _tower in archTowerList)
        {
            if (_tower.gameObject.activeInHierarchy)
                _tower.gameObject.SetActive(false);
        }
    }
}