using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyData _enemyData;
    private float _movementSpeed = 0f; // Movement speed of the enemy
    private float _health; // Health points of the enemy
    private float _currencyValue = 1;
    private Transform _playerTransform; // Reference to the player's transform
    [SerializeField] private GameManager _manager;
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _manager = FindAnyObjectByType<GameManager>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Find the player's transform
    }

    void Update()
    {
        // Move towards the player
        transform.position = Vector2.MoveTowards(transform.position, _playerTransform.position, _movementSpeed * Time.deltaTime);
        if (_health <= 0)
        {
            Die();
        }
    }

    // Destroy the enemy object
    private void Die()
    {
        IngameStats.Instance.changeIngameCurrency(_currencyValue);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("enemy hit player");
            _manager.EndScene();
        }
    }
    public void InitData(EnemyData enemyData)
    {
        _enemyData = enemyData;
        float randomEnemyDifficulty = Random.Range(1f,1.2f);
        _health = _enemyData.MinHealth * randomEnemyDifficulty;
        _movementSpeed = _enemyData.MinMovementSpeed * randomEnemyDifficulty;
        _currencyValue = Random.Range(_enemyData.MinCurrencyValue, _enemyData.MaxCurrencyValue);
    }
    public void Hit(float damage, string Type)
    {
        print(damage);
        IngameStats stats = IngameStats.Instance;
        if (Type == "spell")
        {
            print($"calc is {damage * stats.BaseMagicDamage * stats.MagicalDamageMultiplier}");
            _health -= damage * stats.BaseMagicDamage * stats.MagicalDamageMultiplier;
        }
        if (Type == "arrow")
        {
            _health -= damage * stats.BaseDamage * stats.PhysicalDamageMultiplier;
        }
        _animator.Play("EnemyHit");
    }
}