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

    [Header("Attack Stats")]
    public float AttackRange;
    public float AttackCooldownSeconds;

    [Header("Value")]
    public float MinCurrencyValue;
    public float MaxCurrencyValue;

    [Header("Game System Variables")]
    public int SpawnValue;
    public int RandomizerWeight;

    [Header("Rendering")]
    public Sprite EnemySprite;
    public Animator Animator;

    [Header("Prefab")]
    public GameObject Prefab;
}