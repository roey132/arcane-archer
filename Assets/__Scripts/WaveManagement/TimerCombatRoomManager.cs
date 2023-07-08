using TMPro;
using UnityEngine;

public class TimerCombatRoomManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Spawner _spawner;
    [SerializeField] private GameObject _enemyPool;
    [SerializeField] private TextMeshPro _timerUi;
    [SerializeField] private int _timerRoomDuation;

    private float _minTimeBetweenSpawns;
    private float _maxTimeBetweenSpawns;

    private int _minEnemiesToSpawn;
    private int _maxEnemiesToSpawn;

    private int _currDifficulty;

    private float _nextSpawnTime;

    private float _currTimer;
    private float _passedTime;

    void Awake()
    {
        GameManager.OnRoomTypeChange += OnRoomTypeChange;
    }
    void OnDestroy()
    {
        GameManager.OnRoomTypeChange -= OnRoomTypeChange;
    }

    void Update()
    {
        _passedTime += Time.deltaTime;
        _currTimer -= Time.deltaTime;

        HandleTimerUI();
        HandleTimerRoom();
        HandleSpawning();
    }

    private void StartTimerRoom()
    {
        _currTimer = _timerRoomDuation;
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

    private void HandleTimerUI()
    {
        _timerUi.text = Mathf.CeilToInt(_currTimer).ToString();
    }

    private void HandleTimerRoom()
    {
        if (_currTimer > 0) return;
        Extensions.RemoveAllChildObjects(_enemyPool);
        GameManager.Instance.UpdateGameState(GameState.BuffSelection);
        enabled = false;
        _timerUi.enabled = false;
    }

    private void HandleSpawning()
    {
        if (_nextSpawnTime > Time.time) return;
        _nextSpawnTime = Time.time + CurrTimeBetweenSpawns();

        int amountToSpawn = Random.Range(_minEnemiesToSpawn, _minEnemiesToSpawn + maxEnemiesLimit() + 1);
        _spawner.SpawnerEvent(amountToSpawn, _currDifficulty);
    }
    private float CurrTimeBetweenSpawns()
    {
        float timeLimiter = (_maxTimeBetweenSpawns - _minTimeBetweenSpawns) * (_passedTime / 30);

        float currTimeBetweenSpawns = Mathf.Clamp(_maxTimeBetweenSpawns - timeLimiter, _minTimeBetweenSpawns, _maxTimeBetweenSpawns);
        return currTimeBetweenSpawns;
    }

    private int maxEnemiesLimit()
    {
        int maxEnemiesLimit = Mathf.RoundToInt((_maxEnemiesToSpawn - _minEnemiesToSpawn) * (_passedTime / 30));
        maxEnemiesLimit = Mathf.Clamp(maxEnemiesLimit, _minEnemiesToSpawn, _maxEnemiesToSpawn - _minEnemiesToSpawn);
        return maxEnemiesLimit;
    }

    public void OnRoomTypeChange(RoomType roomType)
    {
        if (roomType != RoomType.TimerRoom) return;
        print("INFO Starting Timer Room");
        StartTimerRoom();
        enabled = true;
        _timerUi.enabled = true;
    }

}