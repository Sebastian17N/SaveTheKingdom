using Assets.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
		public GameObject[] Chests;
		
		public float TimeToActivateChest;
		public int CoinsAward;
		public (string type, int quantity) GemsAward;
		public (int unitId, int quantity) ShardsAward;

		public GameObject UnitShards;

		[Header("Activate Buttons")]   
		public Button[] Buttons;
		//public float TimeActivate;

		void Start()
		{
			Chest.SetActive(false);
			IterateButtons(false);
			ShowWinLoseImage();
			StarRatingSystem();
			StartCoroutine(InActivateSummaryImages());        
			StartCoroutine(ActivateChest());        
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
			AchievedStars[0].gameObject.GetComponent<SpriteRenderer>().sprite = StarGrey;
			AchievedStars[1].gameObject.GetComponent<SpriteRenderer>().sprite = StarGrey;
			AchievedStars[2].gameObject.GetComponent<SpriteRenderer>().sprite = StarGrey;

			var howManyStars = 0;
			switch (deadZoneHealthPercentage)
			{
				case > 0f and < 0.5f:
					howManyStars = 1;
					break;

				case >= 0.5f and < 1:
					howManyStars = 2;
					break;

				case 1:
					howManyStars = 3;
					break;
			}

			for (var i = 0; i < howManyStars; i++)
			{
				AchievedStars[i].gameObject.GetComponent<SpriteRenderer>().sprite = StarGold;
				Chest = Chests[i];
			}

			if (howManyStars > 0)
            {
				var level = PlayerPrefs.GetString("CurrentLevel");
				var mapsConfigJsonModel = JsonLoader.LoadConfig(level);				
				CoinsAward = mapsConfigJsonModel.AwardCoins[howManyStars - 1];
				GemsAward = 
					(mapsConfigJsonModel.AwardGemsType, mapsConfigJsonModel.AwardGemsNumber[howManyStars - 1]);

				var shard = mapsConfigJsonModel.AwardShards.ToList().Single(shard => shard.FirstWin);
				ShardsAward = (shard.UnitId, shard.MinRange[0]);
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
