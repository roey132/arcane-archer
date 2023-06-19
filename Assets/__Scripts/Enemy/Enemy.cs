using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyData _enemyData;
    private float _movementSpeed;
    private float _currMovementSpeed;
    private float _health;
    private float _damage;
    private float _currDamage;

    private float _currencyValue;


    private Transform _playerTransform;

    [SerializeField] private GameManager _manager;
    private Animator _animator;

    private DebuffData _activeDebuffData;
    private int _debuffStack;

    private float _debuffTimer;
    private float _debuffTickTimer;

    private SpriteRenderer _springsRenderer;

    void Start()
    {
        _springsRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _manager = FindAnyObjectByType<GameManager>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Move towards the player
        transform.position = Vector2.MoveTowards(transform.position, _playerTransform.position, _currMovementSpeed * Time.deltaTime);

        if (_activeDebuffData != null) HandleDebuff();

        if (_health <= 0)
        {
            Die();
        }
    }

    public void InitData(EnemyData enemyData)
    {
        _enemyData = enemyData;
        float randomEnemyDifficulty = Random.Range(1f, 1.2f);
        _health = _enemyData.MaxHealth * randomEnemyDifficulty;
        _movementSpeed = _enemyData.ChaseMovementSpeed * randomEnemyDifficulty;
        _currMovementSpeed = _movementSpeed;
        _currencyValue = Random.Range(_enemyData.MinCurrencyValue, _enemyData.MaxCurrencyValue);
    }
    private void HandleDebuff()
    {
        if (_debuffTimer < 0)
        {
            ResetStats();
            return;
        }
        _debuffTickTimer -= Time.deltaTime;
        if (_debuffTickTimer < 0)
        {
            EnemyElementalDebuffs.ApplyDebuff(this, _activeDebuffData);
            _debuffTickTimer = _activeDebuffData.DebuffTickTime;
        }
        _debuffTimer -= Time.deltaTime;
    }
    public void SetActiveEffect(DebuffData debuff)
    {
        _debuffTimer = debuff.DebuffTime;
        _debuffTickTimer = 0;
        if (_activeDebuffData == debuff)
        {
            _debuffStack += 1;
            _debuffStack = Mathf.Clamp(_debuffStack, 1, 5);
            return;
        }
        _activeDebuffData = debuff;
        _debuffStack = 1;
    }
    private void ResetStats()
    {
        _activeDebuffData = null;
        _currMovementSpeed = _movementSpeed;
        _springsRenderer.color = Color.white;
        _currDamage = _damage;
    }

    private void Die()
    {
        IngameStats.Instance.ChangeIngameCurrency(_currencyValue);
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
    public void SlowPct(float slowPct)
    {
        _currMovementSpeed = _movementSpeed * ((100 - slowPct) / 100);
    }
    public void SetColor(Color color)
    {
        print($"attempting to set color to {color}");
        _springsRenderer.color = color;
    }
}