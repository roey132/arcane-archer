using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    private float _damage;
    private AmmoData _ammo;
    private string _effectName;
    private Rigidbody2D _rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") && !collision.CompareTag("Projectile"))
        {
            if (collision.CompareTag("Enemy")){
                EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
                enemyStats.Hit(_damage);

                collision.GetComponent<KnockbackEnemy>()?.ApplyKnockback(_rb.velocity, 2f, 0.05f);
                HandleEnemyDebuff(collision.GetComponent<Enemy>());
            }
            if (_effectName != "")
            {
                _ammo.ActivateEffect(transform.position);
            }
            transform.parent.gameObject.SetActive(false);
        }
    }

    public void InitProjectile(AmmoData data)
    {
        _ammo = data;
        _damage = data.Damage;
        _effectName = data.EffectName;
        gameObject.GetComponent<SpriteRenderer>().sprite = data.Sprite;
        _rb = GetComponent<Rigidbody2D>();
    }
    public void HandleEnemyDebuff(Enemy enemy)
    {
        print("debuff handler runs");
        if (_ammo.Debuff == null) return;
        print("Sets enemy debuff");
        enemy.SetActiveEffect(_ammo.Debuff);
    }
}
