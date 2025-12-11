using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public GameObject FakeWall;
    public GameObject enemyPrefab;
    public GameObject BossPrefab;
    public Transform[] spawnPoints;
    public int baseEnemiesPerWave = 2;

    public float timeBetweenSpawns = 4f;
    public float timeBetweenWaves = 10f;
    public float speedIncreasePerWave = 0.1f;

    public float spawnHeightOffset = 2f;

    public bool spawningEnabled = false;

    private int waveNumber = 0;
    private int totalWaves = 3;      // 3 waves total
    private int enemiesAlive = 0;    // track active enemies

    public DoorController doorToOpen; // door

    public int GetCurrentWave()
    {
        return waveNumber;
    }

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
        while (waveNumber < totalWaves)
        {
            waveNumber++;
            int enemiesThisWave = baseEnemiesPerWave + (waveNumber - 1);
            float waveSpeedMultiplier = 1f + speedIncreasePerWave * (waveNumber - 1);

            for (int i = 0; i < enemiesThisWave; i++)
            {
                Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];

                Vector3 spawnPos = sp.position + Vector3.up * spawnHeightOffset;

                GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

                var e = enemy.GetComponent<Enemy>();
                if (e != null)
                {
                    e.SetSpeedMultiplier(waveSpeedMultiplier);
                    e.OnEnemyDeath += EnemyDied; // checking if all waves completed and enemies dead
                }

                enemiesAlive++;

                yield return new WaitForSeconds(timeBetweenSpawns);
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    // Called when an enemy dies
    void EnemyDied()
    {
        enemiesAlive--;

        // If all enemies dead open door
        if (waveNumber >= totalWaves && enemiesAlive <= 0)
        {
        
              FakeWall.SetActive(false);
            
        }
    }
}
