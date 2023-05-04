using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public float spawnDistance = 10f; // The minimum distance from the player to spawn enemies
    public int minHp = 5; // The minimum hp for enemies
    public int maxHp = 15; // The maximum hp for enemies
    public float spawnCooldown = 2f; // The cooldown time between enemy spawns
    public Transform spawnArea; // The area in which to spawn enemies

    public Vector2 spawnPosition;

    private Transform playerTransform; // Reference to the player's transform
    private float lastSpawnTime; // The time of the last enemy spawn

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Find the player's transform
    }

    void Update()
    {
        setSpawnPoint();
        // Spawn enemies if the player is far enough away and the cooldown has elapsed
        if (Time.time > lastSpawnTime + spawnCooldown && Vector2.Distance(spawnPosition, playerTransform.position) > spawnDistance)
        {
            SpawnEnemy();
            lastSpawnTime = Time.time; // Reset the spawn cooldown
        }
    }

    // Spawn a random enemy at a random position within the spawn area
    private void setSpawnPoint()
    {
        spawnPosition = new Vector2(Random.Range(spawnArea.position.x - spawnArea.localScale.x / 2f, spawnArea.position.x + spawnArea.localScale.x / 2f),
                                    Random.Range(spawnArea.position.y - spawnArea.localScale.y / 2f, spawnArea.position.y + spawnArea.localScale.y / 2f)); // Randomize the spawn position within the spawn area
    }
    private void SpawnEnemy()
    {
        int hp = Random.Range(minHp, maxHp + 1); // Randomize the hp
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity); // Spawn the enemy
        enemy.GetComponent<Enemy>().hp = hp; // Set the enemy's hp
    }
}