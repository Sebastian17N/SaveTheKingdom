using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightSummaryGameManager : MonoBehaviour
{
    public bool DidGamerWin = false;
    public GameObject WinLoseImage;
    public Sprite[] WinLoseImages;
    public GameObject SummaryImages;
    public float TimeToInactivateSummaryImages;

   [Header("Star Rating System")]    
    public GameObject[] AchievedStars;
    public Sprite StarGold;
    public Sprite StarGrey;
    public float BasicHealth;
    public float Health;
   
    [Header("Chest")]
    public GameObject Chest;
    public float TimeToActivateChest;

    [Header("Activate Buttons")]   
    public Button[] Buttons;
    public float TimeActivate;

    void Start()
    {
        Chest.SetActive(false);
        IterateButtons(false);
        ShowWinLoseImage();
        StarRatingSystem();
        StartCoroutine(InActivateSummaryImages());
        StartCoroutine(ActivateButton());
        StartCoroutine(ActivateChest());   
        
    }

    void Update()
    {
        
    }
    private void ShowWinLoseImage()
    {
        DidGamerWin = Convert.ToBoolean(PlayerPrefs.GetInt("DidGamerWin"));
        if(DidGamerWin)
        {
            WinLoseImage.gameObject.GetComponent<SpriteRenderer>().sprite = WinLoseImages[0];
        }
        else
        {
            WinLoseImage.gameObject.GetComponent<SpriteRenderer>().sprite = WinLoseImages[1];
            //WinLoseImage.transform.position = new Vector3( 0, 0.6f, 0);
        }

    }

    private void StarRatingSystem()
    {
        BasicHealth = PlayerPrefs.GetFloat("BasicHealth");
        Health = PlayerPrefs.GetFloat("Health");

        var DeadZoneHealthpercentage =
            float.Parse(Health.ToString()) / float.Parse(BasicHealth.ToString());
        if (DeadZoneHealthpercentage == 0)
        {
            AchievedStars[0].gameObject.GetComponent<SpriteRenderer>().sprite = StarGrey;
            AchievedStars[1].gameObject.GetComponent<SpriteRenderer>().sprite = StarGrey;
            AchievedStars[2].gameObject.GetComponent<SpriteRenderer>().sprite = StarGrey;
        }
        else if(DeadZoneHealthpercentage > 0f && DeadZoneHealthpercentage < 0.7f)
        {
            AchievedStars[0].gameObject.GetComponent<SpriteRenderer>().sprite = StarGold;
            AchievedStars[1].gameObject.GetComponent<SpriteRenderer>().sprite = StarGrey;
            AchievedStars[2].gameObject.GetComponent<SpriteRenderer>().sprite = StarGrey;
        }
        else if (DeadZoneHealthpercentage >= 0.7f && DeadZoneHealthpercentage < 1)
        {
            AchievedStars[0].gameObject.GetComponent<SpriteRenderer>().sprite = StarGold;
            AchievedStars[1].gameObject.GetComponent<SpriteRenderer>().sprite = StarGold;
            AchievedStars[2].gameObject.GetComponent<SpriteRenderer>().sprite = StarGrey;
        }
        else if(DeadZoneHealthpercentage == 1)
        {
            AchievedStars[0].gameObject.GetComponent<SpriteRenderer>().sprite = StarGold;
            AchievedStars[1].gameObject.GetComponent<SpriteRenderer>().sprite = StarGold;
            AchievedStars[2].gameObject.GetComponent<SpriteRenderer>().sprite = StarGold;
        }
    }
    IEnumerator ActivateButton()
    {
        yield return new WaitForSeconds(TimeActivate);
        IterateButtons(true);
    }
    IEnumerator ActivateChest()
    {
        yield return new WaitForSeconds(TimeToActivateChest);
        Chest.SetActive(true);
    }
    IEnumerator InActivateSummaryImages()
    {
        yield return new WaitForSeconds(TimeToInactivateSummaryImages);
        SummaryImages.SetActive(false);
    }
    private void IterateButtons(bool activate)
    {
        foreach (var item in Buttons)
        {
            item.gameObject.SetActive(activate);
        }
    }
}
