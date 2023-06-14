using System.Collections;
using UnityEngine;

public class DashAttackState : EnemyState
{
    [Header("Objects")]
    [SerializeField] private MeleeEnemyStats _stats;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private KnockbackEnemy _knockbackEnemy;

    [Header("Dash Variables")]
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _dashPrepareTime;

    [Header("States")]
    [SerializeField] private EnemyState _chaseState;

    [SerializeField]  private bool _isAttacking;
    [SerializeField] private bool _attackPerformed;

    private void OnDisable()
    {
        _stats.CollidedWithPlayer -= HitPlayerOnCollision;
    }

    public override EnemyState StateBehaviour()
    {
        print("running attack state");
        if (!_isAttacking) HandleAttack();
        if (_attackPerformed) 
        {
            _isAttacking = false;
            _attackPerformed = false;
            return _chaseState;
        }
        return null;
    }

    private void HandleAttack()
    {
        _stats.NextAttackTime = Time.time + _stats.AttackCooldownSeconds;
        _isAttacking = true;
        StartCoroutine(Dash());
    }
    private IEnumerator Dash()
    {
        print("preparing dash");
        float timer = _dashPrepareTime * 0.8f;
        _knockbackEnemy.enabled = false;
        while (timer > 0)
        {
            yield return null;
            timer -= Time.deltaTime;
            Debug.DrawRay(transform.position, _stats.Player.position - transform.position, Color.red);
        }
        print("setting dash vector");
        Vector2 DashDir = _stats.Player.position - transform.position;
        yield return new WaitForSeconds(_dashPrepareTime * 0.2f);

        print("dashing");
        _stats.CollidedWithPlayer += HitPlayerOnCollision;

        _rb.velocity = DashDir.normalized * _dashSpeed;
        yield return new WaitForSeconds(_dashDuration);
        print("finish dashing");
        _rb.velocity = Vector2.zero;
        _attackPerformed = true;
        _knockbackEnemy.enabled = true;
        _stats.CollidedWithPlayer -= HitPlayerOnCollision;
    }
    private void HitPlayerOnCollision()
    {
        IngameStats.Instance.hitPlayer(_stats.BaseDamage * _stats.DamageModifier);
    }
}
