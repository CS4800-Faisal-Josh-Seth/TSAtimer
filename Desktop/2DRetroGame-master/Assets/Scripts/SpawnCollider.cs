using UnityEngine;

public class SpawnCollider : MonoBehaviour
{
    public GameObject colliderToSpawn;  // Drag the prefab of the collider to spawn here in the Inspector
    public float spawnDelay = 2f;  // Set the spawn delay in seconds in the Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider that entered was the player
        if (other.gameObject.tag == "Player")
        {
            // Invoke the SpawnNewCollider function after the specified delay
            Invoke("SpawnNewCollider", spawnDelay);
        }
    }

    private void SpawnNewCollider()
    {
        // Instantiate the collider prefab at the same position as this game object
        Instantiate(colliderToSpawn, transform.position, Quaternion.identity);
    }
}
