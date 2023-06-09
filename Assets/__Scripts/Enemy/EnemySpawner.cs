using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public List<GameObject> ActiveEnemies = new();
    public float spawnDistance = 10f; // The minimum distance from the player to spawn enemies
    public Transform spawnArea; // The area in which to spawn enemies

    public Vector2 spawnPosition;

    private Transform playerTransform; // Reference to the player's transform
    private float spawnTimer; // The time of the last enemy spawn

    private bool _inCombatRoom;

    private void Awake()
    {
        GameManager.OnGameStateChange += InCombat;
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= InCombat;
    }

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Find the player's transform
    }

    void Update()
    {
        if (!_inCombatRoom) return;

        spawnTimer -= Time.deltaTime;
        setSpawnPoint();

        // Spawn enemies if the player is far enough away and the cooldown has elapsed
        if (spawnTimer <= 0 && Vector2.Distance(spawnPosition, playerTransform.position) > spawnDistance)
        {
            SpawnEnemy();
            spawnTimer = EnemySpawnManager.Instance.WaveSpawnCooldown; // Reset the spawn cooldown
        }

        if (ActiveEnemies.Count != 0)
        {
            bool allNull = true;
            foreach (GameObject obj in ActiveEnemies)
            {
                if (obj != null)
                {
                    allNull = false;
                }
            }
            if (allNull) 
            {
                EnemySpawnManager.Instance.EndWave();
            };
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
        //EnemyData enemyData = WaveManager.Instance.GetEnemyData();
        //if (enemyData == null) return;
        
        //GameObject enemy = Instantiate(EnemyPrefab, spawnPosition, Quaternion.identity); // Spawn the enemy
        //enemy.transform.Find("EnemyStats").GetComponent<EnemyStats>().InitData(enemyData);
        //ActiveEnemies.Add(enemy);
        
    }
    private void InCombat(GameState state)
    {
        if (state == GameState.InCombat)
        {
            ActiveEnemies = new();
            _inCombatRoom = true;
            return;
        }

        _inCombatRoom = false;
    }
}