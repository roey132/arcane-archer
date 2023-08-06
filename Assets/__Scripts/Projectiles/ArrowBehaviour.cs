using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    private float _damage;
    private ArrowData _ammo;
    private string _effectName;
    private Rigidbody2D _rb;
    private int _penetrateCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall")) transform.parent.gameObject.SetActive(false);

        if (collision.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
            enemyStats?.Hit(_damage);

            collision.GetComponent<KnockbackEnemy>()?.ApplyKnockback(_rb.velocity, 10f, 0.05f);
            if (_penetrateCount <= 0)
            {
                transform.parent.gameObject.SetActive(false);
                return;
            }
            _penetrateCount --;
        }
    }

    public void InitProjectile(ArrowData data)
    {
        _ammo = data;
        _damage = data.Damage;
        gameObject.GetComponent<SpriteRenderer>().sprite = data.Sprite;
        _rb = GetComponent<Rigidbody2D>();
        _penetrateCount = data.PenetrateCount;
    }
}
