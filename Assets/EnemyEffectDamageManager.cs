using UnityEngine;

public class EnemyEffectDamageManager : MonoBehaviour
{
    private float _damage;
    private bool _playerWasHit;
    private void OnEnable()
    {
        _playerWasHit = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_playerWasHit) return;
        if (!collision.CompareTag("Player")) return;
        _playerWasHit=true;
        IngameStats.Instance.HitPlayer(_damage);
    }
    public void Activate(float damage)
    {
        gameObject.SetActive(true);
        _damage = damage;
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
