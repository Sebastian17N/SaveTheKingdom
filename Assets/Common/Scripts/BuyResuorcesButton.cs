using Assets.Common.Enums;
using System;
using TMPro;
using UnityEngine;

public class BuyResuorcesButton : MonoBehaviour
{
    public RewardType Type;
    public void BuyResuorces()
    {
        var amountBuyingText = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        var purchaseButton = transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;

        ResourcesMasterController.AddAndUpdateResources(RewardType.MoonStones, -Convert.ToInt32(purchaseButton));
        ResourcesMasterController.AddAndUpdateResources(Type, Convert.ToInt32(amountBuyingText));
    }
}
