using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject FakeWall;
    public Transform[] spawnPoints;

    public float timeBetweenSpawns = 0.3f;
    public float spawnHeightOffset = 2f;

    public bool spawningEnabled = false;

    private int enemiesAlive = 0;    // Track active enemies

    public void BeginSpawning()
    {
        if (!spawningEnabled)
        {
            spawningEnabled = true;
            StartCoroutine(SpawnWaves());
        }
    }

    IEnumerator SpawnWaves()
    {
        
        
                Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];

                Vector3 spawnPos = sp.position + Vector3.up * spawnHeightOffset;

                GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

                var e = enemy.GetComponent<Enemy>();
                if (e != null)
                {
                    e.OnEnemyDeath += EnemyDied; // ✅ subscribe to death event
                }

                enemiesAlive++;

                yield return new WaitForSeconds(timeBetweenSpawns);
    
    }

    // Called when an enemy dies
    void EnemyDied()
    {
        enemiesAlive--;

        // If all enemies dead open door
        if ( enemiesAlive == 0)
        {
        
              FakeWall.SetActive(false);
            
        }
    }
}
