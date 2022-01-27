using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;
using System;

public class EnemiesSpawner : MonoBehaviour
{
    public ScriptableEnemies[] enemiesPrefabs;
    public GameObject enemies;

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

                //if (character == 'x')
                //{
                //    enemies = enemiesPrefabs[0];
                //}
                //else if (character == 'y')
                //{
                //    enemies = enemiesPrefabs[1];
                //}
                //else if (character == 'z')
                //{
                //    enemies = enemiesPrefabs[2];
                //}
                GameObject enemy = Instantiate(enemies, spawnSpot[y]);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
        
    }

    //public List<Enemy> enemiesPrefabs;
    //public Enemy enemies;

    //public float spawnInterval;
    //public Transform[] spawnSpot;

    //public void Start()
    //{
    //    LoadLevel("level_1");
    //}

    //void LoadLevel(string name)
    //{
    //    var path = "Assets/Map/Levels/" + name + ".txt";
    //    var text = AssetDatabase.LoadAssetAtPath<TextAsset>(path).text;
    //    var lines = Regex.Split(text, Environment.NewLine);
    //    StartCoroutine(SpawningCoroutine(lines));
    //}

    //public IEnumerator SpawningCoroutine(string[] text)
    //{
    //    var spawnSpotID = text.Length;
    //    var numberOfEnemies = text[0].Length;

    //    for (int x = 0; x < numberOfEnemies; x++)
    //    {
    //        for (int y = 0; y < spawnSpotID; y++)
    //        {
    //            var character = text[y][x];

    //            if (character == '_')
    //                continue;

    //            if (character == 'x')
    //            {
    //                enemies = enemiesPrefabs[0];
    //            }
    //            else if (character == 'y')
    //            {
    //                enemies = enemiesPrefabs[1];
    //            }
    //            else if (character == 'z')
    //            {
    //                enemies = enemiesPrefabs[2];
    //            }
    //            var enemy = Instantiate(enemiesPrefabs[(int)enemies.enemiesType], spawnSpot[y]);
    //        }
    //        yield return new WaitForSeconds(spawnInterval);
    //    }

    //}
}
