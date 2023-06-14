using UnityEngine;

public class RangedEnemyStats : EnemyStats
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
