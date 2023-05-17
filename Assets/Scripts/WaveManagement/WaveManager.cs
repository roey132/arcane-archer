using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    private float _wavePoints = 10;
    private float _currentWavePoints;
    [SerializeField] public List<EnemyData> WaveEnemies;
    public float WaveSpawnCooldown;
    private int _currentWave;
    [SerializeField] public bool WaveIsActive;

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
        _currentWave ++;
        _currentWavePoints = _wavePoints;
        WaveIsActive = true;
    }
    public void EndWave()
    {
        GameManager.Instance.showItemPickItemUI();
    }
    public EnemyData GetEnemyData()
    {
        if (_currentWavePoints <= 0)
        {
            WaveIsActive = false;
            return null;
        }
        int randomEnemy = Random.Range(0, WaveManager.Instance.WaveEnemies.Count - 1);
        EnemyData enemyData = WaveEnemies[randomEnemy];
        _currentWavePoints -= enemyData.SpawnValue;
        return enemyData;
    }
}
