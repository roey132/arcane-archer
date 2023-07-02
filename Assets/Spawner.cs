using System;
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Curves")]
    [SerializeField] private AnimationCurve _amountToSpawnCurve;
    [SerializeField] private AnimationCurve _valueToSpawnCurve;
    [SerializeField] private AnimationCurve _randomizerCurve;

    [Header("Variables")]
    [SerializeField] private float _maxRadius;
    [SerializeField] private float _minRadius;

    [Header("Refs")]
    [SerializeField] private Transform _enemyPool;
    private Collider2D _spawnArea;

    [Header("Debugging")]
    [SerializeField] private Vector2 _centerPoint;
    [SerializeField] private float _radius;
    [SerializeField] private Color _debugCircleColor;
    [SerializeField] private int _enemyCountForTests;
    [SerializeField] private float _cooldownBetweenSpawns;

    public static event Action<int> EnemySpawn;

    private void Start()
    {
        _spawnArea = GameObject.FindGameObjectWithTag("SpawnArea").GetComponent<Collider2D>();
    }

    public void SpawnerEvent(int maxEnemies, int enemyLevel)
    {
        // currently uses a set radius of 25, might need to change later
        Vector2 spawnerCenter = PointGenerator.GetRandomPointInArea(center:Vector2.zero, radius:25, spawnArea:_spawnArea);

        // get a random multiplier to generate a radius and an amount to spawn
        float randomMultiplier = UnityEngine.Random.Range(0f, 1f);

        float randomRadius = randomMultiplier * _maxRadius;
        float radius = Mathf.Clamp(randomRadius, _minRadius, _maxRadius);

        int amountToSpawn = Mathf.CeilToInt(_amountToSpawnCurve.Evaluate(randomMultiplier) * maxEnemies);

        DebugUtils.DrawDebugCircle(_centerPoint, _radius, _debugCircleColor,100f);

        for (int i = 0; i < amountToSpawn; i++)
        {
            StartCoroutine(DelayedSpawnEvent(spawnerCenter, radius, 100, enemyLevel, 0.1f * i));
        }
    }



    private IEnumerator DelayedSpawnEvent(Vector2 center, float radius, int maxEnemyValue, int enemyLevel, float timeToWaitSeconds)
    {
        yield return new WaitForSeconds(timeToWaitSeconds);
        SpawnEvent(center, radius, 100, enemyLevel);
    }

    private void SpawnEvent(Vector2 center, float radius, int maxEnemyValue, int enemyLevel)
    {
        Collider2D spawnArea = GameObject.FindGameObjectWithTag("SpawnArea").GetComponent<Collider2D>();

        GameObject enemyPrefab = EnemySpawnManager.Instance.GetEnemyObject(maxEnemyValue);
        if (enemyPrefab == null) return;

        Vector2 spawnPoint = PointGenerator.GetRandomPointInArea(center, radius, spawnArea);

        GameObject currEnemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity, _enemyPool);
        InitEnemy(currEnemy, enemyLevel);

        int enemyValue = EnemySpawnManager.Instance.GetEnemyValue(currEnemy);
        EnemySpawn?.Invoke(enemyValue);
    }

    private void InitEnemy(GameObject enemyObject, int enemyLevel)
    {
        EnemyStats currStats = enemyObject.transform.Find("EnemyStats").GetComponent<EnemyStats>();

        currStats.InitData(enemyLevel);
    }


}
