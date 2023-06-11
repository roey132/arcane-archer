using UnityEngine;


public class MeleeEnemyStats : EnemyStats
{
    void Start()
    {
        InitData(TestInit);
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        CalculateDirectionToPlayer(Player.position);
        CalculateDistanceFromPlayer(Player.position);
    }
}
