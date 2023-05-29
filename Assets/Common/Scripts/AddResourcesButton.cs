using Assets.Common.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddResourcesButton : MonoBehaviour
{
    public GameObject PurchasePanel;
    public RewardType Type;
    private void Start()
    {
        Type = GetComponentInParent<ResourcesController>().Type;
    }

    public void ClickOnAddResourcesButton()
    {
        switch (Type)
        {
            case RewardType.Coins:
                SceneManager.LoadScene("Shop");
                break;
            case RewardType.Sapphires:
                CreatePurchasePanel();
                break;
            case RewardType.Topazes:
                CreatePurchasePanel();
                break;
            case RewardType.Emeralds:
                CreatePurchasePanel();
                break;
            case RewardType.MoonStones:
                SceneManager.LoadScene("Shop");
                break;
            default:
                break;
        }
    }

    public void CreatePurchasePanel()
    {
        var canvasTransform = GetComponentInParent<Canvas>();
        var purchasePanel = Instantiate(PurchasePanel, canvasTransform.transform);
        purchasePanel.GetComponent<AddResourcesPanel>().Type = Type;
    }
}
