using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject zombiePrefab;

    [SerializeField]
    private float zombieInterval = 5f;

    [SerializeField]
    private GameObject player; // Reference to the player GameObject

    [SerializeField]
    private float spawnDistance = 30f; // Minimum distance from the player

    [SerializeField]
    private float spawnRange = 50f; // Range within which to spawn zombies

    void Start()
    {
        StartCoroutine(spawnEnemy(zombieInterval, zombiePrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        SpawnZombie(enemy);
        StartCoroutine(spawnEnemy(interval, enemy));
    }

    private void SpawnZombie(GameObject enemy)
    {
        if (player == null)
        {
            Debug.LogError("Player GameObject is not assigned.");
            return;
        }

        Vector3 spawnPosition = GetRandomSpawnPosition();
        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 randomPosition;
        float distance;

        do
        {
            // Generate a random position within the specified range
            float randomX = Random.Range(-spawnRange, spawnRange);
            float randomY = Random.Range(-spawnRange, spawnRange);
            randomPosition = new Vector3(randomX, randomY, 0);
            distance = Vector3.Distance(randomPosition, playerPosition);
        }
        while (distance < spawnDistance); // Ensure the position is at least `spawnDistance` away from the player

        return randomPosition;
    }
}
