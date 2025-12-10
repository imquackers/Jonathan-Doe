using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public System.Action OnEnemyDeath;

    // movement
    public float moveSpeed = 2f;
    private Transform player;
    private Rigidbody rb;
    private float speedMultiplier = 1f;

    // health
    public int maxHealth = 50;
    private int currentHealth;

    // damage
    public int damageToPlayer = 10;
    public float damageCooldown = 1f;
    private float lastDamageTime = -1f;

    void Start()
    {
        currentHealth = maxHealth;
        player = FindClosestPlayer();


        rb = GetComponent<Rigidbody>();
        if (rb != null) rb.freezeRotation = true;
    }
    Transform FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Transform closest = null;
        float closestDist = Mathf.Infinity;

        foreach (GameObject p in players)
        {
            float dist = Vector3.Distance(transform.position, p.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = p.transform;
            }
        }

        return closest;
    }

    void FixedUpdate()
    {
        if (player == null || rb == null) return;

        Vector3 dir = (player.position - transform.position);
        dir.y = 0;
        dir.Normalize();

        // FIXED MOVEMENT
        rb.linearVelocity = dir * moveSpeed * speedMultiplier;
    }

    public void SetSpeedMultiplier(float mult)
    {
        speedMultiplier = mult;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0) Die();
    }

    public void Die()
    {
        OnEnemyDeath?.Invoke();

        ScoreManager scoreManager = Object.FindFirstObjectByType<ScoreManager>();
        if (scoreManager != null)
            scoreManager.AddScore(10);

        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            if (Time.time - lastDamageTime >= damageCooldown)
            {
                playerHealth.TakeDamage(damageToPlayer);
                lastDamageTime = Time.time;
            }
        }
    }
}
