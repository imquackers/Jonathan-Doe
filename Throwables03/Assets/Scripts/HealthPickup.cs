using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int amount = 50;
    //pickup type 
    public bool startFullWave = false; //first waves
    public bool startBossFight = false; //boss fight
    public Transform singleEnemySpawnPoint;
    public Transform bossSpawnPoint;
    public bool finalHealthPackTrigger = false;
    public TileSpawner tileSpawner;
    public bool FakeWallTrigger = false;
    public GameObject FakeWalls;

    //enemy setup
    public GameObject enemyPrefab; //single spawning enemies
    public GameObject bossPrefab;
  public  void OnTriggerEnter(Collider collision)
    {
        // Only trigger for the player
        if (collision.GetComponent<Collider>().CompareTag("Player"))
        {
            // Get PlayerHealth component
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
             playerHealth.Heal(amount);
                if(FakeWallTrigger)
                {
                    FakeWalls.SetActive(true);
                }
                if (startFullWave)
                    {
                        WaveSpawner waveSpawner = FindFirstObjectByType<WaveSpawner>();
                        if (waveSpawner != null)
                        {
                            waveSpawner.BeginSpawning();
                        }
                    }
                else
                    {
                     if (enemyPrefab != null && singleEnemySpawnPoint != null)
                        {
                            Instantiate(enemyPrefab, singleEnemySpawnPoint.position, singleEnemySpawnPoint.rotation);
                        }
                    }

                    if (startBossFight)
                    {
                        if(bossPrefab != null && bossSpawnPoint != null)
                        {
                            Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);
                        }
                    }
                if (finalHealthPackTrigger && tileSpawner !=null)
                {
                    tileSpawner.SpawnTiles();
                }
                Destroy(gameObject);
            }
            WaveSpawner spawner = FindFirstObjectByType<WaveSpawner>();
            if (spawner != null)
            {
                spawner.BeginSpawning();
            }
        }
    
    }
}
