using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public GameObject GameOverPanel;
    public Slider healthSlider; 

    void Start()
    {
        GameOverPanel.SetActive(false);
        currentHealth = 50;
        UpdateHealthUI();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        Debug.Log("damage: " + amount + " | health: " + currentHealth);

       UpdateHealthUI();
       
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Heal(int amount)
{
     currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UpdateHealthUI();
        
         
}
 void UpdateHealthUI()
{
    healthSlider.value = currentHealth;
}


    void Die()
{
    GameOverPanel.SetActive(true);
    Debug.Log("died");

    ScoreManager scoreManager = Object.FindFirstObjectByType<ScoreManager>();
    WaveSpawner waveSpawner = Object.FindFirstObjectByType<WaveSpawner>();

    int finalScore = scoreManager != null ? scoreManager.score : 0;
    int finalWave = waveSpawner != null ? waveSpawner.GetCurrentWave() : 0;

    GameOverMenu gameOver = Object.FindFirstObjectByType<GameOverMenu>();
    if (gameOver != null)
        gameOver.ShowGameOver(finalScore, finalWave);
}

}


