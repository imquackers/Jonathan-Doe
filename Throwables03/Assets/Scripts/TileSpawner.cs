using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public GameObject tilePrefab;  // pickable floor tile prefab
    public int rows = 70;
    public int columns = 30;
    public float spacing = 1.1f;
    float offsetX = 48f;
    float offsetZ = 0f;



    public void LastPickup()
    {

        SpawnTiles();
    }

    public void SpawnTiles()
    {
        for (int x = 0; x < columns; x++)
        {
            for (int z = 0; z < rows; z++)
            {
                Vector3 pos = new Vector3(offsetX + x * spacing, 0, offsetZ + z * spacing);
                GameObject tile = Instantiate(tilePrefab, pos, Quaternion.identity);

                // Ensure collider is enabled and Rigidbody is kinematic (optional)
                Collider col = tile.GetComponent<Collider>();
                if (col == null) tile.AddComponent<BoxCollider>();

                Rigidbody rb = tile.GetComponent<Rigidbody>();
                if (rb == null) rb = tile.AddComponent<Rigidbody>();
                rb.isKinematic = true;  // static until picked up
            }
        }
    }
}



