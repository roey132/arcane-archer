using System.Collections;
using UnityEngine;

public class DashAttackState : EnemyState
{
    [Header("Objects")]
    [SerializeField] private MeleeEnemyStats _stats;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private KnockbackEnemy _knockbackEnemy;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _physicalCollider;

    [Header("Dash Variables")]
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _dashPrepareTime;

    [Header("States")]
    [SerializeField] private EnemyState _chaseState;

    [SerializeField]  private bool _isAttacking;
    [SerializeField] private bool _attackPerformed;

    [SerializeField] private string _prepAnimationName;
    [SerializeField] private string _idleAnimationName;

    private bool _isDashing;
    private void OnEnable()
    {
        _stats.CollidedWithPlayer += HitPlayerOnCollision;
    }

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
        float timer = _dashPrepareTime * 0.8f;
        _knockbackEnemy.enabled = false;

        // debug ray
        while (timer > 0)
        {
            yield return null;
            timer -= Time.deltaTime;
            Debug.DrawRay(transform.position, _stats.Player.position - transform.position, Color.red);
        }
        // set direction of dash before dashing
        Vector2 DashDir = _stats.Player.position - transform.position;
        _animator.CrossFade(Animator.StringToHash(_prepAnimationName), 0);

        yield return new WaitForSeconds(_dashPrepareTime * 0.2f);

        _isDashing = true;

        _rb.velocity = DashDir.normalized * _dashSpeed;
        yield return new WaitForSeconds(_dashDuration);

        // finish dash
        _animator.CrossFade(Animator.StringToHash(_idleAnimationName), 0);

        _rb.velocity = Vector2.zero;

        _attackPerformed = true;
        _knockbackEnemy.enabled = true;

        _isDashing = false;
    }
    private void HitPlayerOnCollision()
    {
        if (!_isDashing) return;
        IngameStats.Instance.HitPlayer(_stats.BaseDamage * _stats.DamageModifier);
    }
}
