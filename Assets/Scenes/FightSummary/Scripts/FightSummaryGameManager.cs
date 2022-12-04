using Assets.Common;
using System;
using System.Collections;
using System.Linq;
using Assets.Common.Enums;
using Assets.Common.JsonModel;
using Assets.Common.Managers;
using Assets.Common.Models;
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

		[Header("Chest")]
		public GameObject Chest;
		public GameObject[] Chests;
		
		public float TimeToActivateChest;
		public int CoinsAward;
		public Reward RewardAward = new();
		public Shards ShardsAward;

		public GameObject UnitShards;

		[Header("Activate Buttons")]   
		public Button[] Buttons;

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
			var basicHealth = PlayerPrefs.GetFloat("BasicHealth");
			var health = PlayerPrefs.GetFloat("Health");

			var deadZoneHealthPercentage = health / basicHealth;
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
				RewardAward.Type = mapsConfigJsonModel.AwardGemsType;
				RewardAward.Amount = mapsConfigJsonModel.AwardGemsNumber[howManyStars - 1];

				var shard = mapsConfigJsonModel.AwardShards.ToList().Single(shard => shard.FirstWin);
				ShardsAward = new Shards(shard.UnitId, shard.MinRange[0]);

				var playerPreferences = PlayerPreferences.Load();
				playerPreferences.Coins += CoinsAward;
				playerPreferences.AddReward = RewardAward;
				playerPreferences.AddShards = ShardsAward;
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
