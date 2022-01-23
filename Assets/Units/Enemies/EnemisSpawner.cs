using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEditor;
using System.Text.RegularExpressions;
using Random = UnityEngine.Random;

public class EnemisSpawner : MonoBehaviour
{
    public List<Enemy> enemiesPrefabs;
    public Enemy enemies;
    
    public float spawnInterval;
    public Transform[] spawnSpot;

    public void Start()
    {        
        LoadLevel("level_1");
    }

    private void Update()
    {
        

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
                    enemies = enemiesPrefabs[Random.Range(0, enemiesPrefabs.Count)];
                    Instantiate(enemiesPrefabs[(int)enemies.enemiesType], spawnSpot[y]);
                }             
            }
            yield return new WaitForSeconds(spawnInterval);
        }        
    }
    
   
}
