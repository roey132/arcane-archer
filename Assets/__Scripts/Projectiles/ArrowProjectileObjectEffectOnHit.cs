using UnityEngine;

public class ArrowProjectileObjectEffectOnHit : AbstractArrowProjectile
{
    [SerializeField] private ArrowObjectManager _arrowManager;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private AbstractOnHitArrowBehaviour _onHitEffectObject;

    private ArrowData _arrowData;
    private float _damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Wall")) 
        {
            ArrowPools.Instance.ReleaseArrow(_arrowData, transform.parent.gameObject);
        }

        if (collision.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
            enemyStats?.Hit(_damage);
            collision.GetComponent<KnockbackEnemy>()?.ApplyKnockback(_rb.velocity, 10f, 0.05f);
            gameObject.SetActive(false);
            _onHitEffectObject.InitOnHitBehaviour(transform.position);
        }
    }
    public override void InitProjectile(Vector2 direction, ArrowData arrowData)
    {
        _arrowData = arrowData;
        _damage = arrowData.Damage;
        gameObject.GetComponent<SpriteRenderer>().sprite = arrowData.Sprite;
        gameObject.SetActive(true);
        _rb.velocity = direction * _arrowData.Speed;
    }
}
