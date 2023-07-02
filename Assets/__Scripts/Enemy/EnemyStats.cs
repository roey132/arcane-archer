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



    public void Hit(float damage)
    {
        print($"damage done to enemy is ({damage})");
        CurrHealth -= damage;
        if (CurrHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Destroy(transform.parent.gameObject);
    }
    public void InitData(int enemyLevel)
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = Self.GetComponent<Rigidbody2D>();

        // add level to calculation

        float randomEnemyDifficulty = Random.Range(1f, 1.2f);
        BaseHealth = Data.MaxHealth * randomEnemyDifficulty;
        ChaseMovementSpeed = Data.ChaseMovementSpeed * randomEnemyDifficulty;
        InRangeMovementSpeed = Data.InRangeMovementSpeed * randomEnemyDifficulty;
        AttackCooldownSeconds = Data.AttackCooldownSeconds;

        BaseDamage = Data.Damage * randomEnemyDifficulty;
        DamageModifier = 1f;
        MovementSpeedModifier = 1f;
        CurrencyValue = Random.Range(Data.MinCurrencyValue, Data.MaxCurrencyValue);
        AttackRange = Data.AttackRange;
        MaintainRange = Data.MaintainRange;
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
