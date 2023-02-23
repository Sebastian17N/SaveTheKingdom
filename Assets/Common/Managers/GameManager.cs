using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Assets.Common.JsonModel;
using Assets.Map.Scripts;
using Assets.Scenes.SpiritMountain.Scripts;
using Assets.Units.Defenses.Scripts;
using Assets.Units.Enemies.Scripts;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Common.Managers
{
	public class GameManager : MonoBehaviour
	{
		//[Header("Attributes")]    
		[Header("Units")]
		public List<GameObject> UnitCards;
		public UnitScriptableObject[] ScriptableUnitObjects;
		public GameObject UnitPrefab;
		public Transform UnitCardHolderTransform;
		public Transform BackgroundTransform;

		[Header("Spell")]
		public List<GameObject> SpellCard;
		public SpellScriptableObject[] ScriptableSpellObject;
		public GameObject SpellPrefab;
		public Transform SpellCardHolderTransform;

		[Header("GameRuntime")]
		public int NumberOfEnemiesLeft;
		string _levelName;
		public float Health;

		public float DamageDealtOnCurrentLevel = 0;
    
		private void Start()
		{
			var listOfChosenUnits = PlayerPrefs.GetString("UnitChosenToBattle").Split(";");
			var listOfChosenSpells = PlayerPrefs.GetString("SpellChosenToBattle").Split(";");

			// Load all units.
			UnitCards = new List<GameObject>();        
			foreach (var scriptableObject in ScriptableUnitObjects)
			{
				if (!listOfChosenUnits.Contains(scriptableObject.UnitId.ToString()))
					continue;

				UnitCards.Add(CreateUnit(scriptableObject));
			}

			// Load all spells.
			SpellCard = new List<GameObject>();
			foreach (var scriptableObject in ScriptableSpellObject)
			{
				if (!listOfChosenSpells.Contains(scriptableObject.SpellId.ToString()))
					continue;

				SpellCard.Add(CreateSpell(scriptableObject));
			}
			// Load level
			GenerateLevel();
			StartCoroutine(CheckIfLevelEndCoroutine());
		}

		void GenerateLevel()
		{
			_levelName = PlayerPrefs.GetString("CurrentLevel_EnemiesMap");
			FindObjectOfType<EnemiesSpawner>().LoadLevel(_levelName);
			NumberOfEnemiesLeft = CountAllEnemiesOnLevel(_levelName);

			var backgroundSpriteName = PlayerPrefs.GetString("CurrentLevel_MapBackground");
			var backgroundSpriteTexture = LoadTexture($"Assets/Map/Images/{backgroundSpriteName}");
			var backgroundSprite = Sprite.Create(backgroundSpriteTexture, 
				new Rect(0, 0, backgroundSpriteTexture.width, backgroundSpriteTexture.height), new Vector2(0.5f, 0.5f), 100, 0, SpriteMeshType.Tight);

			GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite = backgroundSprite;
		}

		public static Texture2D LoadTexture(string filePath)
		{
			if (!File.Exists(filePath))
				return null;
		    
			var fileData = File.ReadAllBytes(filePath);
			var texture = new Texture2D(2, 2);

			return 
				!texture.LoadImage(fileData) 
					? null 
					: texture;
		}

		/// <summary>
		/// Amount number of enemies on the level, based on text file map.
		/// </summary>
		/// <param name="levelName">Filename level map.</param>
		/// <returns>Number of enemies on a template map.</returns>
		private int CountAllEnemiesOnLevel(string levelName)
		{
			var path = $"Assets/Map/Levels/{levelName}.txt";
			var text = AssetDatabase.LoadAssetAtPath<TextAsset>(path).text;
			var lines = Regex.Split(text, Environment.NewLine);

			var enemiesAmount = 0;

			foreach (var line in lines)
				enemiesAmount += line.Where(field => field != '_').Count();

			return enemiesAmount;
		}

		/// <summary>
		/// Creates an unit card on UI.
		/// </summary>
		/// <param name="unitScriptableObject">Scripable object template.</param>
		/// <returns>Created game object.</returns>
		private GameObject CreateUnit(UnitScriptableObject unitScriptableObject)
		{
			var unit = Instantiate(UnitPrefab, UnitCardHolderTransform);
			unit.GetComponentInChildren<TMP_Text>().text = $"{unitScriptableObject.Cost}";

			var iconImage = unit.transform.Find("Icon").GetComponent<Image>();
			iconImage.sprite = unitScriptableObject.Icon;

			var iconShadowImage = unit.transform.Find("IconShadow").GetComponent<Image>();
			iconShadowImage.sprite = unitScriptableObject.Icon;

			var iconGoldImage = unit.transform.Find("IconGold").GetComponent<Image>();
			iconGoldImage.sprite = unitScriptableObject.Icon;

			var manager = unit.GetComponent<UnitCardManager>();
			manager.UnitScriptableObject = unitScriptableObject;
			manager.Sprite = unitScriptableObject.Sprite;
			manager.CooldownImage = iconImage;
			manager.CooldownReadyImage = iconGoldImage;

			return unit;
		}


		private GameObject CreateSpell(SpellScriptableObject scriptableObject)
		{
			var spell = Instantiate(SpellPrefab, SpellCardHolderTransform);
			spell.GetComponentInChildren<Image>().sprite = scriptableObject.Sprite;

			var manager = spell.GetComponent<UseSpell>();
			manager.SpellScriptableObject = scriptableObject;
			manager.Sprite = scriptableObject.Sprite;

			return spell;
		}

		IEnumerator CheckIfLevelEndCoroutine()
		{
			while (true)
			{
				if (NumberOfEnemiesLeft <= 0)
				{
					PlayerPrefs.SetInt(_levelName + "_finished", 1);
					PlayerPrefs.SetInt("DidGamerWin", 1);
					//PlayerPrefs.SetFloat("BasicHealth", BasicHealth);
					SceneManager.LoadScene("FightSummary");

					PlayerPreferences.LogGatherAchievements(DamageDealtOnCurrentLevel, Scenes.Quests.Scripts.QuestType.DamageDealt);
					break;
				}

				yield return new WaitForSeconds(1f);
			}
		}
		

	}
}
