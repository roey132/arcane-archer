using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAttackState : EnemyState
{
    [SerializeField] private EnemyState _chaseState;
    [SerializeField] private GameObject _projectileObject;
    [SerializeField] private RangedEnemyStats _stats;
    [SerializeField] private float _projectileSpeed;
    public override EnemyState StateBehaviour()
    {
        ShootProjectile();
        _stats.NextAttackTime = Time.time + _stats.AttackCooldownSeconds;
        return _chaseState;
    }
    public void ShootProjectile()
    {
        GameObject projectile = Instantiate(_projectileObject,GameManager.Instance.ObjectCollector);
        projectile.GetComponent<EnemyProjectile>().InitProjectile(_stats, _stats.DirectionToPlayer, _projectileSpeed);
        projectile.transform.position = _stats.Self.position;
    }
    
}
