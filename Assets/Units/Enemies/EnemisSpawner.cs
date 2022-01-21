using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
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

    private void Update()
    {
        

    }

    public IEnumerator SpawningCoroutine()
    {
        while (true)
        {
            enemies = enemiesPrefabs[Random.Range(0, enemiesPrefabs.Count)];

            var spawnSpotID = Random.Range(0, spawnSpot.Length);
            Instantiate(enemiesPrefabs[(int)enemies.enemiesType], spawnSpot[spawnSpotID]);

            yield return new WaitForSeconds(spawnInterval);
        }        
    }

    //foreach (Enemy enemy in enemies)
    //    {
    //        if (enemy.spawnTime <= Time.time)
    //        {
    //            Instantiate(enemiesPrefabs[(int)enemy.enemiesType], transform.GetChild(enemy.Spawner).transform);
    //        }
    //    }
    //{
    //    while (true)
    //    {
    //        while(Spawning)
    //        {
    //            SpawnEnemies();
    //            yield return new WaitForSeconds(SpawnTime);
    //        }
    //        yield return new WaitForEndOfFrame();
    //    }
    //}

    //[SerializeField]
    //GameObject EnemiePrefab;

    //[SerializeField]
    //float SpawnTime = 2f;

    //public bool Spawning = true;
    //public Enemy enemy;   

    //IEnumerator SpawningCoroutine()
    //{
    //    while (true)
    //    {
    //        while(Spawning)
    //        {
    //            SpawnEnemies();
    //            yield return new WaitForSeconds(SpawnTime);
    //        }
    //        yield return new WaitForEndOfFrame();
    //    }
    //}

}
