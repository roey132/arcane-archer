using UnityEngine;

public abstract class EnemyStats : MonoBehaviour
{
    [SerializeField] public Transform Self;
    [SerializeField] public Transform Player;
    [SerializeField] public EnemyData TestInit;

    [Header("Base Stats")]
    public EnemyData EnemyData;
    public float BaseHealth;
    public float BaseMovementSpeed;
    public float BaseDamage;
    public float CurrencyValue;

    [Header("Current Stats")]
    public float CurrHealth;
    public float CurrMovementSpeed;
    public float CurrDamage;

    [Header("Behaviour Stats")]
    public float MaxAttackDistance;
    public float MaintainAttackDistance;

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
        EnemyData = enemyData;
        float randomEnemyDifficulty = Random.Range(1f, 1.2f);
        BaseHealth = EnemyData.MinHealth * randomEnemyDifficulty;
        BaseMovementSpeed = EnemyData.MinMovementSpeed * randomEnemyDifficulty;
        AttackCooldownSeconds = EnemyData.AttackCooldownSeconds;

        BaseDamage = EnemyData.Damage * randomEnemyDifficulty;
        CurrDamage = BaseDamage;
        CurrMovementSpeed = BaseMovementSpeed;
        CurrencyValue = Random.Range(EnemyData.MinCurrencyValue, EnemyData.MaxCurrencyValue);
        MaxAttackDistance = EnemyData.MaxAttackDistance;
        MaintainAttackDistance = EnemyData.MaintainAttackDistance;
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
