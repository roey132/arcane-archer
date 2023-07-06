using UnityEngine;


public class ValueCombatRoomManager : MonoBehaviour
{
    private int _currValue;

    private int _spawnedEnemies;
    private int _enemyDeaths;

    private bool _canSpawn;

    private float _maxTimeBetweenSpawns;
    private float _minTimeBetweenSpawns;

    private int _minEnemiesToSpawn;
    private int _maxEnemiesToSpawn;

    private float _nextSpawnTime;

    private int _currDifficulty;

    [SerializeField] private Spawner _spawner;

    void Awake()
    {
        GameManager.OnRoomTypeChange += OnRoomTypeChange;
    }
    void OnDestroy()
    {
        GameManager.OnRoomTypeChange -= OnRoomTypeChange;
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

    public void StartValueRoom()
    {
        _spawnedEnemies = 0;
        _enemyDeaths = 0;
        _currDifficulty = GameManager.Instance.CurrentRoomDifficulty;
        _currValue = DifficultyCalculations.DifficultyValue(_currDifficulty);
        _canSpawn = true;


        _maxTimeBetweenSpawns = DifficultyCalculations.MinTimeBetweenSpawns(_currDifficulty);
        _minTimeBetweenSpawns = DifficultyCalculations.MaxTimeBetweenSpawns(_currDifficulty);

        _minEnemiesToSpawn = DifficultyCalculations.MinEnemiesSpawned(_currDifficulty);
        _maxEnemiesToSpawn = DifficultyCalculations.MaxEnemiesSpawned(_currDifficulty);

        print($"INFO room difficulty {_currDifficulty}");
        print($"INFO enemies min:{_minEnemiesToSpawn} max:{_maxEnemiesToSpawn}");
        print($"INFO time min:{_minTimeBetweenSpawns} max:{_maxTimeBetweenSpawns}");

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

    public void OnRoomTypeChange(RoomType roomType)
    {
        if (roomType != RoomType.ValueRoom) return;
        print("INFO Starting Value Room");
        StartValueRoom();
        enabled = true;
    }

    public void OnEnemyDeath()
    {
        _enemyDeaths++;
        if (_canSpawn) return;

        if (_enemyDeaths != _spawnedEnemies) return;
        GameManager.Instance.UpdateGameState(GameState.BuffSelection);
        enabled = false;
    }
}
