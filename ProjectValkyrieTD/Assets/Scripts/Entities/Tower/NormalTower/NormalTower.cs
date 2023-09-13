using UnityEngine;

public class NormalTower : MonoBehaviour
{
    [Header("Tower Settings:")]
    [SerializeField] private string towerName = default;
    public string TowerName => towerName;
}