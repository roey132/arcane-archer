using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData",menuName = "Enemy/BaseEnemyData")]
public class EnemyData : ScriptableObject{
    public string EnemyName;
    public float MaxHealth;
    public float ChaseMovementSpeed;
    public float InRangeMovementSpeed;
    public float MaxAttackDistance;
    public float SpawnValue;
    public float MinCurrencyValue;
    public float MaxCurrencyValue;
    public float Damage;
    public float AttackCooldownSeconds;
    public float RandomizerWeight;
    public Sprite EnemySprite;
    public Animator Animator;
}