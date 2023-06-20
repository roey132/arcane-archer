using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAttackState : EnemyState
{
    [SerializeField] private EnemyState _chaseState;
    [SerializeField] private GameObject _projectileObject;
    [SerializeField] private RangedEnemyStats _stats;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _prepAnimationDurationSeconds;
    [SerializeField] private string _idleAnimationName;
    [SerializeField] private string _prepAnimationName;
    public override EnemyState StateBehaviour()
    {
        StartCoroutine(ShootProjectile());
        _stats.NextAttackTime = Time.time + _stats.AttackCooldownSeconds;
        return _chaseState;
    }
    public IEnumerator ShootProjectile()
    {
        _animator.CrossFade(Animator.StringToHash(_prepAnimationName), 0);
        yield return new WaitForSeconds(_prepAnimationDurationSeconds);
        GameObject projectile = Instantiate(_projectileObject, GameManager.Instance.ObjectCollector);
        projectile.GetComponent<EnemyProjectile>().InitProjectile(_stats, _stats.DirectionToPlayer, _projectileSpeed);
        projectile.transform.position = _stats.Self.position;
    }
}
