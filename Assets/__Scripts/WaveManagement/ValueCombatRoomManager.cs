using UnityEngine;


public class ValueCombatRoomManager : MonoBehaviour
{
    private int _currValue;

    private int _spawnedEnemies;
    private int _enemyDeaths;

    private bool _canSpawn;

    private float _maxTimeBetweenSpawns;
    private float _minTimeBetweenSpawns;

    private int _maxEnemiesToSpawn;

    private float _nextSpawnTime;

    [SerializeField] private Spawner _spawner;

    void Start()
    {
        GameManager.OnGameStateChange += OnGameStateChange;
    }
    void OnDestroy()
    {
        GameManager.OnGameStateChange -= OnGameStateChange;
    }

    private void OnEnable()
    {
        Spawner.EnemySpawn += CalculateValueOnSpawn;
        EnemyStats.EnemyDeath += OnEnemyDeath;
    }
    private void OnDisable()
    {
        Spawner.EnemySpawn -= CalculateValueOnSpawn;
        EnemyStats.EnemyDeath -= OnEnemyDeath;
    }

    void Update()
    {
        if (!_canSpawn) return;
        if (_nextSpawnTime > Time.time) return;
        _nextSpawnTime = Time.time + _maxTimeBetweenSpawns;
        _spawner.SpawnerEvent(_maxEnemiesToSpawn, 1);
    }

    public void StartValueRoom(int value)
    {
        _spawnedEnemies = 0;
        _enemyDeaths = 0;
        _currValue = value;
        _canSpawn = true;


        _maxTimeBetweenSpawns = 2f;
        _minTimeBetweenSpawns = 1.5f;

        _maxEnemiesToSpawn = 3;

        _nextSpawnTime = 0;
    }

    public void CalculateValueOnSpawn(int value)
    {
        _currValue -= value;
        _spawnedEnemies ++;
        if (_currValue <= 0) 
        {
            _canSpawn = false;
        }
    }

    public void OnGameStateChange(GameState state)
    {
        if (state != GameState.InCombat) return;
        //enabled = true;
        //StartValueRoom(20);
    }

    public void OnEnemyDeath()
    {
        _enemyDeaths++;
        if (_canSpawn) return;

        if (_enemyDeaths != _spawnedEnemies) return;
        GameManager.Instance.UpdateGameState(GameState.BuffSelection);
    }
}
