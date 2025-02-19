using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn; // The prefab to spawn
    public float spawnInterval = 3f; // Time between spawns
    public float spawnRadius = 1.5f; // How far from the player to spawn
    public Transform playerTransform; // Reference to the player's transform

    private void Start()
    {
        //InvokeRepeating(nameof(SpawnPrefab), 0f, spawnInterval);
    }

    public void SpawnPrefab()
    {
        if (prefabToSpawn != null && playerTransform != null)
        {
            // Calculate a random spawn position near the player
            Vector3 spawnPosition = playerTransform.position + playerTransform.forward * spawnRadius;
            spawnPosition.y = playerTransform.position.y + 1f; // Keep it at player's height

            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
