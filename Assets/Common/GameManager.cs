using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Attributes")]
    // Object template to get attributes from.
    public List<GameObject> UnitCards;

    [Header("Units")]
    public UnitScriptableObject[] ScriptableObjects;
    public GameObject Prefab;
	public Transform CardHolderTransform;
    public Transform BackgroundTransform;

    [Header("GameRuntime")]
    public int NumberOfEnemiesLeft;
    string LevelName;
    public float Health;
    public float BasicHealth;


    private void Start()
    {
        UnitCards = new List<GameObject>();

        // Load all units.
        foreach (UnitScriptableObject scriptableObject in ScriptableObjects)
        {
            UnitCards.Add(CreateUnit(scriptableObject));
        }

        // Load level
        GenerateLevel();
        StartCoroutine(CheckIfLevelEndCoroutine());
    }

    void GenerateLevel()
    {
        LevelName = PlayerPrefs.GetString("current_level");
        FindObjectOfType<EnemiesSpawner>().LoadLevel(LevelName);

        NumberOfEnemiesLeft = CountAllEnemiesOnLevel(LevelName);
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
        GameObject unit = Instantiate(Prefab, CardHolderTransform);
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

    IEnumerator CheckIfLevelEndCoroutine()
    {
        while (true)
        {
            if (NumberOfEnemiesLeft <= 0)
			{
                PlayerPrefs.SetInt(LevelName + "_finished", 1);
                PlayerPrefs.SetInt("DidGamerWin", 1);
                PlayerPrefs.SetFloat("Health", Health);
                SceneManager.LoadScene("FightSummary");
                break;
			}

            yield return new WaitForSeconds(1f);
        }
    }
}
