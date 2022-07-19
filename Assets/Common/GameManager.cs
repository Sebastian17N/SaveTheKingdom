using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    string LevelName;
    public float Health;
    
    private void Start()
    {
        // Load all units.
        UnitCards = new List<GameObject>();        
        foreach (UnitScriptableObject scriptableObject in ScriptableUnitObjects)
        {
            UnitCards.Add(CreateUnit(scriptableObject));
        }

        SpellCard = new List<GameObject>();
        foreach (SpellScriptableObject scriptableObject in ScriptableSpellObject)
        {
            SpellCard.Add(CreateSpell(scriptableObject));
        }
        // Load level
        GenerateLevel();
        StartCoroutine(CheckIfLevelEndCoroutine());
    }

    void GenerateLevel()
    {
        LevelName = PlayerPrefs.GetString("CurrentLevel_EnemiesMap");
        FindObjectOfType<EnemiesSpawner>().LoadLevel(LevelName);
        NumberOfEnemiesLeft = CountAllEnemiesOnLevel(LevelName);

        var backgroundSpriteName = PlayerPrefs.GetString("CurrentLevel_MapBackground");
        var backgroundSpriteTexture = LoadTexture($"Assets/Map/Images/{backgroundSpriteName}");
        var backgroundSprite = Sprite.Create(backgroundSpriteTexture, new Rect(0, 0, backgroundSpriteTexture.width, backgroundSpriteTexture.height), new Vector2(0.5f, 0.5f), 100, 0, SpriteMeshType.Tight);

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
    /// Count number of enemies on the level, based on text file map.
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
        GameObject unit = Instantiate(UnitPrefab, UnitCardHolderTransform);
        unit.GetComponentInChildren<TMP_Text>().text = $"{unitScriptableObject.Cost}";

        var iconImage = unit.transform.Find("Icon").GetComponent<Image>();
        iconImage.sprite = unitScriptableObject.Sprite;

        var iconShadowImage = unit.transform.Find("IconShadow").GetComponent<Image>();
        iconShadowImage.sprite = unitScriptableObject.Sprite;

        var iconGoldImage = unit.transform.Find("IconGold").GetComponent<Image>();
        iconGoldImage.sprite = unitScriptableObject.Sprite;

        UnitCardManager manager = unit.GetComponent<UnitCardManager>();
        manager.UnitScriptableObject = unitScriptableObject;
        manager.Sprite = unitScriptableObject.Sprite;
        manager.CooldownImage = iconImage;
        manager.CooldownReadyImage = iconGoldImage;

        return unit;
    }


    private GameObject CreateSpell(SpellScriptableObject scriptableObject)
    {
        GameObject spell = Instantiate(SpellPrefab, SpellCardHolderTransform);
        spell.GetComponentInChildren<Image>().sprite = scriptableObject.Sprite;

        UseSpell manager = spell.GetComponent<UseSpell>();
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
                PlayerPrefs.SetInt(LevelName + "_finished", 1);
                PlayerPrefs.SetInt("DidGamerWin", 1);
                //PlayerPrefs.SetFloat("Health", Health);
                SceneManager.LoadScene("FightSummary");
                break;
			}

            yield return new WaitForSeconds(1f);
        }
    }
}
