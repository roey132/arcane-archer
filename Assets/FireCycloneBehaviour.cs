using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FireCycloneBehaviour : MonoBehaviour
{
    [SerializeField] private float _followRadius; // The radius to detect colliders.
    [SerializeField] private LayerMask _layerMask; // Specify which layers the colliders should belong to.
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _timeBetweenTicks;
    [SerializeField] private float _tickDamageMultiplier;
    [SerializeField] private Collider2D _cycloneCollider;


    private Collider2D _closestCollider; // Variable to store the closest collider found.

    private float _damage;

    private float _detectionTimer = 0.1f;
    private float _tickTimer;

    void Update()
    {
        _detectionTimer -= Time.deltaTime;
        _tickTimer -= Time.deltaTime;
        if (_detectionTimer <= 0)
        {
            _detectionTimer = 0.1f;
            SetClosestCollider();
        }

        if (_closestCollider != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, _closestCollider.transform.position, _movementSpeed * Time.deltaTime);
        }

        if (_tickTimer <= 0f)
        {
            DealTickDamage();
            _tickTimer = _timeBetweenTicks;
        }

    }

    private void SetClosestCollider()
    {
        // Detect colliders in the given radius.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _followRadius, _layerMask);

        // Initialize the closest distance as a large value to ensure the first collider found becomes the closest.
        float closestDistance = Mathf.Infinity;

        // Process the colliders found.
        foreach (Collider2D collider in colliders)
        {
            // Calculate the distance between the object and the collider.
            float distance = Vector2.Distance(transform.position, collider.transform.position);

            // Check if this collider is closer than the previous closest one.
            if (distance < closestDistance)
            {
                closestDistance = distance;
                _closestCollider = collider;
            }
        }

        // If there is a closest collider, you can perform actions or access information about it.
        if (_closestCollider != null)
        {
            Debug.Log("Closest collider: " + _closestCollider.name);
        }
    }
    private void DealTickDamage()
    {
        if (_tickTimer > 0) return;
        _tickTimer = _timeBetweenTicks;
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = true;
        Collider2D[] colliders = new Collider2D[20];
        int numColliders = Physics2D.OverlapCollider(_cycloneCollider, contactFilter, colliders);

        for (int i = 0; i < numColliders; i++)
        {
            Collider2D currCollider = colliders[i];
            if (!currCollider.CompareTag("Enemy")) continue;
            print($"{currCollider}");
            if (currCollider.gameObject.GetComponent<EnemyStats>())
            {
                print("found class");
            }
            currCollider.gameObject.GetComponent<EnemyStats>()?.Hit(_damage * _tickDamageMultiplier);
        }
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    // Optionally, draw a visual representation of the radius in the Unity Editor for better visualization.
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _followRadius);
    }
}
