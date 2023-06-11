using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData",menuName = "Enemy/BaseEnemyData")]
public class EnemyData : ScriptableObject{
    public string EnemyName;
    public float MinHealth;
    public float MinMovementSpeed;
    public float MaxAttackDistance;
    public float MaintainAttackDistance;
    public float SpawnValue;
    public float MinCurrencyValue;
    public float MaxCurrencyValue;
    public float Damage;
    public float Level;
    public float AttackCooldownSeconds;
    public Sprite EnemySprite;
    public Animator Animator;
}