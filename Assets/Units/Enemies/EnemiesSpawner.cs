using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;
using System;

public class EnemiesSpawner : MonoBehaviour
{
    public ScriptableEnemy[] enemyScriptableObjects;
    public GameObject EnemyPrefab;

    public float spawnInterval;
    public Transform[] spawnSpot;

    public void Start()
    {
        LoadLevel("level_1");
    }

    void LoadLevel(string name)
    {
        var path = "Assets/Map/Levels/" + name + ".txt";
        var text = AssetDatabase.LoadAssetAtPath<TextAsset>(path).text;
        var lines = Regex.Split(text, Environment.NewLine);
        StartCoroutine(SpawningCoroutine(lines));
    }

    public IEnumerator SpawningCoroutine(string[] text)
    {
        var spawnSpotID = text.Length;
        var numberOfEnemies = text[0].Length;
                 
        for (int x = 0; x < numberOfEnemies; x++)
        {
            for (int y = 0; y < spawnSpotID; y++)
            {
                var character = text[y][x];

                if (character == '_')
                    continue;

                if (character == 'x')
                {
                    CreateEnemyPrefab(enemyScriptableObjects[0]);
                }
                else if (character == 'y')
                {
                    CreateEnemyPrefab(enemyScriptableObjects[1]);
                }
                else if (character == 'z')
                {
                    CreateEnemyPrefab(enemyScriptableObjects[2]);
                }
                GameObject enemy = Instantiate(EnemyPrefab, spawnSpot[y]);
            }
            yield return new WaitForSeconds(spawnInterval);
        }        
    }

    private void CreateEnemyPrefab(ScriptableEnemy ScriptableEnemie)
    {
        EnemyPrefab.GetComponent<SpriteRenderer>().sprite = ScriptableEnemie.Sprite;
        EnemyPrefab.GetComponent<EnemyBasic>().Speed = ScriptableEnemie.Speed;
        EnemyPrefab.GetComponent<EnemyBasic>().EnemyHealth = ScriptableEnemie.EnemyHealth;
    }

}
