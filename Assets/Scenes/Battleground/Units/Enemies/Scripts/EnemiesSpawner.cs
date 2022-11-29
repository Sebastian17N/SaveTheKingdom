using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Assets.Common;
using UnityEditor;
using UnityEngine;

namespace Assets.Units.Enemies.Scripts
{
	public class EnemiesSpawner : MonoBehaviour
	{
		private List<ScriptableEnemy> _enemyScriptableObjects;
		public GameObject EnemyPrefab;

		public float SpawnInterval;

		public void Start()
		{
			_enemyScriptableObjects = 
				ScriptableObjectLoader
					.LoadAllScriptableObjectsFromFolder("ScriptableEnemies")
					.Select(obj => obj as ScriptableEnemy)
					.ToList();
		}

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
							CreateEnemyPrefab(_enemyScriptableObjects[0], transform.GetChild(y));
							break;
						case 'y':
							CreateEnemyPrefab(_enemyScriptableObjects[1], transform.GetChild(y));
							break;
						case 'z':
							CreateEnemyPrefab(_enemyScriptableObjects[2], transform.GetChild(y));
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
				enemyBasic.Health = 40;
				enemyBasic.AttackSpeed = 1;
			}
		
		}
	}
}
