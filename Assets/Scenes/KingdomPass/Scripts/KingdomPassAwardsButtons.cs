using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KingdomPassAwardsButtons : MonoBehaviour
{
    public bool isAwardActivated = false;
    public bool isFreeAwardTaked = false;
    public bool isPremiumAwardTaked = false;
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
        if (isAwardActivated && !isFreeAwardTaked)
        {
            freeGrayCover.enabled = false;
            premiumGrayCover.enabled = false;
            freeTickImage.enabled = false;
            premiumTickImage.enabled = false;
            premiumAwardTakeButton.SetActive(true);
            freeAwardTakeButton.SetActive(true);
        }
        else if(!isAwardActivated && !isFreeAwardTaked)
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
        isAwardActivated = true;
    }
    public void TakeFreeAward()
    {
        isFreeAwardTaked = true;
    }
    public void TakePremiumAward()
    {
        if (kingdomPassManager.isKingdomPassActivated)
        {
            isPremiumAwardTaked = true;
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
        if (isFreeAwardTaked && isAwardActivated)
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
        if (isPremiumAwardTaked && isAwardActivated && kingdomPassManager.isKingdomPassActivated)
        {
            premiumTickImage.enabled = true;
            premiumGrayCover.enabled = false;
            premiumAwardTakeButton.SetActive(false);
            premiumAwardBackground.GetComponent<Image>().enabled = false;
            premiumAwardAmountText.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
    }
}
