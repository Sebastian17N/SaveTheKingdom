using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Assets.Units.Enemies.Scripts
{
	public class EnemiesSpawner : MonoBehaviour
	{
		public ScriptableEnemy[] EnemyScriptableObjects;
		public GameObject EnemyPrefab;

		public float SpawnInterval;

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
			var spawnSpotId = text.Length;
			var numberOfEnemies = text[0].Length;
			
			for (var x = 0; x < numberOfEnemies; x++)
			{
				for (var y = 0; y < spawnSpotId; y++)
				{
					switch (text[y][x])
					{
						case 'x':
							CreateEnemyPrefab(EnemyScriptableObjects[0], transform.GetChild(y));
							break;
						case 'y':
							CreateEnemyPrefab(EnemyScriptableObjects[1], transform.GetChild(y));
							break;
						case 'z':
							CreateEnemyPrefab(EnemyScriptableObjects[2], transform.GetChild(y));
							break;
						default:
							continue;
					}
				}

				yield return new WaitForSeconds(SpawnInterval);
			}
		}

		private void CreateEnemyPrefab(ScriptableEnemy scriptableEnemie, Transform spawnSpot)
		{
			var enemy = Instantiate(EnemyPrefab, spawnSpot);
			enemy.GetComponent<SpriteRenderer>().sprite = scriptableEnemie.Sprite;

			// TODO: Move it to scriptable object.
			{
				var enemyBasic = enemy.GetComponent<EnemyBasic>();
				enemyBasic.Health = 10;
				enemyBasic.AttackSpeed = 1;
			}
		
		}
	}
}
