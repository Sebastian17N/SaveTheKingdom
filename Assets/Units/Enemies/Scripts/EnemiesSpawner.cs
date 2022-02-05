using System.Collections;
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

	/// <summary>
	/// Load next level enemies map.
	/// </summary>
	/// <param name="fileLevelName">File name of level (without extension).</param>
	public void LoadLevel(string fileLevelName)
	{
		var path = $"Assets/Map/Levels/{fileLevelName}.txt";
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
				switch (text[y][x])
				{
					case 'x':
						CreateEnemyPrefab(enemyScriptableObjects[0], spawnSpot[y]);
						break;
					case 'y':
						CreateEnemyPrefab(enemyScriptableObjects[1], spawnSpot[y]);
						break;
					case 'z':
						CreateEnemyPrefab(enemyScriptableObjects[2], spawnSpot[y]);
						break;
					default:
						continue;
				}
			}

			yield return new WaitForSeconds(spawnInterval);
		}
	}

	private void CreateEnemyPrefab(ScriptableEnemy ScriptableEnemie, Transform spawnSpot)
	{
		var enemy = Instantiate(EnemyPrefab, spawnSpot);
		enemy.GetComponent<SpriteRenderer>().sprite = ScriptableEnemie.Sprite;
	}
}
