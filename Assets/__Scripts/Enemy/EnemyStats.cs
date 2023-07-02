using System;
using UnityEngine;

public abstract class EnemyStats : MonoBehaviour
{
    [SerializeField] public Transform Self;
    [SerializeField] public Transform Player;
    [SerializeField] public EnemyData Data;

    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private AnimationCurve _levelScaleCurve;

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

    public static event Action EnemyDeath;
    public bool isDead = false;
    public void Hit(float damage)
    {
        CurrHealth -= damage;

        if (isDead) return;
        if (CurrHealth <= 0)
        {
            isDead = true;
            Die();
        }
    }

    public void Die()
    {
        EnemyDeath?.Invoke();
        Destroy(transform.parent.gameObject);
    }
    public void InitData(int enemyLevel)
    {
        // get basic objects
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = Self.GetComponent<Rigidbody2D>();

        // add level and variation to enemy stats
        float randomEnemyDifficulty = UnityEngine.Random.Range(1f, 1.2f);
        float statsMultiplier = Mathf.Pow(enemyLevel, 1.3f) * randomEnemyDifficulty;

        // scaling stats
        BaseHealth = Data.MaxHealth * statsMultiplier;
        CurrHealth = BaseHealth;
        ChaseMovementSpeed = Data.ChaseMovementSpeed * statsMultiplier;
        BaseDamage = Data.Damage * statsMultiplier;

        // static behaviour stats
        InRangeMovementSpeed = Data.InRangeMovementSpeed;
        AttackCooldownSeconds = Data.AttackCooldownSeconds;
        AttackRange = Data.AttackRange;
        MaintainRange = Data.MaintainRange;

        // alterable stats
        DamageModifier = 1f;
        MovementSpeedModifier = 1f;

        // randomize value
        CurrencyValue = UnityEngine.Random.Range(Data.MinCurrencyValue, Data.MaxCurrencyValue);
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
