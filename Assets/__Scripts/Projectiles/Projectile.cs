using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _damage;
    private AmmoData _ammo;
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
            //HandleEnemyDebuff(collision.GetComponent<Enemy>());

        }
        //if (_effectName != "")
        //{
        //    _ammo.ActivateEffect(transform.position);
        //}
    }

    public void InitProjectile(AmmoData data)
    {
        _ammo = data;
        _damage = data.Damage;
        //_effectName = data.EffectName;
        gameObject.GetComponent<SpriteRenderer>().sprite = data.Sprite;
        _rb = GetComponent<Rigidbody2D>();
        _penetrateCount = data.PenetrateCount;
    }
    //public void HandleEnemyDebuff(Enemy enemy)
    //{
    //    print("debuff handler runs");
    //    if (_ammo.Debuff == null) return;
    //    print("Sets enemy debuff");
    //    enemy.SetActiveEffect(_ammo.Debuff);
    //}
}
