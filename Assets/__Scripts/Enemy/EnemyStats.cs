using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyStats : MonoBehaviour
{
    [SerializeField] public Transform Self;
    [SerializeField] public Transform Player;
    [SerializeField] public EnemyData TestInit;

    [SerializeField] private Rigidbody2D _rb;

    [Header("Base Stats")]
    public EnemyData EnemyData;
    public float BaseHealth;
    public float BaseDamage;
    public float CurrencyValue;

    [Header("Current Stats")]
    public float CurrHealth;
    public float MovementSpeedModifier;
    public float DamageModifier;

    [Header("Behaviour Stats")]
    public float AttackRange;
    public float MaintainRange;
    public float ChaseMovementSpeed;
    public float InRangeMovementSpeed;

    [Header("Calculated Measures")]
    public float DistanceFromPlayer;
    public Vector2 DirectionToPlayer;

    [Header("Attack Varaibles")]
    public float AttackCooldownSeconds;
    public float NextAttackTime;



    public void Hit(float damage)
    {
        CurrHealth -= damage;
        if (CurrHealth < 0)
        {
            Die();
        }
    }
    public void Die()
    {
        // death function
    }
    public void InitData(EnemyData enemyData)
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = Self.GetComponent<Rigidbody2D>();

        EnemyData = enemyData;
        float randomEnemyDifficulty = Random.Range(1f, 1.2f);
        BaseHealth = EnemyData.MaxHealth * randomEnemyDifficulty;
        ChaseMovementSpeed = EnemyData.ChaseMovementSpeed * randomEnemyDifficulty;
        InRangeMovementSpeed = EnemyData.InRangeMovementSpeed * randomEnemyDifficulty;
        AttackCooldownSeconds = EnemyData.AttackCooldownSeconds;

        BaseDamage = EnemyData.Damage * randomEnemyDifficulty;
        DamageModifier = 1f;
        MovementSpeedModifier = 1f;
        CurrencyValue = Random.Range(EnemyData.MinCurrencyValue, EnemyData.MaxCurrencyValue);
        AttackRange = EnemyData.AttackRange;
        MaintainRange = EnemyData.MaintainRange;
    }
    
    public void CalculateDistanceFromPlayer(Vector3 playerPosition)
    {
        DistanceFromPlayer =  Vector3.Distance(transform.position ,playerPosition);

    }

    public void CalculateDirectionToPlayer(Vector3 playerPosition)
    {
        DirectionToPlayer = (playerPosition - transform.position).normalized;
    }
}
