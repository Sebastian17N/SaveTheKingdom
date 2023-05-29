using Assets.Common.Enums;
using TMPro;
using UnityEngine;

public class DailyCoinsOffer : MonoBehaviour
{
    public int BuyCoinsNumber;
    public int CostCoinsNumber;
    public TextMeshProUGUI BuyCoinsNumberText;
    public TextMeshProUGUI CostCoinsNumberText;

    private void Start()
    {
        BuyCoinsNumberText.text = $"x {BuyCoinsNumber}";
        CostCoinsNumberText.text = $"BUY {CostCoinsNumber}";
    }

    public void BuyCoins()
    {
        ResourcesMasterController.AddAndUpdateResources(RewardType.Coins, BuyCoinsNumber);
        ResourcesMasterController.AddAndUpdateResources(RewardType.MoonStones, -CostCoinsNumber);
    }
}
