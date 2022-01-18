using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemisSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject EnemiePrefab;

    [SerializeField]
    float AsteroidSpawnTime = 2f;

    public bool  Spawning = true;

    void Start()
    {
        StartCoroutine(SpawningCoroutine());
    }

    IEnumerator SpawningCoroutine()
    {
        while (true)
        {
            while(Spawning)
            {
                SpawnEnemies();
                yield return new WaitForSeconds(AsteroidSpawnTime);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private void SpawnEnemies()
    {
       var enemies = Instantiate(EnemiePrefab, transform.position, Quaternion.identity);
    }
}
