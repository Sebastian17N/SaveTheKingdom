using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Common.Models;
using Assets.Common.Enums;

public class KingdomPassRewardsButtons : MonoBehaviour
{
    public Reward RegularRewardType;
    public Reward PremiumRewardType;
    public KingdomPassManager kingdomPassManager;
    public int passPointsRequiredToActivateAward;

    #region Prefab Elements
    public TextMeshProUGUI ordinalNumberText;

    [Header("Free Award")]
    public Image freeAwardBackground;
    public Image freeAwardImage;
    public GameObject freeAwardTakeButton;
    public TextMeshProUGUI freeAwardAmountText;
    public Image freeTickImage;
    public Image freeGrayCover;

    [Header("Premium Award")]
    public Image premiumAwardBackground;
    public Image premiumAwardImage;
    public TextMeshProUGUI premiumAwardAmountText;
    public Image premiumTickImage;
    public Image premiumGrayCover;
    public GameObject premiumAwardTakeButton;
    public Image padlockImage;
    #endregion

    void Start()
    {
        kingdomPassManager = FindObjectOfType<KingdomPassManager>();
    }

    public void AwardActive()
    {
        if (RegularRewardType.State == RewardState.Active && PremiumRewardType.State == RewardState.Active)
        {
            freeGrayCover.enabled = false;
            premiumGrayCover.enabled = false;
            freeTickImage.enabled = false;
            premiumTickImage.enabled = false;
            premiumAwardTakeButton.SetActive(true);
            freeAwardTakeButton.SetActive(true);
        }
        else if(RegularRewardType.State == RewardState.Inactive && PremiumRewardType.State == RewardState.Inactive)
        {
            freeGrayCover.enabled = true;
            premiumGrayCover.enabled = true;
            freeTickImage.enabled = false;
            premiumTickImage.enabled = false;
            premiumAwardTakeButton.SetActive(false);
            freeAwardTakeButton.SetActive(false);
        }
    }
    public void ActivateAward()
    {
        RegularRewardType.State = RewardState.Active;
        PremiumRewardType.State = RewardState.Active;
    }
    public void TakeFreeAward()
    {
        RegularRewardType.State = RewardState.Taken;
        ResourcesMasterController.AddAndUpdateResources(RegularRewardType.Type, RegularRewardType.Amount);
    }
    public void TakePremiumAward()
    {
        if (kingdomPassManager.isKingdomPassActivated)
        {
            PremiumRewardType.State = RewardState.Taken;
            ResourcesMasterController.AddAndUpdateResources(PremiumRewardType.Type, PremiumRewardType.Amount);
        }
    }
    public void KingdomPassActivated()
    {
        if (kingdomPassManager.isKingdomPassActivated)
        {
            premiumAwardTakeButton.GetComponent<Image>().color = Color.white;
            premiumAwardTakeButton.GetComponentInChildren<TextMeshProUGUI>().text = "CLAIM";
            padlockImage.enabled = false;
        }
    }
    public void FreeAwardTaked()
    {
        if (RegularRewardType.State == RewardState.Taken)
        {
            freeTickImage.enabled = true;
            freeGrayCover.enabled = false;
            freeAwardTakeButton.SetActive(false);
            freeAwardBackground.GetComponent<Image>().enabled = false;
            freeAwardAmountText.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
    }
    public void PremiumAwardTaked()
    {
        if (PremiumRewardType.State == RewardState.Taken && kingdomPassManager.isKingdomPassActivated)
        {
            premiumTickImage.enabled = true;
            premiumGrayCover.enabled = false;
            premiumAwardTakeButton.SetActive(false);
            premiumAwardBackground.GetComponent<Image>().enabled = false;
            premiumAwardAmountText.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
    }
}
