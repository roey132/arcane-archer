using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
    [SerializeField] private float TimePassed;
    private float _nextSpawnTime;
    private Vector2 _testPoint;
    private void Start()
    {
        
        _spawnArea = GameObject.FindGameObjectWithTag("SpawnArea").GetComponent<Collider2D>();
        
    }

    public float SpawnerEvent(int maxEnemies, int enemyLevel, bool isTimed)
    {
        Vector2 spawnerCenter = GetValidSpawnPoint(Vector2.zero, 25, _spawnArea);
        // limit the max random value for the first 10 seconds of a room
        float randomLimit = _randomizerCurve.Evaluate(TimePassed / 10);
        // get a random multiplier to generate a radius and an amount to spawn
        float randomMultiplier = Random.Range(randomLimit, 0);

        float randomRadius = randomMultiplier * _maxRadius;
        float radius = Mathf.Clamp(randomRadius, _minRadius, _maxRadius);

        return 0f;
    }

    public float AmountSpawnerEvent(int maxEnemies, int enemyLevel, float randomMultiplier, Vector2 spawnerCenter, float radius)
    {
        
        // get the amount to spawn by evaluating the curve and multiplying by max enemies
        int amountToSpawn = Mathf.CeilToInt(_amountToSpawnCurve.Evaluate(randomMultiplier) * maxEnemies) ;

        for (int i = 0; i < amountToSpawn; i++)
        {
            StartCoroutine(IterateSpawnEvents(spawnerCenter, radius, 100, enemyLevel, 0.2f * i));
        }
        return 0f ;
    }
    //public float ValueSpawnerEvent(int maxEnemies, int enemyLevel, float randomMultiplier, Vector2 spawnerCenter, float radius)
    //{
    //    float halfTimer
    //}

    private IEnumerator IterateSpawnEvents(Vector2 center, float radius, int maxEnemyValue, int enemyLevel, float timeToWaitSeconds)
    {
        yield return new WaitForSeconds(timeToWaitSeconds);
        SpawnEvent(center, radius, 100, enemyLevel);
    }

    private void OnDrawGizmos()
    {
        DebugDrawCircle(_centerPoint, _radius, _debugCircleColor);

    }
    private void Update()
    {
        TimePassed = TimePassed + Time.deltaTime;
        DebugDrawCircle(_testPoint, 0.1f, Color.green);
        if (_nextSpawnTime >= Time.time) return;
        if (_enemyCountForTests <= 0) return;
        _enemyCountForTests --;
        //AmountSpawnerEvent(10, 1);
        _testPoint = GetValidSpawnPoint(_centerPoint, _radius, _spawnArea);
        _nextSpawnTime = Time.time + _cooldownBetweenSpawns;
    }

    public float SpawnEvent(Vector2 center, float radius, int maxEnemyValue, int enemyLevel)
    {
        Collider2D spawnArea = GameObject.FindGameObjectWithTag("SpawnArea").GetComponent<Collider2D>();

        GameObject enemyPrefab = EnemySpawnManager.Instance.GetEnemyObject(maxEnemyValue);
        Vector2 spawnPoint = GetValidSpawnPoint(center, radius, spawnArea);

        GameObject currEnemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity, _enemyPool);
        InitEnemy(currEnemy, enemyLevel);

        return EnemySpawnManager.Instance.GetEnemyValue(currEnemy);
    }

    public void InitEnemy(GameObject enemyObject, int enemyLevel)
    {
        EnemyStats currStats = enemyObject.transform.Find("EnemyStats").GetComponent<EnemyStats>();

        currStats.InitData(enemyLevel);

    }

    public Vector2 GetValidSpawnPoint(Vector2 center, float radius, Collider2D spawnArea)
    {
        while (true)
        {
            Vector2 point = GeneratePoint(center, radius);
            if (spawnArea.OverlapPoint(point))
            {
                return point;
            }
        }
    } 
    public Vector2 GeneratePoint(Vector2 center, float radius)
    {
        float randomAngle = Random.Range(0f, 2f * Mathf.PI);
        float randomRadius = radius * Random.Range(0.1f, 1f);

        float x = center.x + randomRadius * Mathf.Cos(randomAngle);
        float y = center.y + randomRadius * Mathf.Sin(randomAngle);

        return new Vector2(x, y);
    }

    private void DebugDrawCircle(Vector2 center, float radius, Color color)
    {
        const float fullCircle = 2f * Mathf.PI;
        const int segments = 36; // Number of line segments to approximate the circle

        float angleIncrement = fullCircle / segments;

        Vector2 previousPoint = center + new Vector2(radius, 0f);
        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleIncrement;
            float x = center.x + Mathf.Cos(angle) * radius;
            float y = center.y + Mathf.Sin(angle) * radius;
            Vector2 currentPoint = new Vector2(x, y);
            Debug.DrawLine(previousPoint, currentPoint, color);
            previousPoint = currentPoint;
        }
    }

}
