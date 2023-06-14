using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData",menuName = "Enemy/BaseEnemyData")]
public class EnemyData : ScriptableObject{
    [Header("Basic Stats")]
    public string EnemyName;
    public float Damage;
    public float MaxHealth;
    public float ChaseMovementSpeed;
    public float InRangeMovementSpeed;
    public float MaintainRange;

    [Header("attack stats")]
    public float AttackRange;
    public float AttackCooldownSeconds;

    [Header("Value")]
    public float MinCurrencyValue;
    public float MaxCurrencyValue;

    [Header("game system variables")]
    public float SpawnValue;
    public float RandomizerWeight;

    [Header("rendering")]
    public Sprite EnemySprite;
    public Animator Animator;
}