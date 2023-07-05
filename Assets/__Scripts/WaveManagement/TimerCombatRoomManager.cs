using UnityEngine;

public class TimerCombatRoomManager : MonoBehaviour
{
    private float _currTimer;

    [SerializeField] private float _minTimeBetweenSpawns;
    [SerializeField] private float _maxTimeBetweenSpawns;

    [SerializeField] private int _minEnemiesToSpawn;
    [SerializeField] private int _maxEnemiesToSpawn;

    private float _nextSpawnTime;

    [SerializeField] private Spawner _spawner;
    [SerializeField] private GameObject _enemyPool;

    private int _currDifficulty;
    private float _passedTime;

    void Awake()
    {
        GameManager.OnGameStateChange += OnGameStateChange;
    }
    void OnDestroy()
    {
        GameManager.OnGameStateChange -= OnGameStateChange;
    }

    void Update()
    {
        _passedTime += Time.deltaTime;
        _currTimer -= Time.deltaTime;

        print($"passed time {_passedTime}");
        HandleTimerRoom();
        HandleSpawning();
    }

    private void StartTimerRoom()
    {
        _currTimer = 40f;
        _passedTime = 0;
        _nextSpawnTime = 0;
        _currDifficulty = GameManager.Instance.CurrentRoomDifficulty;
        _minTimeBetweenSpawns = DifficultyCalculations.MinTimeBetweenSpawns(_currDifficulty);
        _maxTimeBetweenSpawns = DifficultyCalculations.MaxTimeBetweenSpawns(_currDifficulty);
        _minEnemiesToSpawn = DifficultyCalculations.MinEnemiesSpawned(_currDifficulty);
        _maxEnemiesToSpawn = DifficultyCalculations.MaxEnemiesSpawned(_currDifficulty);

        print($"INFO room difficulty {_currDifficulty}");
        print($"INFO enemies min:{_minEnemiesToSpawn} max:{_maxEnemiesToSpawn}");
        print($"INFO time min:{_minTimeBetweenSpawns} max:{_maxTimeBetweenSpawns}");
    }
    private void HandleTimerRoom()
    {
        if (_currTimer > 0) return;
        Extensions.RemoveAllChildObjects(_enemyPool);
        GameManager.Instance.UpdateGameState(GameState.BuffSelection);
        enabled = false;
    }

    private void HandleSpawning()
    {
        if (_nextSpawnTime > Time.time) return;
        print($"Time between spawns : {CurrTimeBetweenSpawns()} passedTime:{_passedTime}");
        _nextSpawnTime = Time.time + CurrTimeBetweenSpawns();

        print($"max enemies to spawn {_minEnemiesToSpawn + maxEnemiesLimit()} passedTime:{_passedTime}");
        int amountToSpawn = Random.Range(_minEnemiesToSpawn, _minEnemiesToSpawn + maxEnemiesLimit() + 1);
        _spawner.SpawnerEvent(amountToSpawn, _currDifficulty);
    }
    private float CurrTimeBetweenSpawns()
    {
        float timeLimiter = (_maxTimeBetweenSpawns - _minTimeBetweenSpawns) * (_passedTime / 30);
        print($"time limit calculation {(_maxTimeBetweenSpawns - _minTimeBetweenSpawns) * (_passedTime / 30)}");

        float currTimeBetweenSpawns = Mathf.Clamp(_maxTimeBetweenSpawns - timeLimiter, _minTimeBetweenSpawns, _maxTimeBetweenSpawns);
        return currTimeBetweenSpawns;
    }

    private int maxEnemiesLimit()
    {
        int maxEnemiesLimit = Mathf.RoundToInt((_maxEnemiesToSpawn - _minEnemiesToSpawn) * (_passedTime / 30));
        maxEnemiesLimit = Mathf.Clamp(maxEnemiesLimit, _minEnemiesToSpawn, _maxEnemiesToSpawn - _minEnemiesToSpawn);
        return maxEnemiesLimit;
    }

    public void OnGameStateChange(GameState state)
    {
        if (state != GameState.InCombat) return;
        print("timer is running");
        StartTimerRoom();
        enabled = true;
    }

}