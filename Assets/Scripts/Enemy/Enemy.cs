using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float movementSpeed = 3f; // Movement speed of the enemy
    public int hp = 10; // Health points of the enemy

    private Transform playerTransform; // Reference to the player's transform
    public GameManager manager;

    void Start()
    {
        manager = FindAnyObjectByType<GameManager>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Find the player's transform
    }

    void Update()
    {
        // Move towards the player
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, movementSpeed * Time.deltaTime);
        if (hp <= 0)
        {
            Die();
        }
    }

    // Destroy the enemy object
    private void Die()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("enemy hit player");
            manager.endScene();
        }
    }
    public void hit(int damage)
    {
        hp -= damage;
    }
}