using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectileObject : AbstractArrowProjectile
{
    [SerializeField] private ArrowObjectManager _arrowManager;
    [SerializeField] private Rigidbody2D _rb;

    private ArrowData _arrowData;
    private float _damage;
    private int _penetrateCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Wall")) ArrowPools.Instance.ReleaseArrow(_arrowData, transform.parent.gameObject); ;

        if (collision.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
            enemyStats?.Hit(_damage);

            collision.GetComponent<KnockbackEnemy>()?.ApplyKnockback(_rb.velocity, 10f, 0.05f);
            if (_penetrateCount <= 0)
            {
                ArrowPools.Instance.ReleaseArrow(_arrowData, transform.parent.gameObject); ;
                return;
            }
            _penetrateCount--;
        }
    }
    public override void InitProjectile(Vector2 direction, ArrowData arrowData)
    {
        _arrowData = arrowData;
        _penetrateCount = arrowData.PenetrateCount;
        _damage = arrowData.Damage;
        gameObject.GetComponent<SpriteRenderer>().sprite = arrowData.Sprite;
        gameObject.SetActive(true);
        _rb.velocity = direction * _arrowData.Speed;
    }
}
