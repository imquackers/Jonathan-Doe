using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public GameObject WallChunk;   // prefab for the broken pieces
    public int chunkCount = 12;          // how many chunks spawn
    public float explosionForce = 4f;    // how strong the chunks scatter
    public float explosionRadius = 2f;   // scattering radius
    public float requiredImpactVelocity = 5f; // how hard tile must hit
    public GameObject breakEffect;


    void OnCollisionEnter(Collision collision)
    {
        // Check if the thing that hit the wall is a thrown tile
        if (collision.collider.CompareTag("Tile"))
        {
            // Check if the tile hit with enough force
            float impact = collision.relativeVelocity.magnitude;
            if (impact >= requiredImpactVelocity)
            {
                Break();
            }
        }
    }

    public void Break()
    {
        
        // destroy original wall
        Destroy(gameObject);
    }
}


