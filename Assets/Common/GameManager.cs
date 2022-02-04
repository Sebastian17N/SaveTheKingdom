using System;
using System.Collections;
using System.Collections.Generic;
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
    public int Cost;
    public Texture Icon;

    [Header("Units")]
    public UnitScriptableObject[] ScriptableObjects;
    public GameObject Prefab;
	public Transform CardHolderTransform;
    public Transform BackgroundTransform;

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
        var levelName = PlayerPrefs.GetString("current_level");
        FindObjectOfType<EnemiesSpawner>().LoadLevel(levelName);
    }

    /// <summary>
    /// Creates an unit card on UI.
    /// </summary>
    /// <param name="unitScriptableObject">Scripable object template.</param>
    /// <returns>Created game object.</returns>
    private GameObject CreateUnit(UnitScriptableObject unitScriptableObject)
    {
        GameObject unit = Instantiate(Prefab, CardHolderTransform);
        
        Icon = unitScriptableObject.Icon;
        Cost = unitScriptableObject.Cost;
        
        unit.GetComponentInChildren<RawImage>().texture = Icon;
        unit.GetComponentInChildren<TMP_Text>().text = $"{Cost}";

        UnitCardManager manager = unit.GetComponent<UnitCardManager>();
        manager.UnitScriptableObject = unitScriptableObject;
        manager.Sprite = unitScriptableObject.Sprite;
        //manager.BackgroundTransform = BackgroundTransform;

        return unit;
    }

    IEnumerator CheckIfLevelEndCoroutine()
    {
        while (true)
        {
            CheckIfLevelEnd();
            yield return new WaitForSeconds(1f);

        }
    }

    private void CheckIfLevelEnd()
    {
        var path = "Assets/Map/Levels/" + name + ".txt";
        var text = AssetDatabase.LoadAssetAtPath<TextAsset>(path).text;
        var lines = Regex.Split(text, Environment.NewLine);
        var levelLength = lines[0].Length;

        var numberOfEnemies = FindObjectsOfType<EnemyBasic>().Length;

        //jak to napisaæ? musi nast¹piæ ostatni element z listy level_???
        if (numberOfEnemies == 0) //&& lines[0].Length;
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
