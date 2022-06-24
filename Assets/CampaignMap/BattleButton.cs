using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleButton : MonoBehaviour
{
    public string SceneGoIn;
    public string LevelName;

    [Header("Star Rating System")]
    public GameObject[] AchievedStars;
    public Sprite StarGold;
    public Sprite StarGrey;
    public float BasicHealth;
    public float Health;

    private void Start()
    {
        StarRatingSystem();
        ActivateBattleButton();
    }

    private void ActivateBattleButton()
    {
        var levelFinished = PlayerPrefs.GetInt(LevelName + "_finished", 0) != 0;
        var boxCollider = GetComponent<BoxCollider2D>();
        var spriteRenderer = GetComponent<SpriteRenderer>();
        var stars = gameObject.transform.Find("Stars").gameObject;
        var padlock = gameObject.transform.Find("Padlock").gameObject;

        if (!levelFinished)
        {
            boxCollider.enabled = false;
            spriteRenderer.color = Color.black;
            stars.SetActive(false);
        }
        else
        {
            boxCollider.enabled = true;
            spriteRenderer.color = Color.white;
            padlock.SetActive(false);
            stars.SetActive(true);
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
        else if (DeadZoneHealthpercentage > 0f && DeadZoneHealthpercentage < 0.7f)
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
        else if (DeadZoneHealthpercentage == 1)
        {
            AchievedStars[0].gameObject.GetComponent<SpriteRenderer>().sprite = StarGold;
            AchievedStars[1].gameObject.GetComponent<SpriteRenderer>().sprite = StarGold;
            AchievedStars[2].gameObject.GetComponent<SpriteRenderer>().sprite = StarGold;
        }
    }
    private void OnMouseDown()
    {
        SceneManager.LoadScene(SceneGoIn);
    }    
}
