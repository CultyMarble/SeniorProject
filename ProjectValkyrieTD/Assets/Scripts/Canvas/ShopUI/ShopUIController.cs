using UnityEngine;
using UnityEngine.UI;

public class ShopUIController : MonoBehaviour
{
    [SerializeField] private Button item01BuyButton = default;
    [SerializeField] private Button item02BuyButton = default;
    [SerializeField] private Button item03BuyButton = default;
    [SerializeField] private Button item04BuyButton = default;
    [SerializeField] private Button item05BuyButton = default;
    [SerializeField] private Button item06BuyButton = default;
    [SerializeField] private Button item07BuyButton = default;
    [SerializeField] private Button item08BuyButton = default;
    [SerializeField] private Button item09BuyButton = default;

    //===========================================================================
    private void Awake()
    {
        item01BuyButton.onClick.AddListener(() => InventoryManager.Instance.AddItemToTowerInventory(10001));

        item02BuyButton.onClick.AddListener(() => InventoryManager.Instance.AddItemToTowerInventory(10002));

        item03BuyButton.onClick.AddListener(() => InventoryManager.Instance.AddItemToTowerInventory(10003));

        item04BuyButton.onClick.AddListener(() => InventoryManager.Instance.AddItemToTowerInventory(10004));

        item05BuyButton.onClick.AddListener(() => InventoryManager.Instance.AddItemToTowerInventory(10005));

        item06BuyButton.onClick.AddListener(() => InventoryManager.Instance.AddItemToTowerInventory(10006));

        item07BuyButton.onClick.AddListener(() => InventoryManager.Instance.AddItemToTowerInventory(10007));

        item08BuyButton.onClick.AddListener(() => InventoryManager.Instance.AddItemToTowerInventory(10008));

        item09BuyButton.onClick.AddListener(() => InventoryManager.Instance.AddItemToTowerInventory(10009));
    }
}