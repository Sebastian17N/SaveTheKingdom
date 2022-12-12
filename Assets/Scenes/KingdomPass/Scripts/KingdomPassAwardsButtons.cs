using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Common.Models;
using Assets.Common.Enums;

public class KingdomPassAwardsButtons : MonoBehaviour
{
    public Reward RewardType;
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

    public void AwardActivated()
    {
        if (RewardType.State == RewardState.Active )
        {
            freeGrayCover.enabled = false;
            premiumGrayCover.enabled = false;
            freeTickImage.enabled = false;
            premiumTickImage.enabled = false;
            premiumAwardTakeButton.SetActive(true);
            freeAwardTakeButton.SetActive(true);
        }
        else if(RewardType.State == RewardState.Inactive)
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
        RewardType.State = RewardState.Active;
    }
    public void TakeFreeAward()
    {
        RewardType.State = RewardState.Taken;
    }
    public void TakePremiumAward()
    {
        if (kingdomPassManager.isKingdomPassActivated)
        {
            RewardType.State = RewardState.PremiumAwardTaken;
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
        if (RewardType.State == RewardState.Taken)
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
        if (RewardType.State == RewardState.PremiumAwardTaken && kingdomPassManager.isKingdomPassActivated)
        {
            premiumTickImage.enabled = true;
            premiumGrayCover.enabled = false;
            premiumAwardTakeButton.SetActive(false);
            premiumAwardBackground.GetComponent<Image>().enabled = false;
            premiumAwardAmountText.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
    }
}
