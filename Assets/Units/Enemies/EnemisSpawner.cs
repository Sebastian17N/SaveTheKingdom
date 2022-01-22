using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemisSpawner : MonoBehaviour
{
    public List<Enemy> enemiesPrefabs;
    public Enemy enemies;
    
    public float spawnInterval;
    public Transform[] spawnSpot;

    public void Start()
    {
        StartCoroutine(SpawningCoroutine());
    }

    public IEnumerator SpawningCoroutine()
    {
        while (true)
        {
            enemies = enemiesPrefabs[Random.Range(0, enemiesPrefabs.Count)];

            var spawnSpotID = Random.Range(0, spawnSpot.Length);
            var enemy = Instantiate(enemiesPrefabs[(int)enemies.enemiesType], spawnSpot[spawnSpotID]);
            enemy.transform.localPosition = new Vector3(0, 0, -1);

            yield return new WaitForSeconds(spawnInterval);
        }        
    }
}
