using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWaveSpellObject : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _tickDamageMultiplier;
    [SerializeField] private bool _isTickDamage;
    [SerializeField] private float _timeBetweenTicks;
    [SerializeField] private float _tickTimer;
    [SerializeField] private Collider2D _waveCollider;
    // Start is called before the first frame update
    void Start()
    {
        _isTickDamage = false;
        _tickTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        _tickTimer -= Time.deltaTime;
        DealTickDamage();
    }

    public void SetToTickDamage()
    {
        _isTickDamage = true;
    }
    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isTickDamage) return;
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyStats>().Hit(_damage);
        }
    }
    private void DealTickDamage()
    {
        if (!_isTickDamage || _tickTimer > 0) return;
        _tickTimer = _timeBetweenTicks;
        print("tick");
        ContactFilter2D contactFilter = new ContactFilter2D();
        Collider2D[] colliders = new Collider2D[20];
        int numColliders = Physics2D.OverlapCollider(_waveCollider, contactFilter, colliders);

        for (int i=0; i < numColliders; i++)
        {
            Collider2D currCollider = colliders[i];
            if (!currCollider.CompareTag("Enemy")) continue;
            currCollider.gameObject.GetComponent<EnemyStats>()?.Hit(_damage * _tickDamageMultiplier);

        }
    }
}
