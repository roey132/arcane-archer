using System;
using UnityEngine;
using UnityEngine.Events;

public class MeleeEnemyStats : EnemyStats
{
    public event Action CollidedWithPlayer;
    void Start()
    {
        InitData(1);
    }

    void Update()
    {
        CalculateDirectionToPlayer(Player.position);
        CalculateDistanceFromPlayer(Player.position);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        CollidedWithPlayer?.Invoke();
    }
}
