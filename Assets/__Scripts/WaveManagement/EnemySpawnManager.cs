using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance;

    [SerializeField] private float _wavePoints = 100;
    private float _currentWavePoints;

    [SerializeField] public List<GameObject> AvailableEnemies;

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
    public GameObject GetEnemyObject()
    {
        for (int i = 0; i < 1000; i++)
        {
            int randomEnemy = Random.Range(0, AvailableEnemies.Count);
            GameObject enemy = AvailableEnemies[randomEnemy];
            return enemy;
        }
        return null;
    }

    public int GetEnemyValue(GameObject enemyObject)
    {
        EnemyStats currStats = enemyObject.transform.Find("EnemyStats").GetComponent<EnemyStats>();

        return currStats.Data.SpawnValue;
    }

    public void SetWaveIsActive(GameState state)
    {
        if (state != GameState.InCombat) return;
        StartWave();
    }
}
