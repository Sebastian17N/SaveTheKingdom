using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalendarReward : MonoBehaviour
{
    public bool isAwardActivated = false;
    public bool isAwardTaked = false;
    public bool isAwardLoosed = false;

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
    void Start()
    {
        greenBackground = GetComponent<Image>();
        anim = GetComponent<Animator>();
    }
    public void AwardActivated()
    {
        if (isAwardActivated && !isAwardTaked)
        {
            greenBackground.enabled = true;
            grayCover.enabled = false;
            anim.enabled = true;
            tickImage.enabled = false;
            awardGrayBackground.GetComponent<Image>().color = Color.white;
        }
        else if (!isAwardActivated && !isAwardTaked)
        {
            greenBackground.enabled = false;
            grayCover.enabled = false;
            anim.enabled = false;
            tickImage.enabled = false;
            awardGrayBackground.GetComponent<Image>().color = Color.white;
        }
    }
    public void AwardLoosed()
    {
        if (isAwardLoosed)
        {
            greenBackground.enabled = false;
            grayCover.enabled = true;
            awardGrayBackground.GetComponent<Image>().color = new Color(0.65f, 0.65f, 0.65f, 0.50f);
            anim.enabled = false;
            tickImage.enabled = false;
        }
    }
    public void AwardTaked()
    {
        if (isAwardTaked)
        {
            greenBackground.enabled = false;
            grayCover.enabled = true;
            awardGrayBackground.GetComponent<Image>().color = new Color(0.65f, 0.65f, 0.65f, 0.50f);
            anim.enabled = false;
            tickImage.enabled = true;
        }
    }
   
}
