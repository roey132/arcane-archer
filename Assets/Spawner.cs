using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    [Header("Curves")]
    [SerializeField] private AnimationCurve _radiusCurve;
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
    
    private float _nextSpawnTime;
    private Vector2 _testPoint;
    private void Start()
    {
        _spawnArea = GameObject.FindGameObjectWithTag("SpawnArea").GetComponent<Collider2D>();

    }
    public float SpawnerEvent()
    {
        return 0f ;
    }

    private void OnDrawGizmos()
    {
        DebugDrawCircle(_centerPoint, _radius, _debugCircleColor);
        DebugDrawCircle(_testPoint, 0.1f, Color.green);
        if (_nextSpawnTime >= Time.time) return;
        _testPoint = GetValidSpawnPoint(_centerPoint, _radius, _spawnArea);
        _nextSpawnTime = Time.time + 0.5f;
    }
    

    public float SpawnEvent(Vector2 center, float radius, int maxEnemyValue, int enemyLevel)
    {
        Collider2D spawnArea = GameObject.FindGameObjectWithTag("SpawnArea").GetComponent<Collider2D>();

        EnemyData enemyData = new EnemyData(); // get enemy data from wave manager
        GameObject enemyPrefab = new GameObject(); // get enemy prefab from enemy data
        Vector2 spawnPoint = new Vector2(); // create function to generate spawn point
        Instantiate(enemyPrefab, spawnPoint, Quaternion.identity, _enemyPool);
        return 0f;
    }

    public Vector2 GetValidSpawnPoint(Vector2 center, float radius, Collider2D spawnArea)
    {
        print($"attempting to find point in center {center} with radius {radius}");
        while (true)
        {
            Vector2 point = GeneratePoint(center, radius);
            if (spawnArea.OverlapPoint(point))
            {
                print($"generated point {point}");
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
        print("this is running");
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
