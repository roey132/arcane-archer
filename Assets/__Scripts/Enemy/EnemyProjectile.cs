using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private RangedEnemyStats _stats;
    [SerializeField] private Rigidbody2D _rb;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
            return;
        }
        if (!collision.CompareTag("Player")) return;
        IngameStats.Instance.HitPlayer(_stats.BaseDamage * _stats.DamageModifier);
        Destroy(gameObject);
    }
    public void InitProjectile(RangedEnemyStats stats, Vector2 projectileDirection, float projectilSpeed)
    {
        _stats = stats;
        _rb.velocity = projectileDirection * projectilSpeed;

        float angle = Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg;
        print($"projectile angle is {angle}");

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
