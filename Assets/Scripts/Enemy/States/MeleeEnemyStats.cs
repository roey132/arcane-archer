using UnityEngine;


public class MeleeEnemyStats : EnemyStats
{
    [SerializeField] public Transform Player;
    [SerializeField] private EnemyData _testInit;
    void Start()
    {
        InitData(_testInit);
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        CalculateDirectionToPlayer(Player.position);
        CalculateDistanceFromPlayer(Player.position);
    }
}
