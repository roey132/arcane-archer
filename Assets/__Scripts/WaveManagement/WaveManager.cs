using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [SerializeField] private float _wavePoints = 2;
    private float _currentWavePoints;
    [SerializeField] public List<EnemyData> WaveEnemies;
    public float WaveSpawnCooldown;
    private int _currentWave;
    [SerializeField] public bool WaveIsActive;

    private void Awake()
    {
        GameManager.OnGameStateChange += SetWaveIsActive;
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= SetWaveIsActive;
    }

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _currentWave = 0;
    }
    private void AddWavePoints()
    {
        _wavePoints += 5;
    }
    public void StartWave()
    {
        WaveIsActive = true;
        _currentWave++;
        _currentWavePoints = _wavePoints;
    }
    public void EndWave()
    {
        if (!WaveIsActive) return;
        WaveIsActive = false;
        GameManager.Instance.UpdateGameState(GameState.BuffSelection);
    }
    public EnemyData GetEnemyData()
    {
        if (_currentWavePoints <= 0)
        {
            return null;
        }
        int randomEnemy = Random.Range(0, WaveManager.Instance.WaveEnemies.Count - 1);
        EnemyData enemyData = WaveEnemies[randomEnemy];
        _currentWavePoints -= enemyData.SpawnValue;
        return enemyData;
    }
    public void SetWaveIsActive(GameState state)
    {
        if (state != GameState.InCombat) return;
        StartWave();
    }
}
