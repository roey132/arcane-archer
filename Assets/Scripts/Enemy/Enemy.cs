using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _movementSpeed = 0f; // Movement speed of the enemy
    private float _hp = 10; // Health points of the enemy
    private int _currencyValue = 1;
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
        if (_hp <= 0)
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
            _manager.endScene();
        }
    }
    public void Hit(float damage, string Type)
    {
        print(damage);
        IngameStats stats = IngameStats.Instance;
        if (Type == "spell")
        {
            print($"calc is {damage * stats.BaseMagicDamage * stats.MagicalDamageMultiplier}");
            _hp -= damage * stats.BaseMagicDamage * stats.MagicalDamageMultiplier;
        } 
        if (Type == "arrow")
        {
            _hp -= damage * stats.BaseDamage * stats.PhysicalDamageMultiplier;
        }
        _animator.Play("EnemyHit");
    }
    public void SetHealth(float health)
    {
        _hp = health;
    }
    public void SetValue(int value)
    {
        _currencyValue = value;
    }
}