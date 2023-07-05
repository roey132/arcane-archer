using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCombatRoomManager : MonoBehaviour
{
    private float _currTimer;

    private float _minTimeBetweenSpawns;
    [SerializeField] private float _maxTimeBetweenSpawns;

    [SerializeField] private int _maxEnemiesToSpawn;

    private float _nextSpawnTime;

    [SerializeField] private Spawner _spawner;
    [SerializeField] private GameObject _enemyPool;

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
        _currTimer -= Time.deltaTime;
        HandleTimerRoom();
        HandleSpawning();
    }

    private void StartTimerRoom()
    {
        _currTimer = 30f;
        _nextSpawnTime = 0;
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
        _nextSpawnTime = Time.time + _maxTimeBetweenSpawns;
        _spawner.SpawnerEvent(_maxEnemiesToSpawn, 1);
    }

    public void OnGameStateChange(GameState state)
    {
        if (state != GameState.InCombat) return;
        print("timer is running");
        StartTimerRoom();
        enabled = true;
    }

}
