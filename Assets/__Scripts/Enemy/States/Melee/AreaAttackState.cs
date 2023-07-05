using UnityEngine;
using System.Collections;

public class AreaAttackState : EnemyState
{
    [Header("Objects")]
    [SerializeField] private MeleeEnemyStats _stats;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private KnockbackEnemy _knockbackEnemy;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _effect;

    [Header("States")]
    [SerializeField] private EnemyState _chaseState;

    [Header("Stats")]
    [SerializeField] private float _attackPrepTime;
    [SerializeField] private float _attackDuration;

    [Header("Indicators")]
    [SerializeField] private bool _isAttacking;
    [SerializeField] private bool _attackPerformed;

    [Header("Animation")]
    [SerializeField] private float _prepAnimationDuration; //how long does the animation take before you start the effect 
    [SerializeField] private float _afterAttackAnimationDuration; // how long does it take for the animation to end after the attack hits
    [SerializeField] private string _idleAnimationName;
    [SerializeField] private string _attackAnimationName;


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
        StartCoroutine(Attack());
    }
    private IEnumerator Attack()
    {
        _animator.CrossFade(Animator.StringToHash(_attackAnimationName), 0);
        yield return new WaitForSeconds(_prepAnimationDuration);
        CreateAttackEffect();
        _attackPerformed = true;
        yield return new WaitForSeconds(_afterAttackAnimationDuration);
        _animator.CrossFade(Animator.StringToHash(_idleAnimationName), 0);
    }
    private void CreateAttackEffect()
    {
        GameObject attackEffect = Instantiate(_effect, GameManager.Instance.ObjectCollector);
        attackEffect.transform.position = _stats.Self.position;
        attackEffect.GetComponent<EnemyEffectDamageManager>().Activate(_stats.BaseDamage * _stats.DamageModifier);
    }
}
