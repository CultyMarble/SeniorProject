using UnityEngine;

public class PrefabCustomTile : MonoBehaviour
{

    //===========================================================================
    private void OnMouseEnter()
    {
        PlacingTowerManager.Instance.UpdateTowerPreviewPosition(transform);

    }
}