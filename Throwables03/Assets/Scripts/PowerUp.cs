using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
    public float speedIncrease = 4f;    // adds this amount to original speed
    public float duration = 8f;
    public bool SpawnsEnemies = false;


    // enemy setup
    public GameObject enemyPrefab; 
    public Transform singleEnemySpawnPoint;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            FPSController player = collision.GetComponent<FPSController>();

            if (player != null)
            {
                StartCoroutine(ApplySpeedBoost(player));
            }
            if(SpawnsEnemies = true)
            // spawn a single enemy
            if (enemyPrefab && singleEnemySpawnPoint)
            {
                Instantiate(enemyPrefab, singleEnemySpawnPoint.position, singleEnemySpawnPoint.rotation);
            }
        }
        
    }

    private IEnumerator ApplySpeedBoost(FPSController player)
    {
        player.AddSpeed(speedIncrease);  // <-- increased speed
        yield return new WaitForSeconds(duration);
        player.ResetSpeed();             // back to normal
      Destroy(gameObject);  
    }
}
