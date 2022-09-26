using System.Globalization;
using System.IO;
using Assets.Common;
using Assets.Common.JsonModel;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scenes.CampaignMap.Scripts
{
	public class BattleButton : MonoBehaviour
	{
		public string SceneGoIn;
		public string LevelName;

		[Header("Star Rating System")] public GameObject[] AchievedStars;
		public Sprite StarGold;
		public Sprite StarGrey;
		public float BasicHealth;
		public float Health;

		private MapsConfigJsonModel _mapsConfigJsonModel;

		private void Start()
		{
			_mapsConfigJsonModel = JsonLoader.LoadConfig(LevelName);
			StarRatingSystem();
			ActivateBattleButton();
		}
			

		private void ActivateBattleButton()
		{
			var isLevelAvailable = false;
			switch (LevelName)
			{
				case "Level_1":
					isLevelAvailable = true;
					break;
				case "":
					break;
				default:
					var levelNumber = int.Parse(LevelName.Substring("Level_".Length));
					isLevelAvailable = PlayerPrefs.GetInt($"Level_{levelNumber - 1}_finished", 0) == 1;
					break;
			}

			var boxCollider = GetComponent<BoxCollider2D>();
			var spriteRenderer = GetComponent<SpriteRenderer>();
			var stars = gameObject.transform.Find("Stars").gameObject;
			var padlock = gameObject.transform.Find("Padlock").gameObject;

			if (isLevelAvailable)
			{
				boxCollider.enabled = true;
				spriteRenderer.color = Color.white;
				padlock.SetActive(false);
				stars.SetActive(true);
			}
			else
			{
				boxCollider.enabled = false;
				spriteRenderer.color = Color.black;
				stars.SetActive(false);
			}
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

		private void OnMouseDown()
		{
			PlayerPrefs.SetString("CurrentLevel_EnemiesMap", _mapsConfigJsonModel.EnemiesMapFileName);
			PlayerPrefs.SetString("CurrentLevel_MapBackground", _mapsConfigJsonModel.SpriteBackgroundPath);

			PlayerPrefs.SetString("CurrentLevel", LevelName);

			SceneManager.LoadScene(SceneGoIn);
		}
	}
}