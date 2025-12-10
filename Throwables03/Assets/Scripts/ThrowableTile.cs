using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableTile : MonoBehaviour
{
    public float breakVelocityThreshold = 20f;   // how hard of a throw to break
    public GameObject breakEffectPrefab;        // particle effect (not made yet)
    public int damage = 25;                      // damage amount

    private bool thrown = false;

    // Call from PlayerPickup when tile thrown
    public void OnThrown()
    {
        thrown = true;
    }

   public  void OnCollisionEnter(Collision collision)
{
    // deal damage
    if (collision.collider.CompareTag("Enemy"))
    {
        var enemy = collision.collider.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }

    // If we hit a breakable wall, break it
    if (collision.collider.CompareTag("BreakableWall"))
    {
        var wall = collision.collider.GetComponent<BreakableWall>();
        if (wall != null)
        {
            wall.Break();
        }
    }

    // Break tile if thrown with enough force
    if (thrown)
    {
        float impact = collision.relativeVelocity.magnitude;
        if (impact >= breakVelocityThreshold)
        {
            Break();
        }
    }
}


    void Break()
    {
        // Spawn break effect if assigned
        if (breakEffectPrefab != null)
        {
            Instantiate(breakEffectPrefab, transform.position, Quaternion.identity);
        }

        // Destroy tile
        Destroy(gameObject);
    }
}
