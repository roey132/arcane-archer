using UnityEngine;


public class MeleeEnemyStats : EnemyStats
{
    void Start()
    {
        InitData(TestInit);
    }

    void Update()
    {
        CalculateDirectionToPlayer(Player.position);
        CalculateDistanceFromPlayer(Player.position);
    }
}
