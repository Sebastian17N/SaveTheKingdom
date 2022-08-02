using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scenes.FightSummary.Scripts
{
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
			StartCoroutine(ActivateChest());        
		}

		void Update()
		{
        
		}
		private void ShowWinLoseImage()
		{
			DidGamerWin = Convert.ToBoolean(PlayerPrefs.GetInt("DidGamerWin"));
			
			WinLoseImage.gameObject.GetComponent<SpriteRenderer>().sprite = 
				DidGamerWin 
					? WinLoseImages[0] 
					: WinLoseImages[1];
		}

		private void StarRatingSystem()
		{
			BasicHealth = PlayerPrefs.GetFloat("BasicHealth");
			Health = PlayerPrefs.GetFloat("Health");

			var deadZoneHealthPercentage = Health / BasicHealth;

			switch (deadZoneHealthPercentage)
			{
				case 0:
					AchievedStars[0].gameObject.GetComponent<SpriteRenderer>().sprite = StarGrey;
					AchievedStars[1].gameObject.GetComponent<SpriteRenderer>().sprite = StarGrey;
					AchievedStars[2].gameObject.GetComponent<SpriteRenderer>().sprite = StarGrey;
					break;

				case > 0f and < 0.7f:
					AchievedStars[0].gameObject.GetComponent<SpriteRenderer>().sprite = StarGold;
					AchievedStars[1].gameObject.GetComponent<SpriteRenderer>().sprite = StarGrey;
					AchievedStars[2].gameObject.GetComponent<SpriteRenderer>().sprite = StarGrey;
					break;

				case >= 0.7f and < 1:
					AchievedStars[0].gameObject.GetComponent<SpriteRenderer>().sprite = StarGold;
					AchievedStars[1].gameObject.GetComponent<SpriteRenderer>().sprite = StarGold;
					AchievedStars[2].gameObject.GetComponent<SpriteRenderer>().sprite = StarGrey;
					break;

				case 1:
					AchievedStars[0].gameObject.GetComponent<SpriteRenderer>().sprite = StarGold;
					AchievedStars[1].gameObject.GetComponent<SpriteRenderer>().sprite = StarGold;
					AchievedStars[2].gameObject.GetComponent<SpriteRenderer>().sprite = StarGold;
					break;
			}
		}  
		
		public IEnumerator ActivateChest()
		{
			yield return new WaitForSeconds(TimeToActivateChest);
			Chest.SetActive(true);
		}

		public void ActivateButton()
		{        
			IterateButtons(true);
		}

		public IEnumerator InActivateSummaryImages()
		{
			yield return new WaitForSeconds(TimeToInactivateSummaryImages);
			SummaryImages.SetActive(false);
		}

		public void IterateButtons(bool activate)
		{
			foreach (var item in Buttons)
			{
				item.gameObject.SetActive(activate);
			}
		}
	}
}