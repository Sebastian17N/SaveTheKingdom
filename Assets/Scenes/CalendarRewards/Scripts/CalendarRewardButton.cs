using Assets.Common.Enums;
using Assets.Common.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalendarRewardButton : MonoBehaviour
{
    public CalendarReward RewardType;

    #region Prefab Elements
    [HideInInspector]public Image greenBackground;
    private Animator anim;
    public Image awardGrayBackground;
    public Image awardImage;
    public Image tickImage;
    public Image grayCover;
    public TextMeshProUGUI dayNumberText;
    public TextMeshProUGUI awardAmountText;
    public int Id;
    #endregion
    private void Awake()
    {
        greenBackground = GetComponent<Image>();
        anim = GetComponent<Animator>();
    }
    public void AwardActivated()
    {
        if (RewardType.State == RewardState.Active)
        {
            greenBackground.enabled = true;
            grayCover.enabled = false;
            anim.enabled = true;
            tickImage.enabled = false;
            awardGrayBackground.GetComponent<Image>().color = Color.white;
        }
        else if (RewardType.State == RewardState.Inactive)
        {
            greenBackground.enabled = false;
            grayCover.enabled = false;
            anim.enabled = false;
            tickImage.enabled = false;
            awardGrayBackground.GetComponent<Image>().color = Color.white;
        }
    }
    public void AwardTaked()
    {
        if (RewardType.State == RewardState.Taken)
        {
            greenBackground.enabled = false;
            grayCover.enabled = true;
            awardGrayBackground.GetComponent<Image>().color = new Color(0.65f, 0.65f, 0.65f, 0.50f);
            anim.enabled = false;
            tickImage.enabled = true;
        }
    }
    public void AwardLoosed()
    {
        if (RewardType.State == RewardState.Loosed)
        {
            greenBackground.enabled = false;
            grayCover.enabled = true;
            awardGrayBackground.GetComponent<Image>().color = new Color(0.65f, 0.65f, 0.65f, 0.50f);
            anim.enabled = false;
            tickImage.enabled = false;
        }
    }
}
