using UnityEngine;

public class RangedEnemyStats : EnemyStats
{
    void Start()
    {
        InitData(1);
    }

    void Update()
    {
        CalculateDirectionToPlayer(Player.position);
        CalculateDistanceFromPlayer(Player.position);
    }
}
