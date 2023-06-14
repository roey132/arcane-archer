using UnityEngine;

public class InRangeState : EnemyState
{
    [SerializeField] private EnemyStats _stats;
    [SerializeField] private EnemyState _attackState;
    [SerializeField] private EnemyState _chaseState;

    public override EnemyState StateBehaviour()
    {
        print("running in range state");
        if (_stats.AttackRange < _stats.DistanceFromPlayer) return _chaseState;
        if (_stats.NextAttackTime < Time.time) return _attackState;
        MaintainRange();
        return null;
    }
    private void MaintainRange()
    {
        _stats.Self.position = Vector2.MoveTowards(_stats.Self.position,
            _stats.Player.position - ((Vector3)_stats.DirectionToPlayer * _stats.MaintainRange),
            _stats.InRangeMovementSpeed * _stats.MovementSpeedModifier * Time.deltaTime);
    }
}
